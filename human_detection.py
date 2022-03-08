from depthai_sdk import Previews
from depthai_sdk.managers import PipelineManager, PreviewManager, NNetManager, BlobManager
import depthai as dai
import cv2

class elden:
    pm = PipelineManager()
    pm.createColorCam(previewSize=(1920, 1080), xout=True)


    labelMap = ["background", "aeroplane", "bicycle", "bird", "boat", "bottle", "bus", "car", "cat", "chair", "cow",
             "diningtable", "dog", "horse", "motorbike", "person", "pottedplant", "sheep", "sofa", "train", "tvmonitor"]


    pipeline = dai.Pipeline()

    # Define sources and outputs
    camRgb = pipeline.create(dai.node.ColorCamera)
    spatialDetectionNetwork = pipeline.create(dai.node.MobileNetSpatialDetectionNetwork)
    monoLeft = pipeline.create(dai.node.MonoCamera)
    monoRight = pipeline.create(dai.node.MonoCamera)
    stereo = pipeline.create(dai.node.StereoDepth)

    xoutRgb = pipeline.create(dai.node.XLinkOut)
    xoutNN = pipeline.create(dai.node.XLinkOut)
    xoutBoundingBoxDepthMapping = pipeline.create(dai.node.XLinkOut)
    xoutDepth = pipeline.create(dai.node.XLinkOut)

    xoutRgb.setStreamName("rgb")
    xoutNN.setStreamName("detections")
    xoutBoundingBoxDepthMapping.setStreamName("boundingBoxDepthMapping")
    xoutDepth.setStreamName("depth")

    

    

    bm = BlobManager(zooName="mobilenet-ssd")
    nm = NNetManager(inputSize=(300, 300), nnFamily="mobilenet")
    nn = nm.createNN(pipeline=pm.pipeline, nodes=pm.nodes, source=Previews.color.name,
                    blobPath=bm.getBlob(shaves=6, openvinoVersion=pm.pipeline.getOpenVINOVersion()))

    pm.addNn(nn)
    
    def ring():
        
        with dai.Device(elden.pm.pipeline) as device:
            pv = PreviewManager(display=[Previews.color.name])
            pv.createQueues(device)
            elden.nm.createQueues(device)
            nnData = []

            previewQueue = device.getOutputQueue(name="rgb", maxSize=4, blocking=False)
            inPreview = previewQueue.get()
            frame = inPreview.getCvFrame()

            detectionNNQueue = device.getOutputQueue(name="detections", maxSize=4, blocking=False)
            inDet = detectionNNQueue.get()
            detections = inDet.detections

            height = frame.shape[0]
            width  = frame.shape[1]

           

            for detection in detections:
                # Denormalize bounding box
                x1 = int(detection.xmin * width)
                x2 = int(detection.xmax * width)
                y1 = int(detection.ymin * height)
                y2 = int(detection.ymax * height)
                try:
                    label = elden.labelMap[detection.label]
                except:
                    label = detection.label
                cv2.putText(frame, str(label), (x1 + 10, y1 + 20), cv2.FONT_HERSHEY_TRIPLEX, 0.5, 255)
                cv2.putText(frame, "{:.2f}".format(detection.confidence*100), (x1 + 10, y1 + 35), cv2.FONT_HERSHEY_TRIPLEX, 0.5, 255)
                cv2.putText(frame, f"X: {int(detection.spatialCoordinates.x)} mm", (x1 + 10, y1 + 50), cv2.FONT_HERSHEY_TRIPLEX, 0.5, 255)
                cv2.putText(frame, f"Y: {int(detection.spatialCoordinates.y)} mm", (x1 + 10, y1 + 65), cv2.FONT_HERSHEY_TRIPLEX, 0.5, 255)
                cv2.putText(frame, f"Z: {int(detection.spatialCoordinates.z)} mm", (x1 + 10, y1 + 80), cv2.FONT_HERSHEY_TRIPLEX, 0.5, 255)
            
            while True:
                pv.prepareFrames()
                inNn = elden.nm.outputQueue.tryGet()

                if inNn is not None:
                    nnData = elden.nm.decode(inNn)

                elden.nm.draw(pv, nnData)
                pv.showFrames()

                if cv2.waitKey(1) == ord('q'):
                    break

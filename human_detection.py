from depthai_sdk import Previews
from depthai_sdk.managers import PipelineManager, PreviewManager, NNetManager, BlobManager
import depthai as dai
import cv2

class elden:
    pm = PipelineManager()
    pm.createColorCam(previewSize=(1920, 1080), xout=True)


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


            while True:
                pv.prepareFrames()
                inNn = elden.nm.outputQueue.tryGet()

                if inNn is not None:
                    nnData = elden.nm.decode(inNn)

                elden.nm.draw(pv, nnData)
                pv.showFrames()

                if cv2.waitKey(1) == ord('q'):
                    break

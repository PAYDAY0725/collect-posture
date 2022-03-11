//*********************************************
//  program for the Posrabbi doll
//*********************************************
//  for OPENHackU
//  PAYDAY
//*********************************************

#include <M5Atom.h>
#include <ESP32Servo.h>
#include <FirebaseESP32.h>


// SG-92R params
#define MIN_PLS 500
#define MAX_PLS 2400

// firebase, wifi settings
#define FIREBASE_HOST "hogehoge"
#define FIREBASE_AUTH "hogehogee"
#define WIFI_SSID "hogehogeee"
#define WIFI_PASSWORD "hogehogeeeee"
FirebaseData fbd;

// servo settings
Servo servo;
ESP32PWM pwm;
int servoPin = 21;

// initial position(deg)
int pos = 0;
float pos_fire = 0;


void setup(){
  // for debug
  Serial.begin(115200);
  Serial.println("==== setup ====");


  // allow allocation of all timers
  ESP32PWM::allocateTimer(0);
  ESP32PWM::allocateTimer(1);
  ESP32PWM::allocateTimer(2);
  ESP32PWM::allocateTimer(3);
  Serial.println("==== allow allocation ====");


  // M5 init
  M5.begin(true, false, true);
  delay(50);
  Serial.println("==== M5 init ====");

  // wifi init
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  while (WiFi.status() != WL_CONNECTED)
  {
    M5.dis.drawpix(0, 0xff0000);        // Green(not-ready)
  }
  Serial.println("==== wifi init ====");
  Serial.print("ip: ");
  Serial.println(WiFi.localIP());

  // firebase init
  Firebase.begin(FIREBASE_HOST, FIREBASE_AUTH);
  Firebase.reconnectWiFi(true);
  Serial.println("==== firebase init ====");

  // servo init
  servo.setPeriodHertz(50);
  servo.attach(servoPin, MIN_PLS, MAX_PLS);
  M5.dis.drawpix(0, 0x0000f0);        // BLUE(ready)
  servo.write(0);
  delay(1);
  Serial.println("==== servo init ====");
}


void loop(){

  // get pos data
  Firebase.getFloat(fbd, "/z/");
  pos_fire = fbd.to<float>();

  // calc pose
  if(pos_fire<=0 && pos_fire>=-200)
  {
    pos = (int)pos_fire * -0.3;
  }
  else if(pos_fire>0){
    pos = 0;
  }
  else if(pos_fire<-200){
    pos = 60;
  }


  // move neck
  servo.write(pos);
  delay(10);


  // for debug
  Serial.print("pos_fire: ");
  Serial.println(pos_fire);
  Serial.print("pos: ");
  Serial.println(pos);
}

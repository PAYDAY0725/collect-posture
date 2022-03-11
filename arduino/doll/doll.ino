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

}

void loop(){

}


const int led_pin = 9;
const int vol_pin = 1;

int vol_value = 0;

void setup() {
  Serial.begin( 9600 );
  pinMode(13, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(11, OUTPUT);
  pinMode(10, OUTPUT);
  pinMode(8, OUTPUT);
}

void loop() {
    int i = 0;
    while(1) {
      //シリアル通信
      if (Serial.available()) { // シリアル通信でデータが送られてくるまで待つ。
        char c = Serial.read(); // 一文字分データを取り出す。
        if (c == 'a') { // nが送られてきたらLEDを点灯させる。
          digitalWrite(13, HIGH);
        } else if(c == 'v') { // fが送られてきたらLEDを消灯させる。
          digitalWrite(13, LOW);
        }
        // 1
        if (c == 'b') { // nが送られてきたらLEDを点灯させる。
          digitalWrite(12, HIGH);
        } else if(c == 'w') { // fが送られてきたらLEDを消灯させる。
          digitalWrite(12, LOW);
        }
        // 2
        if (c == 'c') { // nが送られてきたらLEDを点灯させる。
          digitalWrite(11, HIGH);
        } else if(c == 'x') { // fが送られてきたらLEDを消灯させる。
          digitalWrite(11, LOW);
        }
        // 3
        if (c == 'd') { // nが送られてきたらLEDを点灯させる。
          digitalWrite(10, HIGH);
        } else if(c == 'y') { // fが送られてきたらLEDを消灯させる。
          digitalWrite(10, LOW);
        }
        // 4
        if (c == 'e') { // nが送られてきたらLEDを点灯させる。
          digitalWrite(8, HIGH);
        } else if(c == 'z') { // fが送られてきたらLEDを消灯させる。
          digitalWrite(8, LOW);
        }
      }
  vol_value = analogRead( 1 );

  analogWrite( led_pin, vol_value/4 );

  Serial.println( vol_value );
  
  delay( 50 );
    }
}

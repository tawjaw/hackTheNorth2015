// Firmware code for the robot
// Code adapated from http://www.instructables.com/id/Arduino-Modules-L298N-Dual-H-Bridge-Motor-Controll/

// Indicator LED
const uint8_t indicator_led = 13;

// movement constants
const uint32_t movement_period = 100;

// Motor 1
const uint8_t motor1_dir1 = 2;
const uint8_t motor1_dir2 = 3;
const uint8_t motor1_speed = 4; 

// Motor 2
const uint8_t motor2_dir1 = 7;
const uint8_t motor2_dir2 = 6;
const uint8_t motor2_speed = 5; 

void setup() {  
	// initialize serial communication
	Serial1.begin(9600);
	
	//Define indicator LED
	pinMode(indicator_led, OUTPUT);

	//Define pins
	pinMode(motor1_dir1,OUTPUT);
	pinMode(motor1_dir2,OUTPUT);
	pinMode(motor1_speed,OUTPUT);
	pinMode(motor2_dir1,OUTPUT);
	pinMode(motor2_dir2,OUTPUT);
	pinMode(motor2_speed,OUTPUT);
	
	Serial1.println("Robot started");


}

uint32_t indicator_led_t0 = millis();
uint32_t indicator_move_t0 = millis();

void loop() {

	// Initialize the Serial interface:
	uint32_t curr_time = millis();
	if (Serial1.available() > 0) {
		int inByte = Serial1.read();
		int speed; // Local variable
		digitalWrite(indicator_led, HIGH);
		indicator_led_t0 = millis();
		switch (inByte) {
			
			//wasd
			case 'w':
				move_motor1(200, 0);
				move_motor2(255, 0);
				indicator_move_t0 = millis();
				break;
			case 's':
				move_motor1(200, 1);
				move_motor2(255, 1);
				indicator_move_t0 = millis();
				break;
			case 'a':
				move_motor1(255, 0);
				move_motor2(255, 1);
				indicator_move_t0 = millis();
				break;
			case 'd':
				move_motor1(255, 1);
				move_motor2(255, 0);
				indicator_move_t0 = millis();
				break;

			//______________Motor 1______________

			case '1': // Motor 1 Forward
				move_motor1(255, 0);
				Serial1.println("Motor 1 Forward");
				Serial1.println("   ");
				break;

			case '2': // Motor 1 Stop (Freespin)
				move_motor1(0, 0);
				Serial1.println("Motor 1 Stop");
				Serial1.println("   ");
				break;

			case '3': // Motor 1 Reverse
				move_motor1(255, 1);
				Serial1.println("Motor 1 Reverse"); 
				Serial1.println("   "); 
				break;

			//______________Motor 2______________

			case '4': // Motor 2 Forward
				move_motor2(255, 0);
				Serial1.println("Motor 2 Forward");
				Serial1.println("   ");
				break;

			case '5': // Motor 1 Stop (Freespin)
				move_motor2(0, 0);
				Serial1.println("Motor 2 Stop");
				Serial1.println("   ");
				break;

			case '6': // Motor 2 Reverse
				move_motor2(255, 1);
				Serial1.println("Motor 2 Reverse");
				Serial1.println("   ");
				break;

			default:
				// turn all the connections off if an unmapped key is pressed:
				for (int thisPin = 2; thisPin < 11; thisPin++) {
					digitalWrite(thisPin, LOW);
				}
				break;
		}
			
	}
	
	else{
		
		if (curr_time- indicator_move_t0 > movement_period){
				move_motor1(0, 0);
				move_motor2(0, 0);
		}
		

		if (curr_time - indicator_led_t0 > 500){
			// turn off indicator 
			digitalWrite(indicator_led, LOW);
		}

	}

}

// move motor 1; forward is direction=0
void move_motor1(uint8_t speed, bool direction){
	
	analogWrite(motor1_speed, speed);
	// reverse
	if (direction){ 
		digitalWrite(motor1_dir1, LOW);
		digitalWrite(motor1_dir2, HIGH);
	}
	//forward
	else{
		digitalWrite(motor1_dir1, HIGH);
		digitalWrite(motor1_dir2, LOW);
		
	}	
}
// move motor 2; forward is direction=0
void move_motor2(uint8_t speed, bool direction){
	
	analogWrite(motor2_speed, speed);
	// reverse
	if (direction){ 
		digitalWrite(motor2_dir1, LOW);
		digitalWrite(motor2_dir2, HIGH);
	}
	//forward
	else{
		digitalWrite(motor2_dir1, HIGH);
		digitalWrite(motor2_dir2, LOW);
		
	}	
}
					

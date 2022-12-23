#include <xc.h>


void PWM_Init(char channel) {
    INTCON |= 0x30;
    PIR1 |= 0x02;
    PIR2 |= 0x01;
    PIE1 |=0x02;
    PIE2 |= 0x01;
    
    TRISC |= 1<<channel;
    
    
}

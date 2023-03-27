/*
 * File:   main.c
 * Author: User
 *
 * Created on 23 dicembre 2022, 10.15
 */

#pragma config FOSC = HS       // Oscillator Selection bits (XT oscillator)
#pragma config WDTE = OFF      // Watchdog Timer Enable bit (WDT disabled)
#pragma config PWRTE = ON      // Power-up Timer Enable bit (PWRT disabled)
#pragma config BOREN = ON      // Brown-out Reset Enable bit (BOR disabled)
#pragma config LVP = ON        // Low-Voltage (Single-Supply) In-Circuit Serial Programming Enable bit (RB3/PGM pin has PGM function; low-voltage programming enabled)
#pragma config CPD = OFF       // Data EEPROM Memory Code Protection bit (Data EEPROM code protection off)
#pragma config WRT = OFF       // Flash Program Memory Write Enable bits (Write protection off; all program memory may be written to by EECON control)
#pragma config CP = OFF        // Flash Program Memory Code Protection bit (Code protection off)

#define _XTAL_FREQ 20*1000000

#define TARGET_TEMP 40*0.488



#include <xc.h>

#include "./../00-Librerie.X/Adc.h"
#include "./../00-Librerie.X/lcd.h"
#include "./../00-Librerie.X/PID.h"

unsigned char duty, high_time, low_time;

void main(void) {

    Adc_Init();
    Lcd_Init();

    TRISC = 0x00;
    PORTC |= 0x20;
    INTCON = 0xA0;
    OPTION_REG = 0x84;

    int old_val;
    int err;

    duty = 45; // Desired duty cycle of the square wave in percentage
    TMR0 = high_time;

    while (1) {
        int val = 0;
        val = Adc_Read(2);
        if (val == old_val) continue;
        duty = pid_controller(TARGET_TEMP, val, 1, 0, 0, 0, 255);
        high_time = 255 - duty;
        low_time = duty;
        Lcd_Clear();
        old_val = val;
        //Lcd_Write_Int(val * 0.488);
        Lcd_Write_Int(duty);
        //if (!(val & 0x01)) Lcd_Write_String(".5");
    }


}

void __interrupt() ISR(void) {
    if (T0IF) { // Check for TMR0 overflow
        PORTCbits.RC5 = ~PORTCbits.RC5; // Invert RC0
        T0IF = 0; // Clear TMR0 overflow flag
        if (PORTCbits.RC5) TMR0 = high_time;
        else TMR0 = low_time;
    }

}

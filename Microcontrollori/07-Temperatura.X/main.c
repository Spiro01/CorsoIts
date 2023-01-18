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

void main(void) {
    // Set Timer0 to 8-bit mode
    T1CON = 0b10000001;
    // Set the period register to 249 (1kHz)
    PR2 = 249;
    // Set the compare register to 125 (50% duty cycle)
    CCPR1L = 125;
    // Enable the compare module
    CCP1CON = 0x09;
    // Start the Timer0
    TMR1 = 0;

    Adc_Init();
    Lcd_Init();
    TRISC = 0x00;
    PORTC |= 0x20;
    int old_val;
    char err;

    while (1) {
        int val = 0;
        val = Adc_Read(2);
        if (val == old_val) continue;
        pid_controller(TARGET_TEMP, val, 1, 0, 0.1, 0, 255);
        Lcd_Clear();
        old_val = val;
        Lcd_Write_Int(val * 0.488);
        if (!(val & 0x01)) Lcd_Write_String(".5");
    }


}

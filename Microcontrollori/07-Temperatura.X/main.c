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
#include <xc.h>
#include "Adc.h"
#include "lcd.h"
#include "pwm.h"

void main(void) {
    Adc_Init();
    Lcd_Init();
    TRISC = 0x00;
    PORTC |= 0x20;
    int old_val;
    while (1) {
        int val = 0;
        val = Adc_Read(2);
        if (val == old_val) continue;
        Lcd_Clear();
        old_val = val;
        Lcd_Write_Int(val * 0.488);
        if (!(val & 0x01)) Lcd_Write_String(".5");


    }
}

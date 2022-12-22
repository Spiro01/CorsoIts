/*
 * File:   main.c
 * Author: User
 *
 * Created on 16 dicembre 2022, 12.50
 */
// CONFIG
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
#include "7seg.h"

char DisplayOn;

void main(void) {
    Adc_Init();
    Lcd_Init();
    INTCON = 0xA0;
    OPTION_REG = 0x80;
    int val;

    while (1) {

        //val = Adc_Read(0x00);

        Display_Write(3, DisplayOn);

    }

}

void __interrupt() ISR(void) {

    if (INTCON & 0x04) {
        INTCON &= ~0x04;
        DisplayOn++;
        if (DisplayOn > 3) DisplayOn = 0;
    }
}

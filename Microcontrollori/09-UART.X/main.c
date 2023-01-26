/*
 * File:   main.c
 * Author: Spiro
 *
 * Created on 20 gennaio 2023, 20.55
 */
#pragma config FOSC = HS       // Oscillator Selection bits (XT oscillator)
#pragma config WDTE = OFF      // Watchdog Timer Enable bit (WDT disabled)
#pragma config PWRTE = ON      // Power-up Timer Enable bit (PWRT disabled)
#pragma config BOREN = ON      // Brown-out Reset Enable bit (BOR disabled)
#pragma config LVP = ON        // Low-Voltage (Single-Supply) In-Circuit Serial Programming Enable bit (RB3/PGM pin has PGM function; low-voltage programming enabled)
#pragma config CPD = OFF       // Data EEPROM Memory Code Protection bit (Data EEPROM code protection off)
#pragma config WRT = OFF       // Flash Program Memory Write Enable bits (Write protection off; all program memory may be written to by EECON control)
#pragma config CP = OFF        // Flash Program Memory Code Protection bit (Code protection off)


#include <xc.h>
#include "./../00-Librerie.X/UART.h"
#include "./../00-Librerie.X/lcd.h"

#define _XTAL_FREQ 20*1000000
unsigned char dato[16];
unsigned char received, counterFinished, receivedBytes;
char i;
unsigned int Counter;

void main(void) {
    INTCON |= 0xA0; // GIE = 1; T0IE = 1;
    OPTION_REG |= 0x85; // PS2 = 1; PS1 = 0; PS0 = 0;
    UART_init(115200);
    Lcd_Init();

    while (1) {
        if (received) {
            Lcd_Set_Cursor(1, receivedBytes++);
            if (receivedBytes > 16) {
                Lcd_Set_Cursor(1, 0);
                Lcd_Write_String("                ");
                Lcd_Set_Cursor(1, 0);
                receivedBytes = 1;
            }
            Lcd_Write_Char(dato[0]);
            i = 0;
            received = 0;
        }
        if (counterFinished) {
            Lcd_Set_Cursor(0, 0);
            Lcd_Write_Int(Counter);
            UART_TxInt(Counter);
            counterFinished = 0;
        }

    }
}

void __interrupt()ISR() {
    if (RCIF) {
        dato[i++] = RCREG;
        dato[i] = '\0';
        received = 1;
        RCIF = 0;
    }
    if (T0IF) {
        static unsigned int interruptCounter;
        interruptCounter++;
        T0IF = 0;
        if (interruptCounter > 625) {
            Counter++;
            interruptCounter = 0;
            counterFinished = 1;

        }
        TMR0 = 131;
    }
}
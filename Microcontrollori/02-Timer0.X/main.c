/*
 * File:   main.c
 * Author: Riccardo Spironelli
 *
 * Created on 24 ottobre 2022, 14.46
 */

#pragma config FOSC = HS        // Oscillator Selection bits (HS oscillator: High-speed crystal/resonator on RA6/OSC2/CLKOUT and RA7/OSC1/CLKIN)
#pragma config WDTE = OFF       // Watchdog Timer Enable bit (WDT disabled)
#pragma config PWRTE = ON       // Power-up Timer Enable bit (PWRT enabled)
#pragma config MCLRE = ON       // RA5/MCLR/VPP Pin Function Select bit (RA5/MCLR/VPP pin function is MCLR)
#pragma config BOREN = ON       // Brown-out Detect Enable bit (BOD enabled)
#pragma config LVP = OFF        // Low-Voltage Programming Enable bit (RB4/PGM pin has digital I/O function, HV on MCLR must be used for programming)
#pragma config CPD = OFF        // Data EE Memory Code Protection bit (Data memory code protection off)
#pragma config CP = OFF         // Flash Program Memory Code Protection bit (Code protection off)

// #pragma config statements should precede project file includes.
// Use project enums instead of #define for ON and OFF. 

#define _XTAL_FREQ 8000000
#define BIT 0x00
#include <xc.h>
#include "7seg.h"

char tempi[] = {15, 30, 60, 120}; //0.5,1,2,4 secondi
char InterruptCounter;
char TempoSelezionato;

void main(void) {
    TRISA = 0xFE;
    TRISB = 0x00;
    INTCON = 0xA0; // GIE = 1; T0IE = 1;
    OPTION_REG = 0x85; // PS2 = 1; PS1 = 1; PS0 = 0;

    char button, old_button;
    char selezione = 0x00;


    //sevenSegment(0x00,&PORTB);
    while (1) {
        button = PORTAbits.RA1;


        if (!button && old_button) {
            __delay_ms(200);
            if (!button && old_button) {
                selezione++;
                if (selezione >= 4)selezione = 0x00;
            } else {
                selezione--;
                if (selezione < 0)selezione = 0x03;
            }
        }

        old_button = button;

        TempoSelezionato = tempi[selezione];



        switch (selezione) {
            case 0:
                PORTB = sevenSegment(0, 0);
                PORTB = sevenSegment(5, 1);
                break;
            case 1:
                PORTB = sevenSegment(1, 0);
                break;
            case 2:
                PORTB = sevenSegment(2, 0);
                break;
            case 3:
                PORTB = sevenSegment(4, 0);
                break;
        }

    }
    return;
}

void __interrupt() ISR(void) {
    if (INTCONbits.T0IF) {
        INTCONbits

        InterruptCounter++;
        if (InterruptCounter >= TempoSelezionato) {
            if (PORTA & (1 << BIT)) PORTA &= ~(1 << BIT);
            else PORTA |= (1 << BIT);
            InterruptCounter = 0;

        }
        
    }

    return;
}

/*
 * File:   main.c
 * Author: User
 *
 * Created on 9 dicembre 2022, 10.01
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
#include "./../00-Librerie.X/lcd.h"
#include "./../00-Librerie.X/numpad.h"

void main(void) {


    Lcd_Init();

    Lcd_Set_Cursor(0, 3);
    Lcd_Write_String("Benvenuto");

    unsigned char operatore [2];
    char selettore, risultato;
    char read = 0xff;

    while (1) {
        read = Numpad_Read();
        if (read >= 0 && read <= 9) {
            if (!selettore) Lcd_Clear();
            if (selettore) Lcd_Set_Cursor(0, 2);
            operatore[selettore] = read;
            Lcd_Write_Int(operatore[selettore]);
        } else if (read == '*') {
            if (selettore < 1) {
                selettore++;
                Lcd_Write_Char('+');
            }
        } else if (read == '#') {
            if (selettore < 1) continue;
            risultato = operatore[0] + operatore[1];
            selettore = 0;
            Lcd_Write_Char('=');
            Lcd_Write_Int(risultato);
        }

    }
}

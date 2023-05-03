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
#include "./../00-Librerie.X/numpad.h"

#define _XTAL_FREQ 20*1000000
unsigned char dato[2];
unsigned char received;
char button, old_button;
int i;

void main(void) {
    UART_init(115200);
    Lcd_Init();
    char buffer[10];
    char numpadSelection = 0xff;

    const char Led_On_UART[] = {0x44, 0x72, 0x20, 0x2B, 0xFF};
    const char Clear_Display_UART[] = {0xA2, 0xB3, 0x01, 0x15, 0xFF};

    while (1) {

        numpadSelection = Numpad_Read();

        switch (numpadSelection) {
            case 1:
                UART_TxString("Hello World!");
                break;
            case 5:
                UART_TxChar('7');
                break;
            case 9:
                UART_TxString(Led_On_UART);
                break;
            default:
                break;
        }

        TRISB |= 0x02;
        button = PORTB & 0x02;
        if (button & !old_button) {
            UART_TxString(Clear_Display_UART);
        }
        old_button = button;

        if (received) {
            received = 0;
           
            Lcd_Write_String(dato);
            i = 0;
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

}

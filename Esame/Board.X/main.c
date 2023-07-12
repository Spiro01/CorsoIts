/*
 * File:   main.c
 * Author: User
 *
 * Created on 27 gennaio 2023, 9.05
 */
#pragma config FOSC = HS       // Oscillator Selection bits (XT oscillator)
#pragma config WDTE = OFF      // Watchdog Timer Enable bit (WDT disabled)
#pragma config PWRTE = ON      // Power-up Timer Enable bit (PWRT disabled)
#pragma config BOREN = ON      // Brown-out Reset Enable bit (BOR disabled)
#pragma config LVP = ON        // Low-Voltage (Single-Supply) In-Circuit Serial Programming Enable bit (RB3/PGM pin has PGM function; low-voltage programming enabled)
#pragma config CPD = OFF       // Data EEPROM Memory Code Protection bit (Data EEPROM code protection off)
#pragma config WRT = OFF       // Flash Program Memory Write Enable bits (Write protection off; all program memory may be written to by EECON control)
#pragma config CP = OFF        // Flash Program Memory Code Protection bit (Code protection off)

//DEFINE region
#define _XTAL_FREQ 20000000 

#define COLPORT PORTB
#define ROWPORT PORTD

#define X_1    RD3
#define X_2    RD2
#define X_3    RD1
#define X_4    RD0
#define Y_1    RB0
#define Y_2    RB1
#define Y_3    RB2

#define RS PORTEbits.RE2
#define EN PORTEbits.RE1
#define D4 PORTDbits.RD4
#define D5 PORTDbits.RD5
#define D6 PORTDbits.RD6
#define D7 PORTDbits.RD7

#define DEVICE_ADDRESS 1

//Include Region
#include <xc.h>
#include <string.h>


//Function declaration section
char Numpad_Read();

void Lcd_Port(char a);

void Lcd_Cmd(char a);

void Lcd_Init();

void Lcd_Clear();

void Lcd_Set_Cursor(char riga, char colonna);

void Lcd_Write_Char(char a);

void Lcd_Write_String(char *a);

void Lcd_Shift_Right();

void Lcd_Shift_Left();

char* Lcd_Write_Int(int val);

void UART_init(long int baudrate);

void UART_TxChar(char ch);

void UART_TxString(const char* str);

void UART_TxInt(int val);

typedef struct 
{
    uint8_t address;
    uint8_t message[16];
    uint8_t expire_hour;
    uint8_t expire_min;
} packet_t;

//global variables region
unsigned char SelectedSpeed, update_lcd, update_UART, update_speed, data_received, data_i;
char Uart_Data[32];

unsigned int MotorSpeed;

void main(void)
{

    Lcd_Init();
    UART_init(115200);
    packet_t packet;
    while (1)
    {
        
        if (data_i >= sizeof(packet_t))
        {
            memcpy(&packet,Uart_Data,sizeof(packet_t));
            if(packet.address == DEVICE_ADDRESS)
            {
                Lcd_Clear();
                Lcd_Write_String(packet.message);
                data_i = 0;
            }
        }
       
        
    }
}

void __interrupt()ISR()
{
    if (RCIF)
    {
        Uart_Data[data_i++] = RCREG;
       
        RCIF = 0;
    }
}


//Function region


void Lcd_Port(char a)
{

    if (a & 1) D4 = 1;
    else D4 = 0;

    if (a & 2) D5 = 1;
    else D5 = 0;

    if (a & 4) D6 = 1;
    else D6 = 0;

    if (a & 8) D7 = 1;
    else D7 = 0;
}

void Lcd_Cmd(char a)
{
    TRISD &= ~0xff;
    TRISE &= ~0x06;


    RS = 0; // Invio comando
    Lcd_Port(a);
    EN = 1;
    __delay_ms(4);
    EN = 0;
}

void Lcd_Init()
{
    TRISD &= ~0xff;
    TRISE &= ~0x06;
    Lcd_Port(0x00);
    __delay_ms(20);
    Lcd_Cmd(0x03);
    __delay_ms(5);
    Lcd_Cmd(0x03);
    __delay_ms(10);
    Lcd_Cmd(0x03);

    Lcd_Cmd(0x02); //LCD pilotato con 4 linee

    Lcd_Cmd(0x02); //comando
    Lcd_Cmd(0x08); //a due righe

    Lcd_Cmd(0x00); //accendo display
    Lcd_Cmd(0x0C); //e aspegni cursore  

    Lcd_Cmd(0x00);
    Lcd_Cmd(0x06); //incremento il cursore
}

void Lcd_Clear() // Cancella LCD
{
    Lcd_Cmd(0);
    Lcd_Cmd(1);
}

void Lcd_Set_Cursor(char riga, char colonna)
{
    char temp, z, y;
    if (riga == 0)
    {
        temp = 0x80 + colonna;
        z = temp >> 4; // z = 4 bit piu' significativi
        y = temp & 0x0F; // y = 4 bit meno significativi
        Lcd_Cmd(z);
        Lcd_Cmd(y);
    } else if (riga >= 1)
    {
        temp = 0xC0 + colonna;
        z = temp >> 4;
        y = temp & 0x0F;
        Lcd_Cmd(z);
        Lcd_Cmd(y);
    }
}

void Lcd_Write_Char(char a)
{
    char temp, y;
    temp = a & 0x0F;
    y = a & 0xF0;

    RS = 1; // Invio a

    Lcd_Port(y >> 4); //shift pin
    EN = 1;
    __delay_us(4);
    EN = 0;
    Lcd_Port(temp);
    EN = 1;
    __delay_us(4);
    EN = 0;
}

void Lcd_Write_String(char *a)
{
    int i;
    for (i = 0; a[i] != '\0'; i++)//
        Lcd_Write_Char(a[i]);
}

void Lcd_Shift_Right()
{
    Lcd_Cmd(0x01);
    Lcd_Cmd(0x0C);
}

void Lcd_Shift_Left()
{
    Lcd_Cmd(0x01);
    Lcd_Cmd(0x08);
}

char* Lcd_Write_Int(int val)
{

    int n = val;
    char buffer[50];
    int i = 0;
    char isNeg = n < 0;

    unsigned int n1 = isNeg ? -n : n;

    while (n1 != 0)
    {
        buffer[i++] = n1 % 10 + '0';
        n1 = n1 / 10;
    }

    if (isNeg)
        buffer[i++] = '-';

    buffer[i] = '\0';

    for (int t = 0; t < i / 2; t++)
    {
        buffer[t] ^= buffer[i - t - 1];
        buffer[i - t - 1] ^= buffer[t];
        buffer[t] ^= buffer[i - t - 1];
    }

    if (n == 0)
    {
        buffer[0] = '0';
        buffer[1] = '\0';
    }

    Lcd_Write_String(buffer);

}

void UART_init(long int baudrate)
{

    TXSTA |= 0x24;
    RCSTA = 0x90;
    SPBRG = (char) (_XTAL_FREQ / (long) (64UL * baudrate)) - 1;
    INTCON |= 0x80;
    INTCON |= 0x40;
    PIE1 |= 0x20;
}

void UART_TxChar(char ch)
{
    TRISC &= ~0x40;
    TRISC |= 0x80;
    while (!(PIR1 & 0x10));
    PIR1 &= ~0x10;
    TXREG = ch;

}

void UART_TxString(const char* str)
{
    unsigned char i = 0;
    while (str[i] != 0)
    {
        UART_TxChar(str[i]);
        i++;
    }
}

void UART_TxInt(int val)
{
    int n = val;
    char buffer[50];
    int i = 0;
    char isNeg = n < 0;

    unsigned int n1 = isNeg ? -n : n;

    while (n1 != 0)
    {
        buffer[i++] = n1 % 10 + '0';
        n1 = n1 / 10;
    }

    if (isNeg)
        buffer[i++] = '-';

    buffer[i] = '\0';

    for (int t = 0; t < i / 2; t++)
    {
        buffer[t] ^= buffer[i - t - 1];
        buffer[i - t - 1] ^= buffer[t];
        buffer[t] ^= buffer[i - t - 1];
    }

    if (n == 0)
    {
        buffer[0] = '0';
        buffer[1] = '\0';
    }
    UART_TxString(buffer);

}
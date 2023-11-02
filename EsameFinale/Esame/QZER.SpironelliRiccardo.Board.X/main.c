/*
 * File:   main.c
 * Author: Spiro
/*
 * File:   main.c
 * Author: Spironelli Riccardo
 *
 */

/*
 * PRAGMA REGION
 */
#pragma config FOSC = HS       // Oscillator Selection bits (XT oscillator)
#pragma config WDTE = OFF      // Watchdog Timer Enable bit (WDT disabled)
#pragma config PWRTE = ON      // Power-up Timer Enable bit (PWRT disabled)
#pragma config BOREN = ON      // Brown-out Reset Enable bit (BOR disabled)
#pragma config LVP = ON        // Low-Voltage (Single-Supply) In-Circuit Serial Programming Enable bit (RB3/PGM pin has PGM function; low-voltage programming enabled)
#pragma config CPD = OFF       // Data EEPROM Memory Code Protection bit (Data EEPROM code protection off)
#pragma config WRT = OFF       // Flash Program Memory Write Enable bits (Write protection off; all program memory may be written to by EECON control)
#pragma config CP = OFF        // Flash Program Memory Code Protection bit (Code protection off)

/*
 * DEFINE REGION
 */
#define _XTAL_FREQ 16000000 

#define RS PORTEbits.RE2
#define EN PORTEbits.RE1
#define D4 PORTDbits.RD4
#define D5 PORTDbits.RD5
#define D6 PORTDbits.RD6
#define D7 PORTDbits.RD7

/*
 * INCLUDE REGION
 */
#include <xc.h>
#include <string.h>
#include <pic16f877a.h>
#include <stdio.h>
#include <stdlib.h>



/*
 * FUNCTION PROTOTYPE REGION
 */

void Lcd_Port(char a);
void Lcd_Cmd(char a);
void Lcd_Init();
void Lcd_Clear();
void Lcd_Set_Cursor(char riga, char colonna);
void Lcd_Write_Char(char a);
void Lcd_Write_String(char *a);
void Lcd_Write_Int(int val);
void UART_init(long int baudrate);
void I2C_Init(const uint32_t feq_K);
void I2C_Begin();
void I2C_End();
void I2C_Write(uint8_t data);
uint8_t I2C_Read(uint8_t ack);
void _24AA04_Write(uint8_t* data, uint8_t position, uint16_t lenght);
void _24AA04_Read(uint8_t* data, uint8_t position, uint16_t lenght);

/*
 * TYPE DECLARATION REGION
 */
typedef struct
{
    uint8_t timeout;
} packet_t;

/*
 * GLOBAL VAR REGION
 */
static uint8_t uart_Data[32];
static uint8_t UART_data_index;
static uint8_t time_counter,update_lcd;
static uint8_t disaster_time;
static uint8_t led_toggle;

void main(void)
{
    INTCON |= 0xA0;
    OPTION_REG |= 0x86;
    TRISB = 0x00;
    PORTB |= 0xFF;
    
    Lcd_Init();
    UART_init(115200);
    I2C_Init(200);

    _24AA04_Read(&disaster_time, 0, sizeof (disaster_time));

    if (!disaster_time)
    {
        disaster_time = 0xFF;
    }
    packet_t packet;
    while (1)
    {
        if( update_lcd )
        {
            Lcd_Clear();
            Lcd_Write_Int(disaster_time - time_counter);
            update_lcd = 0;
            
        }
        
        
        if ((time_counter >= disaster_time - 30) && led_toggle)
        {
            PORTB = ~PORTB;
            led_toggle = 0;
        }

        if (UART_data_index >= sizeof (packet_t))
        {
            memcpy(&packet, uart_Data, sizeof (packet_t));
            disaster_time = packet.timeout;
            _24AA04_Write(&packet.timeout, 0, sizeof (packet.timeout));
            UART_data_index = 0;
            PORTB = 0xFF;
            time_counter = 0;
        }
        if (time_counter >= disaster_time)
        {
            Lcd_Clear();
            Lcd_Write_String("catastrofe");
            Lcd_Set_Cursor(1, 0);
            Lcd_Write_String("in atto");
            time_counter = 0;
            update_lcd = 0;
            INTCON &= ~0x20;
        }
    }
}

void __interrupt()ISR()
{
    if (T0IF)
    {
        TMR0 = 6;
        static uint8_t interrupt_counter;
        if (++interrupt_counter >= 125)
        {
            interrupt_counter = 0;
            time_counter++;
            led_toggle = 1;
            update_lcd = 1;
        }
        T0IF = 0;
    }
    if (RCIF)
    {
        uart_Data[UART_data_index++] = RCREG;
        RCIF = 0;
    }
}


/*
 * FUNCTION DEFINITION REGION
 */

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

    RS = 0;
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

    Lcd_Cmd(0x02);

    Lcd_Cmd(0x02); 
    Lcd_Cmd(0x08);

    Lcd_Cmd(0x00); 
    Lcd_Cmd(0x0C);

    Lcd_Cmd(0x00);
    Lcd_Cmd(0x06);
}

void Lcd_Clear()
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
        z = temp >> 4;
        y = temp & 0x0F; 
        Lcd_Cmd(z);
        Lcd_Cmd(y);
    }
    else if (riga >= 1)
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

    RS = 1;
    Lcd_Port(y >> 4);
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
    for (i = 0; a[i] != '\0'; i++)
        Lcd_Write_Char(a[i]);
}

void Lcd_Write_Int(int val)
{

    char buffer[16];
    itoa(buffer,val,10);
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

static void I2C_Hold()
{
    while ((SSPCON2 & 0x1F) || (SSPSTAT & 0x04));
}

void I2C_Begin()
{
    I2C_Hold();
    SEN = 1;
}

void I2C_End()
{
    I2C_Hold();
    PEN = 1;
}

void I2C_Init(const uint32_t feq_K)
{
    TRISC3 &= ~0x01;
    TRISC4 &= ~0x01;

    SSPCON = 0x28;
    SSPCON2 = 0x00;

    SSPADD = (char) (_XTAL_FREQ / (4 * feq_K * 100)) - 1;
    SSPSTAT = 0x00;
}

void I2C_Write(uint8_t data)
{
    I2C_Hold();
    SSPBUF = data;
}

uint8_t I2C_Read(uint8_t ack)
{
    uint8_t incoming;
    I2C_Hold();
    RCEN = 1;

    I2C_Hold();
    incoming = SSPBUF;

    I2C_Hold();
    ACKDT = (ack) ? 0 : 1;
    ACKEN = 1;

    return incoming;
}

void _24AA04_Write(uint8_t* data, uint8_t position, uint16_t lenght)
{
    I2C_Begin();
    I2C_Write(0xA0);
    I2C_Write(position);

    for (uint16_t i = 0; i < lenght; i++)
    {
        I2C_Write(data[i]);
    }

    I2C_End();
}

void _24AA04_Read(uint8_t* data, uint8_t position, uint16_t lenght)
{
    I2C_Begin();
    I2C_Write(0xA0);
    I2C_Write(position);
    I2C_Begin();
    I2C_Write(0xA1);
    uint16_t i;
    for (i = 0; i < lenght; i++)
    {
        data[i] = I2C_Read(1);
    }
    I2C_End();
}
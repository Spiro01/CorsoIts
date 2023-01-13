#include <xc.h>


#define RS PORTEbits.RE2
#define EN PORTEbits.RE1
#define D4 PORTDbits.RD4
#define D5 PORTDbits.RD5
#define D6 PORTDbits.RD6
#define D7 PORTDbits.RD7

#define _XTAL_FREQ 20*1000000
//LCD Header per indirizzamento a 4 bit 

void Lcd_Port(char a) {

    if (a & 1) D4 = 1;
    else D4 = 0;

    if (a & 2) D5 = 1;
    else D5 = 0;

    if (a & 4) D6 = 1;
    else D6 = 0;

    if (a & 8) D7 = 1;
    else D7 = 0;
}

void Lcd_Cmd(char a) {
    TRISD &= ~0xff;
    TRISE &= ~0x06;


    RS = 0; // Invio comando
    Lcd_Port(a);
    EN = 1;
    __delay_ms(4);
    EN = 0;
}

void Lcd_Init() {
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
    if (riga == 0) {
        temp = 0x80 + colonna;
        z = temp >> 4; // z = 4 bit piu' significativi
        y = temp & 0x0F; // y = 4 bit meno significativi
        Lcd_Cmd(z);
        Lcd_Cmd(y);
    } else if (riga >= 1) {
        temp = 0xC0 + colonna;
        z = temp >> 4;
        y = temp & 0x0F;
        Lcd_Cmd(z);
        Lcd_Cmd(y);
    }
}

void Lcd_Write_Char(char a) {
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

void Lcd_Write_String(char *a) {
    int i;
    for (i = 0; a[i] != '\0'; i++)//
        Lcd_Write_Char(a[i]);
}

void Lcd_Shift_Right() {
    Lcd_Cmd(0x01);
    Lcd_Cmd(0x0C);
}

void Lcd_Shift_Left() {
    Lcd_Cmd(0x01);
    Lcd_Cmd(0x08);
}

char* Lcd_Write_Int(int val) {

    int n = val;
    char buffer[50];
    int i = 0;
    char isNeg = n < 0;

    unsigned int n1 = isNeg ? -n : n;

    while (n1 != 0) {
        buffer[i++] = n1 % 10 + '0';
        n1 = n1 / 10;
    }

    if (isNeg)
        buffer[i++] = '-';

    buffer[i] = '\0';

    for (int t = 0; t < i / 2; t++) {
        buffer[t] ^= buffer[i - t - 1];
        buffer[i - t - 1] ^= buffer[t];
        buffer[t] ^= buffer[i - t - 1];
    }

    if (n == 0) {
        buffer[0] = '0';
        buffer[1] = '\0';
    }

    Lcd_Write_String(buffer);

}
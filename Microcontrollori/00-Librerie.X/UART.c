#include <xc.h>
#define _XTAL_FREQ 20*1000000

void UART_init(long int baudrate) {

    TXSTA |= 0x24;
    RCSTA = 0x90;
    SPBRG = (char) (_XTAL_FREQ / (long) (64UL * baudrate)) - 1;
    INTCON |= 0x80;
    INTCON |= 0x40;
    PIE1 |= 0x20;
}

void UART_TxChar(char ch) {
    TRISC &= ~0x40;
    TRISC |= 0x80;
    while (!(PIR1 & 0x10));
    PIR1 &= ~0x10;
    TXREG = ch;

}

void UART_TxString(const char* str) {
    unsigned char i = 0;
    while (str[i] != 0) {
        UART_TxChar(str[i]);
        i++;
    }
}

void UART_TxInt(int val) {
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
    UART_TxString(buffer);

}


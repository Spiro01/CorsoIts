#define _XTAL_FREQ 20 * 1000000

void UART_Init(long int baud) {
    TRISC &= ~0x40;
    TRISC |= 0x80;
    TXSTA |= 0x24;

    RCSTA |= 0x80;
    RCSTA |= 0x10;

    SPBRG = (char) (_XTAL_FREQ / (long) (64UL * baud)) - 1;

    INTCON |= 0x80;
    INTCON |= 0x40;
    PIE1 |= 0x20;

}

void UART_TxChar(char c) {
    while (!(PIR1 & 0x10));

    PIR1 &= ~0x10;
    TXREG = c;
}

void UART_TxString(const char* str) {
    unsigned char i = 0;
    while (str[i] != 0) {

    }
}

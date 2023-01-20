
void UART_init(long int baudrate) {
    TRISC &= ~0x40;
    TRISC |= 0x80;
    TXSTA |= 0x24;
    RCSTA = 0x90;
    SPBRG = (char) (_XTAL_FREQ / (long) (64UL * baudrate)) - 1;
    INTCON |= 0x80;
    INTCON |= 0x40;
    PIE1 |= 0x20;
}

void UART_TxChar(char ch) {

    while (!(PIR1 & 0x10)) {
        PIR1 &= ~0x10;
        TXREG = ch;
    }
}

void UART_TxString(const char* str) {
    unsigned char i = 0;
    while (str[i] != 0) {
        UART_TxChar(str[i]);
        i++;
    }
}
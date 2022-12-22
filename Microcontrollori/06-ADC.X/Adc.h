


void Adc_Init(){
    ADCON0 = 0x81;
    ADCON1 = 0x80;
}

int Adc_Read(char channel){
    TRISA |= 0x01;
   
    ADCON0 &= ~0x38;

    ADCON0 |= channel << 3;
   
    __delay_ms(1);
   
    ADCON0 |= 1 << 2;  
   
    while(ADCON0 & 0x04) { }
   
    ADCON0 |= 0x04;
   
    return ADRESL + (ADRESH << 8);
}

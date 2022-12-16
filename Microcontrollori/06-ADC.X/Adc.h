/* 
 * File:   Adc.h
 * Author: User
 *
 * Created on 16 dicembre 2022, 12.49
 */

#include <xc.h>

void ADC_Init() {

    // FOSC/32; canale AN0=> RA0; GO_DONE stop; ADON abilitato;  
    ADCON0 = 0B10000101;
    __delay_ms(5);
    
    // ADFM giust. sinistra; ADPREF VDD; ADNREF VSS;
    ADCON1 = 0B10000000;
    __delay_ms(5);


    ADRESL = 0x00;


    ADRESH = 0x00;

}

void ADC_Conv() {
 
    ADCON0bits.GO_nDONE = 1; // Inizio conversione
    
    while (ADCON0bits.GO_nDONE) {  // Attendi fine conversione
    }
   
}
int ADC_ConvTemp() {
    

    ADCON0 = 0B10000111;
   // ADCON0bits.GO_nDONE = 1; // Inizio conversione
    
    while (ADCON0bits.GO_nDONE) {  // Attendi fine conversione
        return((ADRESH<<8) + ADRESL);
    }
}

   void ADC_ConvTrim() {
       
    ADCON0 = 0B10000011;
    //ADCON0bits.GO_nDONE = 1; // Inizio conversione
    
    while (ADCON0bits.GO_nDONE) {  // Attendi fine conversione
    }
}
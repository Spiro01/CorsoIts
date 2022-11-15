/* 
 * File:   button.h
 * Author: Spiro
 *
 * Created on 15 novembre 2022, 15.46
 */


void readButton(){
    static char button, old_button;
    
if (!button && old_button) {
            __delay_ms(200);
            if (!button && old_button) {
                TempoSelezionato++;
                if (TempoSelezionato >= 4)TempoSelezionato = 0x00;
            } else {
                TempoSelezionato--;
                if (TempoSelezionato < 0)TempoSelezionato = 0x03;
            }
        }

        old_button = button;


    }
}


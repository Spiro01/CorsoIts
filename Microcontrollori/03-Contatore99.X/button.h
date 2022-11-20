/* 
 * File:   button.h
 * Author: Spiro
 *
 * Created on 15 novembre 2022, 15.46
 */


void checkButton(char* PORT,char button, void *function()) {
    static char button, old_button;
    button = &PORT & button;
    if (!button && old_button) {
        __delay_ms(20);
        button = PORT & button;
        if (!button && old_button) {
            function();
        }
    }

    old_button = button;


}



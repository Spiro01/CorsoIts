#define COLPORT PORTB
#define ROWPORT PORTD

#include <xc.h>

#define X_1    RD3
#define X_2    RD2
#define X_3    RD1
#define X_4    RD0
#define Y_1    RB0
#define Y_2    RB1
#define Y_3    RB2



const unsigned char keypad [] = {'*', 7, 4, 1, 0, 8, 5, 2, '#', 9, 6, 3};

char Numpad_Read() {
    TRISD |= 0x0f;
    TRISB &= ~0x07;
    char colScan, rowScan, currentKeyVal, currentKey;
    static char oldKeyVal;
    for (colScan = 0; colScan < 3; colScan++) {
        COLPORT |= 0x07;
        COLPORT &= ~(1 << colScan);
        __delay_ms(15);
        for (rowScan = 0; rowScan < 4; rowScan++) {
            currentKeyVal = (ROWPORT & (1 << rowScan));

            if (!currentKeyVal && oldKeyVal) {
                currentKey = keypad[rowScan + (4 * colScan)];
                oldKeyVal = 0;
                while (!currentKeyVal) {
                    currentKeyVal = (ROWPORT & (1 << rowScan));
                    __delay_ms(20);
                }
                return currentKey;
            }

            oldKeyVal = 1;
        }
    }
    return 0xff;

}


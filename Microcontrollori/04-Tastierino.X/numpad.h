#define COLPORT PORTB
#define ROWPORT PORTD

#include <xc.h>

void NumPad_Init() {
    TRISD = 0x0f;
    TRISB = 0x00;
}

const unsigned char keypad [] = {'*', 7, 4, 1, 0, 8, 5, 2, '#', 9, 6, 3};

char Numpad_Read() {
   static char colScan, rowScan, currentKeyVal, currentKey, oldKeyVal;

    for (colScan = 0; colScan < 3; colScan++) {
        COLPORT |= 0x07;
        COLPORT &= ~(1 << colScan);

        for (rowScan = 0; rowScan < 4; rowScan++) {
            currentKeyVal = (ROWPORT >> rowScan) & 1;
            if (!currentKeyVal) {
                currentKey = keypad[rowScan + (4 * colScan)];
                return currentKey;
            }
            

        }


    }

    return 0xff;

}
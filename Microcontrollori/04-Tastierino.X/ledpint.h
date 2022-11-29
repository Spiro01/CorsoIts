#define DECPORT PORTD
#define UNITPORT PORTB

void Print_Led(char value) {
    DECPORT &= 0x0f;
    UNITPORT &= 0x07;
    
    __debug_break();
    DECPORT |= (value/10) <<4;
    UNITPORT |= (value%10) <<4;
    
}
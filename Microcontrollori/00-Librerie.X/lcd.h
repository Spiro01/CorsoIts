
void Lcd_Port(char a);

void Lcd_Cmd(char a);

void Lcd_Init();

void Lcd_Clear();

void Lcd_Set_Cursor(char riga, char colonna);

void Lcd_Write_Char(char a);

void Lcd_Write_String(char *a);

void Lcd_Shift_Right();

void Lcd_Shift_Left();

char* Lcd_Write_Int(int val);
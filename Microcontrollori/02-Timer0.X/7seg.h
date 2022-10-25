/* 
 * File:   7seg.h
 * Author: Spiro
 *
 * Created on 25 ottobre 2022, 15.31
 */

char sevenSegment(char value, char display) {

    if (value < 0xFF) {
        char A = (value >> 3) & 1;
        char B = (value >> 2) & 1;
        char C = (value >> 1) & 1;
        char D = (value >> 0) & 1;

        char result = 0;
        result |= (A || C || B && D || !B&&!D) << 2; //a
        result |= (!B || !C&&!D || C && D) << 3; //b
        result |= (B || !C || D) << 5; //c
        result |= (!B&&!D || C&&!D || B&&!C && D || !B && C || A) << 6; //d
        result |= (!B && !D || C &&!D) << 7; //e
        result |= (A || !C&&!D || B && !C || B&&!D) << 1; //f
        result |= (A || B&&!C || !B && C || C&&!D) << 0; //g
        result |= display << 4; //switch
        return result;

    }
}


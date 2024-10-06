#include <iostream>

int main()
{
    // (A + B) * C + D
    __int8 A[8] = { 1,2,1,2,1,2,1,2 };
    __int8 B[8] = { 3,4,3,4,3,4,3,4 };
    __int8 C[8] = { 3,3,3,3,3,3,3,3 };
    __int16 D[8] = { 10,5,10,5,10,5,10,5 };

    __int16 F[8] = { 0 };

    __asm
    {
        movq mm0, [A]
        movq mm1, [B]
        movq mm2, [C]
        movq mm3, [D]

        punpcklbw mm4, mm0
        punpckhbw mm5, mm0

        pxor mm0, mm0

        punpcklbw mm6, mm1
        punpckhbw mm7, mm1

        pxor mm1, mm1

        punpcklbw mm0, mm2
        punpckhbw mm1, mm2

        psrlw mm0, 8
        psrlw mm1, 8

        pxor mm2, mm2
        movq mm2, [D + 8]

        paddsw mm4, mm6
        pxor mm6, mm6
        movq mm6, mm4

        pmullw mm4, mm0
        pmulhw mm6, mm0

        psllw mm6, 12
        psrlw mm4, 4

        paddsw mm6, mm4
        psrlw mm6, 4

        paddsw mm6, mm3

        movq[F], mm6

        paddsw mm5, mm7
        pxor mm7, mm7
        movq mm7, mm5

        pmullw mm7, mm1
        pmulhw mm5, mm1

        psllw mm5, 12
        psrlw mm7, 4

        paddsw mm7, mm5
        psrlw mm7, 4

        paddsw mm7, mm2

        movq[F + 8], mm7
    }

    std::cout << "Result: ";

    for (int i = 0; i < 8; ++i)
    {
        std::cout << F[i] << " ";
    }
    std::cout << std::endl;
}
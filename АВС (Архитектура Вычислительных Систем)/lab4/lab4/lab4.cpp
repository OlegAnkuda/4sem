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
        movupd xmm0, [A]
        movupd xmm1, [B]
        movupd xmm2, [C]
        movupd xmm3, [D]
        xorpd xmm4, xmm4

        pcmpgtb xmm4, xmm0
        punpcklbw xmm0, xmm4

        pcmpgtb xmm4, xmm1
        punpcklbw xmm1, xmm4

        pcmpgtb xmm4, xmm2
        punpcklbw xmm2, xmm4

        paddd xmm0, xmm1
        pmullw xmm0, xmm2
        paddd xmm0, xmm3

        movupd[F], xmm0
    }
    std::cout << "Result: ";
    for (int i = 0; i < 8; ++i)
    {
        std::cout << F[i] << " ";
    }
}
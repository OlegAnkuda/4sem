#include <stdio.h>
#include <math.h>
#include <time.h>
#include <stdlib.h>

extern float Y1(float x);
extern float S(float x, int n);
extern float Y2(float x);

int main() 
{
    float a, b, h, epsilon, time = 0.0f;
    int n = 0;


    printf("Enter a, b, h, epsilon: ");
    scanf("%f %f %f %f", &a, &b, &h, &epsilon);
    float g=a;
    float k=g*Y1(g)-Y2(g);
    printf("%f,  %f,   %f    \n",k, Y1(g),Y2(g));
    for (float x = a; x <= b; x += h)
    {
        clock_t start = clock();
        float y=x*Y1(x)-Y2(x);


        if(epsilon >= abs(y - S(x, n))) ++n;

        else break;



        clock_t end = clock();
        float elapsed_time = (float)(end - start)/ CLOCKS_PER_SEC;
        time += elapsed_time;
        printf("x = %f,  Y = %f, S = %f, n = %d, time = %f\n", x, y, S(x,n), n, elapsed_time);
    }

    printf("time: %f\n", time);
}

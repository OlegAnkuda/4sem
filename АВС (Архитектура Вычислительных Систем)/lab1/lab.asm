default rel

global Y1: function
global Y2: function
global S: function

extern atan
extern log
extern sqrt
extern pow
extern sin

SECTION .text   align=1 exec                            

Y1:                      ; Метка начала функции Y
    endbr64             ; Завершение инструкции 64-битной ветви для совместимости
    push    rbp         ; Сохранение текущего значения регистра rbp на стеке
    mov     rbp, rsp    ; Установка rbp равным rsp (указатель на текущий стековый фрейм)
    sub     rsp, 16     ; Выделение места на стеке под локальные переменные (16 байт)

    movss   dword [rbp-4], xmm0  ; Помещение значения xmm0 (переданное в качестве аргумента) в память по адресу rbp-4
    fld     dword [rbp-4]        ; Загрузка значения из памяти [rbp-4] в регистр FPU (Floating Point Unit)
    fstp    qword [rbp-10H]      ; Выгрузка значения из стека FPU в память по адресу rbp-10H (в формате qword)

    movsd   xmm0, qword [rbp-10H]    ; Помещение значения из памяти [rbp-10H] в xmm0
    call    atan             ; Вызов функции sin с аргументом из xmm0

    movsd   qword [rbp-10H], xmm0    ; Сохранение результата sin обратно в память по адресу rbp-10H
    fld     qword [rbp-10H]          ; Загрузка значения из памяти [rbp-10H] в регистр FPU
    fstp    dword [rbp-10H]          ; Выгрузка значения из стека FPU в память по адресу rbp-10H (в формате dword)

    movss   xmm0, dword [rbp-10H]    ; Помещение значения из памяти [rbp-10H] обратно в xmm0
    leave                           ; Эквивалент комбинации mov rsp, rbp; pop rbp (восстановление стека)
    ret                             ; Возврат из функции

Y2:      ; Function begins
        endbr64              ; Инструкция для завершения 64-битной ветви для совместимости
        push    rbp          ; Сохранение текущего значения регистра rbp на стеке
        mov     rbp, rsp     ; Установка rbp равным rsp (указатель на текущий стековый фрейм)
        sub     rsp, 16      ; Выделение места на стеке под локальные переменные (16 байт)

        movss   dword [rbp-4], xmm0   ; Помещение значения xmm0 (аргумент функции) в память по адресу rbp-4
        fld     dword [rbp-4]         ; Загрузка значения из памяти [rbp-4] в стек FPU
        fstp    qword [rbp-10H]       ; Выгрузка значения из стека FPU в память по адресу rbp-10H
        
        ; Вычисление x^2 (квадрата аргумента x)
        movsd   xmm0, qword [rbp-10H] ; Помещение значения из памяти [rbp-10H] обратно в xmm0
        mulsd   xmm0, xmm0            ; Умножение xmm0 на само себя (x^2)
        ; Вычисление sqrt(1 + x^2)
        sqrtsd  xmm0, xmm0            ; Вычисление квадратного корня из (1 + x^2)
        
        ; Вызов функции log для вычисления ln(sqrt(1 + x^2))
        movsd   xmm0, xmm0            ; Помещение значения sqrt(1 + x^2) в xmm0 (аргумент для log)
        call    log                   ; Вызов функции log для вычисления натурального логарифма
        
        movsd   qword [rbp-10H], xmm0 ; Сохранение результата log обратно в память по адресу rbp-10H
        fld     qword [rbp-10H]       ; Загрузка значения из памяти [rbp-10H] в стек FPU
        fstp    dword [rbp-10H]       ; Выгрузка значения из стека FPU в память по адресу rbp-10H
        
        movss   xmm0, dword [rbp-10H] ; Помещение значения из памяти [rbp-10H] обратно в xmm0
        leave                          ; Эквивалент комбинации mov rsp, rbp; pop rbp (восстановление стека)
        ret                            ; Возврат из функции



factorial:                  ; Метка начала функции factorial
    endbr64                 ; Завершение инструкции 64-битной ветви для совместимости
    push    rbp             ; Сохранение текущего значения регистра rbp на стеке
    mov     rbp, rsp        ; Установка rbp равным rsp (указатель на текущий стековый фрейм)

    mov     dword [rbp-14], edi    ; Сохранение значения аргумента edi в памяти по адресу rbp-14
    mov     dword [rbp-8], 1       ; Инициализация переменной [rbp-8] со значением 1
    mov     dword [rbp-4], 1       ; Инициализация переменной [rbp-4] со значением 1
    jmp     fact_loop               ; Переход к метке fact_loop для начала цикла вычисления факториала

fact_mul:               ; Метка начала подпрограммы fact_mul
    mov     eax, dword [rbp-8]     ; Загрузка значения [rbp-8] в eax
    imul    eax, dword [rbp-4]      ; Умножение eax на [rbp-4]
    mov     dword [rbp-8], eax      ; Сохранение результата умножения обратно в [rbp-8]
    add     dword [rbp-4], 1         ; Увеличение [rbp-4] на 1
    jmp     fact_loop               ; Переход к метке fact_loop для продолжения цикла

fact_loop:               ; Метка начала цикла вычисления факториала
    mov     eax, dword [rbp-4]     ; Загрузка значения [rbp-4] в eax
    cmp     eax, dword [rbp-14]     ; Сравнение [rbp-4] с аргументом (значение edi)
    jle     fact_mul                ; Переход к fact_mul, если [rbp-4] <= edi
    mov     eax, dword [rbp-8]      ; Загрузка значения [rbp-8] (результата факториала) в eax
    pop     rbp                     ; Восстановление значения регистра rbp
    ret                             ; Возврат из функции

; Конец функции factorial

S:
    endbr64                     ; Завершение 64-битной ветви для совместимости
    push    rbp                 ; Сохранение текущего значения регистра rbp на стеке
    mov     rbp, rsp            ; Установка rbp равным rsp (указатель на текущий стековый фрейм)
    sub     rsp, 48             ; Выделение места на стеке под локальные переменные (48 байт)

    movss   dword [rbp-14H], xmm0   ; Помещение значения xmm0 (аргумента x) в память по адресу rbp-14H
    mov     dword [rbp-18H], edi    ; Сохранение значения аргумента edi (n) в памяти по адресу rbp-18H
    fldz                            ; Загрузка нуля в регистр FPU (стек FPU)
    fstp    dword [rbp-8H]          ; Выгрузка значения из стека FPU в память по адресу rbp-8H

    mov     dword [rbp-4H], 1       ; Инициализация переменной k = 1
    jmp     s_loop                  ; Переход к метке s_loop для начала цикла вычисления

s_calc:
    mov     eax, dword [rbp-4H]     ; Загрузка значения k в eax
    add     eax, eax                ; Умножение k на 2 (2k)
    sub     eax, 1                  ; Отнимаение 1 от результата (2k - 1)
    mov     dword [rbp-20H], eax    ; Сохранение результата в памяти [rbp-20H]

    mov     eax, dword[rbp-4H]      ; Загрузка значения k в eax
    add     eax, eax                ; Умножение k на 2 (2k)
    mov     dword [rbp-28H], eax    ; Сохранение результата в памяти [rbp-28H]

    fild    dword [rbp-20H]
    fild    dword [rbp-28H]
    fmulp st1,st0
    fstp    qword [rbp-20H]         ; 2k(2k-1)


    fild    dword [rbp-28H]          ; Загрузка значения (2k в стек FPU
    fstp    qword [rbp-28H]          ; Выгрузка результата из стека FPU в память [rbp-28H]


    fld     dword [rbp-14H]          ; Загрузка значения x в стек FPU

    fstp    qword [rbp-30H]          ; Выгрузка результата из стека FPU в память [rbp-30H]

    movsd   xmm0, qword [rbp-30H]    ; Помещение  значения из памяти [rbp-30H] в xmm0
    movsd   xmm1, qword [rbp-28H]    ; Помещение значения из памяти [rbp-28H] в xmm1
    call    pow                      ; Вызов функции pow для вычисления x^(2k)
    movsd   qword [rbp-28H], xmm0    ; Сохранение результата pow обратно в память [rbp-28H]

    fld     qword [rbp-20H]
    fld     qword [rbp-28H]
    fdivrp  st1,st0
    fstp    qword [rbp-30H]

    fld     qword [rbp-30H]
    fld     dword [rbp-8H]           ; Загрузка текущей суммы в стек FPU
    faddp   st1, st0                 ; Сложение суммы с новым результатом
    fstp    dword [rbp-8H]           ; Выгрузка результата из стека FPU в память [rbp-8H]

    add     dword [rbp-4H], 1        ; Инкремент переменной k
    jmp     s_loop                   ; Переход к метке s_loop для следующей итерации цикла

s_loop:
    mov     eax, dword [rbp-4H]      ; Загрузка значения k в eax
    cmp     eax, dword [rbp-18H]      ; Сравнение k с n
    jle     s_calc                    ; Переход к s_calc, если k <= n

    fld     dword [rbp-8H]            ; Загрузка окончательной суммы в стек FPU
    fstp    dword [rbp-20H]           ; Выгрузка суммы из стека FPU в память [rbp-20H]
    movss   xmm0, dword [rbp-20H]     ; Помещение значения из памяти [rbp-20H] в xmm0
    leave                              ; Эквивалент комбинации mov rsp, rbp; pop rbp (восстановление стека)
    ret                                ; Возврат из функции

one:    dq      0x3FF0000000000000   ; Константа 1.0 в формате double (64 бита)

SECTION .data   align=1 noexec                          ; section number 2, data


SECTION .bss    align=1 noexec                          ; section number 3, bss


SECTION .rodata align=8 noexec                          ; section number 4, const

end:                                                 
        dq 0BFF0000000000000H                           

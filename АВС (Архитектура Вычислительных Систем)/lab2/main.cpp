#include <ctime>               // Подключение заголовочного файла для работы с временем
#include <iostream>            // Подключение заголовочного файла для ввода-вывода в консоль
#include <stdio.h>             // Подключение стандартного заголовочного файла ввода-вывода
#include <stdlib.h>            // Подключение стандартных заголовочных файлов, таких как выделение памяти
#include <CL/cl.h>             // Подключение заголовочного файла для OpenCL
#include <cmath>               // Подключение заголовочного файла для математических функций

void checkError(cl_int error, const char *message) {
    if (error != CL_SUCCESS) {
        printf("%s: Error %d\n", message, error);  // Вывод сообщения об ошибке с указанием кода ошибки
        exit(1);                                    // Выход из программы в случае ошибки
    }
}

int main() {
    cl_int error;   // Объявление переменной для кодов ошибок OpenCL

    float x;        // Объявление переменной для ввода значения x
    int n;          // Объявление переменной для ввода значения n
    printf("Input x, n\n");  // Вывод сообщения о вводе значений x, n
    std::cin >> x >> n;      // Ввод значений x, n
    printf("You inputed x, n: %f %i\n", x, n);  // Вывод введенных значений x, n

    // Получение количества доступных платформ OpenCL
    cl_uint numPlatforms;
    cl_uint retnumDevices;
    error = clGetPlatformIDs(0, NULL, &numPlatforms);  // Получение количества платформ
    checkError(error, "Failed to get platform count"); // Проверка на ошибку при получении количества платформ

    if (numPlatforms == 0) {
        printf("No OpenCL platforms found\n");  // Вывод сообщения об отсутствии платформ OpenCL
        return 0;
    }

    // Получение списка платформ
    cl_platform_id *platforms = (cl_platform_id *)malloc(numPlatforms * sizeof(cl_platform_id));
    error = clGetPlatformIDs(numPlatforms, platforms, NULL);  // Получение идентификаторов платформ
    checkError(error, "Failed to get platform IDs");           // Проверка на ошибку при получении идентификаторов платформ

    printf("Number of OpenCL platforms: %u\n", numPlatforms);  // Вывод количества доступных платформ OpenCL

    // Для каждой платформы выводим информацию о доступных устройствах
    for (cl_uint i = 0; i < numPlatforms; ++i) {

        cl_device_id device_id = NULL;

        printf("Platform %u:\n", i + 1);  // Вывод номера текущей платформы

        // Получение количества доступных устройств для текущей платформы
        cl_uint numDevices;
        error = clGetDeviceIDs(platforms[i], CL_DEVICE_TYPE_ALL, 0, NULL, &numDevices);
        if (error == CL_DEVICE_NOT_FOUND) {
            printf("No devices found for this platform\n");
            continue;
        }
        checkError(error, "Failed to get device count");

        // Получение списка доступных устройств
        cl_device_id *devices = (cl_device_id *)malloc(numDevices * sizeof(cl_device_id));
        error = clGetDeviceIDs(platforms[i], CL_DEVICE_TYPE_ALL, numDevices, devices, NULL);
        checkError(error, "Failed to get device IDs");

        // Вывод информации о каждом устройстве
        for (cl_uint j = 0; j < numDevices; ++j) {
            printf("  Device %u:\n", j + 1);  // Вывод номера текущего устройства

            char deviceName[128];
            error = clGetDeviceInfo(devices[j], CL_DEVICE_NAME, sizeof(deviceName), deviceName, NULL);
            checkError(error, "Failed to get device name");
            printf("    Name: %s\n", deviceName);  // Вывод имени текущего устройства

            cl_device_type deviceType;
            error = clGetDeviceInfo(devices[j], CL_DEVICE_TYPE, sizeof(deviceType), &deviceType, NULL);
            checkError(error, "Failed to get device type");
            if (deviceType & CL_DEVICE_TYPE_CPU)
                printf("    Type: CPU\n");
            if (deviceType & CL_DEVICE_TYPE_GPU)
                printf("    Type: GPU\n");
            if (deviceType & CL_DEVICE_TYPE_ACCELERATOR)
                printf("    Type: Accelerator\n");

            // Другие характеристики устройства также могут быть выведены здесь
        }

        // Получение идентификатора одного из доступных устройств для текущей платформы
        error = clGetDeviceIDs(platforms[i], CL_DEVICE_TYPE_ALL, 1, &device_id, &retnumDevices);

        // Создание контекста OpenCL для текущего устройства
        cl_context context = clCreateContext(NULL, 1, &device_id, NULL, NULL, &error);
        if (!context) {
            printf("Failed to create OpenCL context\n");
            return 1;
        }

        // Создание очереди команд для текущего устройства OpenCL
        cl_command_queue command_queue = clCreateCommandQueue(context, device_id, 0, &error);
        if (!command_queue) {
            printf("Failed to create command queue\n");
            clReleaseContext(context);
            return 1;
        }

        // Создание буфера для хранения результатов на устройстве
        cl_mem results_mem_obj = clCreateBuffer(context, CL_MEM_WRITE_ONLY, n * sizeof(float), NULL, &error);
        if (!results_mem_obj) {
            printf("Failed to create buffer\n");
            clReleaseCommandQueue(command_queue);
            clReleaseContext(context);
            return 1;
        }

        // Компиляция ядра OpenCL
        const char *kernelSource =
            "__kernel void computeTerm(__global float* results, float x) {             \n"
            "    int id = get_global_id(0);                                    \n"
            "    float term = pow(x, 4*id+1)/ (4*id + 1); \n"
            "    results[id] = term; \n"
            "}                                                                  \n";

        cl_program program = clCreateProgramWithSource(context, 1, &kernelSource, NULL, &error);
        if (!program) {
            printf("Failed to create program\n");
            clReleaseMemObject(results_mem_obj);
            clReleaseCommandQueue(command_queue);
            clReleaseContext(context);
            return 1;
        }

        // Компиляция и сборка программы OpenCL
        error = clBuildProgram(program, 1, &device_id, NULL, NULL, NULL);
        if (error != CL_SUCCESS) {
            printf("Failed to build program\n");
            char buildLog[4096];
            clGetProgramBuildInfo(program, device_id, CL_PROGRAM_BUILD_LOG, sizeof(buildLog), buildLog, NULL);
            printf("Build log:\n%s\n", buildLog);
            clReleaseProgram(program);
            clReleaseMemObject(results_mem_obj);
            clReleaseCommandQueue(command_queue);
            clReleaseContext(context);
            return 1;
        }

        // Создание ядра OpenCL
        cl_kernel kernel = clCreateKernel(program, "computeTerm", &error);
        if (!kernel) {
            printf("Failed to create kernel\n");
            clReleaseProgram(program);
            clReleaseMemObject(results_mem_obj);
            clReleaseCommandQueue(command_queue);
            clReleaseContext(context);
            return 1;
        }

        // Установка аргументов для ядра OpenCL
        error = clSetKernelArg(kernel, 0, sizeof(cl_mem), (void *)&results_mem_obj);
        error |= clSetKernelArg(kernel, 1, sizeof(float), (void *)&x); // Передача x как аргумента в ядро
        if (error != CL_SUCCESS) {
            printf("Failed to set kernel arguments\n");
            clReleaseKernel(kernel);
            clReleaseProgram(program);
            clReleaseMemObject(results_mem_obj);
            clReleaseCommandQueue(command_queue);
            clReleaseContext(context);
            return 1;
        }

        size_t global_item_size = n;
        double *times = (double *)malloc(n * sizeof(double));
        float *endres = (float *)malloc(n * sizeof(float));

        for (int j = 1; j < global_item_size; j++) {
            size_t temp = j;
            clock_t start_time = clock();  // Замер времени начала выполнения
            error = clEnqueueNDRangeKernel(command_queue, kernel, 1, NULL, &temp, NULL, 0, NULL, NULL);
            // Запуск ядра OpenCL для вычисления результата

            // Чтение результатов с устройства
            float *results = (float *)malloc(j * sizeof(float));
            error = clEnqueueReadBuffer(command_queue, results_mem_obj, CL_TRUE, 0, temp * sizeof(float), results, 0, NULL, NULL);

            clock_t end_time = clock();  // Замер времени окончания выполнения
            double execution_time = (double)(end_time - start_time) / CLOCKS_PER_SEC * 1000.0;  // Вычисление времени выполнения в миллисекундах
            times[j] = execution_time;   // Сохранение времени выполнения

            endres = results;  // Сохранение результатов
        }

        // Вывод результатов и времени выполнения
        float end_summ = 0;
        float end_time = 0;
        for (int i = 1; i < n; i++) {
            end_summ += endres[i];  // Суммирование результатов
            printf("x %10.3f: n:%3.3d summ %25.5f: time:%10.4f\n", x, i, end_summ, times[i]);  // Вывод результатов и времени
            end_time += times[i];    // Суммирование времени выполнения
        }
        printf("All time: %f \n", end_time);  // Вывод общего времени выполнения

        // Освобождение ресурсов OpenCL
        free(endres);
        clReleaseKernel(kernel);
        clReleaseProgram(program);
        clReleaseMemObject(results_mem_obj);
        clReleaseCommandQueue(command_queue);
        clReleaseContext(context);
    }

    return 0;
}

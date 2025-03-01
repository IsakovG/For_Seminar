/*
* Дисциплина: "Программирование на C#"
* Группа: БПИ-246 (подгруппа 1)
* Студент: Исаков Глеб Аюбович
* Дата: 06.10.2024
* Задача: По входным данным из файла input.txt составить
* два массива, если их размеры одинаковы, вычислить 
* значение их скалярного произведения P и записать в
* файл output.txt, иначе записать в файл output.txt значение 0
*/
using System;
using System.IO;
public static class Program
{
    // Метод SearchIndexesOfCorrectLines проходится по входному массиву и возвращает
    // массив, в котором элементы - это индексы первых двух элементов входного
    // массива, в которых есть корректные данные.
    public static int[] SearchIndexesOfCorrectLines(string[] input_array)
    {
        int[] output_array = new[] {-1, -1};
        for (int i = 0; i < input_array.Length; i++)
        {
            if (input_array.Length == 0)
            {
                break;
            }
            else if (NumOfInts(input_array[i]) > 0 &&  output_array[0] == -1)
            {
                output_array[0] = i;
            }
            else if (NumOfInts(input_array[i]) > 0 &&  output_array[1] == -1)
            {
                output_array[1] = i;
            }
            else if (output_array[0] != -1 && output_array[1] != -1)
            {
                break;
            }
        }
        return output_array;

    }
    // Метод ScalarProduct вычисляет и возвращает скалярное произведение 
    // двух массивов, если их размеры равны, в ином же случае возвращает 0.
    public static int ScalarProduct(int[] first, int[] second)
    {
        int product = 0;
        if (first.Length == second.Length)
        {
            for (int i = 0; i < first.Length; i++)
            {
                product += first[i] * second[i];
            }
        }
        return product;
    }
    // Метод NumOfInts возвращает количество элементов массива (строки), которых можно преобразовать в тип int.
    public static int NumOfInts(string[] input_array)
    {
        int counter_of_ints = 0;
        for (int i = 0; i < input_array.Length; i++)
        {
            counter_of_ints = (!int.TryParse(input_array[i], out _)) ? counter_of_ints : ++counter_of_ints;
        }
        return counter_of_ints;
    }
        public static int NumOfInts(string input_string)
    {
        string [] array = input_string.Split();
        int counter_of_ints = 0;
        for (int i = 0; i < array.Length; i++)
        {
            counter_of_ints = (!int.TryParse(array[i], out _)) ? counter_of_ints : ++counter_of_ints;
        }
        return counter_of_ints;
    }
    // Метод OnlyInts создает новый массив (выходной) и передает из входного массива 
    // в выходной массив те элементы, которые можно преобразовать в тип int,
    // после чего возвращает получившийся массив.
    public static int[] OnlyInts(string[] input_array)
    {
        int j = 0;
        int[] output_array = new int[NumOfInts(input_array)];
        for (int i = 0; i < input_array.Length; i++)
        {
            if (int.TryParse(input_array[i], out int int_element))
            {
                output_array[j++] = int_element;
            }
        }
        return output_array;
    }
    public static void Main()
    {
        ConsoleKeyInfo keyToExit;
        string[] array_of_all_lines;
        string[] array_A_strings;
        string[] array_B_strings;
        int[] array_A_ints_only;
        int[] array_B_ints_only;
        string path = @"input.txt";
        do
        {
            if (File.Exists(path))
            {
                array_of_all_lines = File.ReadAllLines(path);
                int[] indexes_of_correct_lines = SearchIndexesOfCorrectLines(array_of_all_lines);
                if ((indexes_of_correct_lines[0] == -1) || (indexes_of_correct_lines[1] == -1))
                {
                    Console.WriteLine("Корректных данных в файле нет");
                }
                else
                {
                array_A_strings = array_of_all_lines[indexes_of_correct_lines[0]].Split(' ');
                array_B_strings = array_of_all_lines[indexes_of_correct_lines[1]].Split(' ');
                array_A_ints_only = OnlyInts(array_A_strings);
                array_B_ints_only = OnlyInts(array_B_strings);
                int product = ScalarProduct(array_A_ints_only, array_B_ints_only);
                File.WriteAllText("output.txt", $"{product:f3}".Replace(".", ","));
                Console.WriteLine("Значение скалярного произведения P было записано в файл output.txt");
                }
            }
            else
            {
                Console.WriteLine("Входной Файл на диске отсутствует");
            }
            Console.Write("Желаете перезапустить программу? (Y/N): ");
            keyToExit = Console.ReadKey();
            Console.WriteLine();
        } while (keyToExit.Key == ConsoleKey.Y);
    }
}
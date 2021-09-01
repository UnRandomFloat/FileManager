using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_FileMan
{
    /// <summary>
    /// Распознает команды введенные пользователем.
    /// </summary>
    class Cmd_Dispetcher
    {
        /// <summary>
        /// Диспетчер распознования и вызова соответствующих команд 
        /// </summary>
        /// <param name="fromConsole">Команда и аргументы вводимые пользователем</param>
        /// <param name="thisPath">Путь активного каталога</param>
        /// <param name="onemoretime">Индикатор завершения работы программы</param>
        public static void StringToCommand(string fromConsole, string thisPath, out bool onemoretime)
        {
            try
            {
                onemoretime = false;
                if (fromConsole.Length == 0)
                {
                    Console.WriteLine("Введена пустая команда. Список доступных команд - scmd");
                    onemoretime = true;
                    return;
                }
                string[] separ = { " '", "'" };
                string[] comand = null;
                if (fromConsole.Split(separ, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                {
                    comand = fromConsole.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    comand = fromConsole.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
                switch (comand[0].Trim())
                {
                    //Команда вывода дерева файловой системы
                    case "ls":
                        Show_File_Folder_Tree.Start(comand, thisPath);
                        onemoretime = true;
                        break;
                    //Команда смены активной директории
                    case "cd":
                        Program.curentPath = Change_Directory.Cd(comand, thisPath);
                        onemoretime = true;
                        break;
                    //Команда вывода информации о файле или папке
                    case "inf":
                        Info.File_Info(comand, thisPath);
                        onemoretime = true;
                        break;
                    //Команда удаление файла или папки
                    case "rm":
                        RemoveFolderFile.removeFolderOrFile(comand, thisPath);
                        onemoretime = true;
                        break;
                    //Команда копирования файла или папки(рекурсивно)
                    case "cp":
                        FolderFileCopy.WhatWeHaveToCopy(comand, thisPath);
                        onemoretime = true;
                        break;
                    //Вывод списка доступных команд
                    case "scmd":
                        CmdList();
                        onemoretime = true;
                        break;
                    //Выход из программы
                    case "exit":
                        Console.WriteLine("Программа завершена! Всего Доброго!");
                        Console.ReadLine();
                        onemoretime = false;
                        break;
                    //Настройки программы
                    case "set":
                        Setups.StartSetting();
                        onemoretime = true;
                        break;
                    default:
                        Console.WriteLine($"Не удалось распознать команду ({comand[0].Trim()}) (список доступных команд и аргументов - scmd)");
                        onemoretime = true;
                        break;
                }
        }
            catch(IndexOutOfRangeException)
            {
            Console.WriteLine("Введена пустая команда. Повторите ввод.");
                onemoretime = true;
            }
}


        /// <summary>
        /// Выводит на консоль форматированный список команд. 
        /// </summary>
        static void CmdList()//Нахоидтся в этом классе потому что удобнее актуализировать при добавлении новых команд
        {
            Console.WriteLine(@"ВАЖНО!!! Все пути (и/или названия файлов/папок), содержащие пробелы необходимо указывать в одинарных ковычках (например: 'C:\Program Files'... 'Новая папка' и т.п.");
            string[,] cmds = {
            {"set","","Настройки программы"},
            {"exit","","Завершение программы"},
            {"cd","~","Переход в корневой каталог"},
            {"cd","..","Переход в папку выше"},
            {"cd",@"С:\Target","Переход в указанную директорию"},
            {"cd",@"Target","Переход в указанную подкаталог активной директории"},
            {"ls","","Список всех файлов и папок в текущей директории"},
            {"ls","-p 2","Список файлов и папок в текущей директории с заданной глубиной(2)"},
            {"ls",@"С:\Target","Список файлов и папок в указанной директории"},
            {"ls",@"С:\Target -p 3","Cписок файлов и папок в указанной директории с заданной глубиной(3)"},
            {"cp",@"Source.txt D:\Target.txt","Копирование файла текущей директории в указанную"},
            {"cp",@"С:\Source.txt D:\Target.txt","Копирование файла из заданной директории в указанную"},
            {"cp",@"С:\Source D:\Target","Копирование указанного каталога в указанную директорию"},
            {"сp",@"D:\Target","Копирование текущего каталога в указанную директорию"},
            {"inf","","Информация о текущем каталоге"},
            {"inf",@"D:\Target","Информация о указанном каталоге"},
            {"inf","Source.txt","Информация о файле из активного каталога"},
            {"inf",@"D:\Source.txt","Информация о файле из указанного каталога"},
            {"rm",@"","Удаление активного каталога и переход в корневой"},
            {"rm",@"Source","Удаление подкаталога из активного каталога"},
            {"rm",@"Source.txt","Удаление файла из активного каталога"},
            {"rm",@"C:\Source.txt","Удаление файла из указанного каталога"},};

            Console.WriteLine();
            Console.WriteLine("{0,-6}{1,-40}{2,-30}", "Имя", "│ " + "Аргумент(ы)", "│ " + "Назначение");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < cmds.GetLength(0); i++)
            {

                Console.WriteLine("{0,-6}{1,-40}{2,-30}", cmds[i, 0], "│ " + cmds[i, 1], "│ " + cmds[i, 2]);
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine(@"ВАЖНО!!! Все пути (и/или названия файлов/папок), содержащие пробелы необходимо указывать в одинарных ковычках (например: 'C:\Program Files'... 'Новая папка' и т.п.");
        }

    }
}


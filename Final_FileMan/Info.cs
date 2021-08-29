using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_FileMan
{
    class Info
    {
        /// <summary>
        /// Проверяет, что находится по заданому пути(папка или файл) и выводит информацию об объекте 
        /// </summary>
        /// <param name="cmdArray">Массив строк(команда раделенная на аргументы)</param>
        /// <param name="thisPath">Путь активной директории</param>
        /// <returns>Строка с полный путем файла или папки или пустая если файл или папка не найдены</returns>
        public static string File_Info(string[] cmdArray, string thisPath)
        {
            try
            {
                switch (cmdArray.Length)
                {
                    //Иноформация об активном каталоге
                    case 1:
                        DirectoryInfo dirInf = new DirectoryInfo(thisPath);
                        Console.WriteLine($"Размер каталога: " + dirInf.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length) / 1024 + " kB");
                        Console.WriteLine($"Корневой каталог: {dirInf.Root}");
                        Console.WriteLine($"Название каталога: {dirInf.Name}");
                        Console.WriteLine($"Полный путь: {dirInf.FullName}");
                        Console.WriteLine($"Время создания каталога: {dirInf.CreationTime}");
                        Console.WriteLine($"Всего файлов(включая файлы вложеных папок)-{dirInf.GetFiles("*", SearchOption.AllDirectories).Length}");
                        Console.WriteLine($"Всего папок(включая папки вложеных папок-{dirInf.GetDirectories().Length}");
                        return dirInf.ToString();
                 
                    // Информация об указанном каталоге/или файле
                    case 2:
                        return FileOrFolderInf(cmdArray, thisPath);
                    default:
                        Console.WriteLine("Не правильное количество аргументов");
                        return "";
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа недостаточно прав");
                return "";
            }
            catch 
            {
                Console.WriteLine("Что то пошло не так, свяжитесь с разработчиком");
                return "";
            }
        }
        /// <summary>
        /// Определяет являюется ли объект по указанному пути файлом или директорией и выводит о нем информацию
        /// </summary>
        /// <param name="cmdArray"></param>
        /// <param name="thisPath"></param>
        public static string FileOrFolderInf(string[] cmdArray, string thisPath)
        {
            //Информация об указанном подкаталоге
            if (Directory.Exists(thisPath + @"\" + cmdArray[1]))
            {
                PrintInfoDir(thisPath + @"\" + cmdArray[1]);
                DirectoryInfo d1 = new DirectoryInfo(thisPath + @"\" + cmdArray[1]);
                return d1.ToString();
            }
            //Информация об указанной директории
            else if (Directory.Exists(cmdArray[1]))
            {
                PrintInfoDir(cmdArray[1]);
                DirectoryInfo d1 = new DirectoryInfo(cmdArray[1]);
                return d1.ToString();
          
            }
            // Информация об указанном файле
            else if (File.Exists(cmdArray[1]))
            {
                PrintInfoFile(cmdArray[1]);
                FileInfo f1 = new FileInfo(cmdArray[1]);
                return f1.FullName;
               
            }
            // Информация об указанном файле активного каталога
            else if (File.Exists(thisPath + @"\" + cmdArray[1]))
            {
                PrintInfoFile(thisPath + @"\" + cmdArray[1]);
                FileInfo f1 = new FileInfo(thisPath + @"\" + cmdArray[1]);
                return f1.FullName;
            }
            Console.WriteLine($"Указанный файл или директория - {cmdArray[1]} - не найдена, убедитесь в корректности и провторите ввод");
            return "";
            // Вывод информации о папке
            void PrintInfoDir(string folderFullPath)
            {
                DirectoryInfo dirInf = new DirectoryInfo(folderFullPath);
                
                Console.WriteLine($"Размер каталога: " + dirInf.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length) / 1024 + " kB");
                Console.WriteLine($"Корневой каталог: {dirInf.Root}");
                Console.WriteLine($"Полный путь: {dirInf.FullName}");
                Console.WriteLine($"Название каталога: {dirInf.Name}");
                Console.WriteLine($"Время создания каталога: {dirInf.CreationTime}"); 
                Console.WriteLine($"Всего файлов(включая файлы вложеных папок)-{dirInf.GetFiles("*", SearchOption.AllDirectories).Length}");
                Console.WriteLine($"Всего папок(включая папки вложеных папок-{dirInf.GetDirectories().Length}");
           
            }
            //Вывод информации о файле
            void PrintInfoFile(string fileFullPath)
            {
                FileInfo fileInf = new FileInfo(fileFullPath);
                Console.WriteLine($"Размер файла: " + fileInf.Length + " kB");
                Console.WriteLine($"Полный путь к файлу:" + fileInf.FullName);
                Console.WriteLine($"Название файла:" + fileInf.Name);
                Console.WriteLine($"Время создания файла:" + fileInf.CreationTimeUtc);
                Console.WriteLine($"Последние изменения:" + fileInf.LastWriteTimeUtc);
                if (fileInf.IsReadOnly)
                {
                    Console.WriteLine($"Атрибут: Только для чтения");
                }
                
            }
        }
    }
}

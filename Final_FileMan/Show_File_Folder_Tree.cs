using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Final_FileMan
{
    /// <summary>
    /// Класс команды вывода списка файлов и папок
    /// </summary>
    class Show_File_Folder_Tree
    {
        public static int elemsPerPage = Properties.Settings.Default.ElemPerPage; //Количество элементов на одной странице
        public static int elemCount = 0;
        public static void Start(string[] cmdArray, string thisPath)
        {
            try
            {
                //распознает аргументы в зависмости от их количества
                switch (cmdArray.Length)
                {
                    case 1:
                        Console.WriteLine(thisPath);
                        Show_Tree(thisPath, 2, 0);
                        break;
                    case 2:
                        if (Directory.Exists(thisPath + @"\" + cmdArray[1]))
                        {
                            Console.WriteLine(thisPath + @"\" + cmdArray[1]);
                            Show_Tree(thisPath + @"\" + cmdArray[1], 2, 0);
                        }
                        else if (Directory.Exists(cmdArray[1]))
                        {
                            Console.WriteLine(cmdArray[1]);
                            Show_Tree(cmdArray[1], 2, 0);
                        }
                        else if (!Directory.Exists(cmdArray[1]) && cmdArray[1].Contains("-p"))
                        {
                            Console.WriteLine($"\nНе верно указан параметр ({cmdArray[1]}) (список доступных команд и аргументов - scmd)");
                        }
                        else
                        {
                            Console.WriteLine($"\nКаталог по указанному пути ({cmdArray[1]}) не существует или не доступен. Убедитесь в правильности и повторите ввод.");
                        }
                        break;
                    case 3:
                        if (cmdArray[1].Contains("-p"))
                        {
                            if (int.TryParse(cmdArray[2], out int deepLvla))
                            {
                                Show_Tree(thisPath, deepLvla, 0);
                            }
                            else
                            {
                                throw new WrongDeepLvl("Неверно указан уровень раскрытия списка файлов. Проверьте праывильность и повторите ввод!");
                            }
                        }
                        else if (cmdArray[2].Contains("-p"))
                        {
                            if (int.TryParse(cmdArray[2].Replace("-p", ""), out int deepLvla))
                            {
                                Show_Tree(cmdArray[1], deepLvla, 0);
                            }
                            else
                            {
                                throw new WrongDeepLvl("Неверно указан уровень раскрытия списка файлов. Проверьте праывильность и повторите ввод!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не правильно введен путь!");
                        }
                        break;
                    case 4:
                        if (Directory.Exists(cmdArray[1]) && int.TryParse(cmdArray[3], out int deepLvl))
                        {
                            Show_Tree(cmdArray[1], deepLvl, 0);
                        }

                        else if (Directory.Exists(thisPath + @"\" + cmdArray[1]) && int.TryParse(cmdArray[3], out int dpLvl))
                        {
                            Show_Tree(thisPath + @"\" + cmdArray[1], dpLvl, 0);
                        }
                        else
                        {
                            Console.WriteLine("Неправильное количество аргументов для команды ls (список доступных команд и аргументов - scmd)");
                        }
                        break;
                    default:
                        Console.WriteLine("Неправильное количество аргументов для команды ls (список доступных команд и аргументов - scmd)");
                        break;
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"\nКаталог по указанному пути -{cmdArray[1]}- не существует, не доступен или указан неверно.\nУбедитесь в правильности и повторите ввод.");
                return;
            }
            catch (WrongDeepLvl)//неудалось распознать глубину рекурсивного вывода файлов
            {
                return;
            }
        }
        /// <summary>
        /// Выводит дерево файлов на консоль
        /// </summary>
        /// <param name="thisPath">Полный путь начального каталога</param>
        /// <param name="deepLvl">Задаваймый числом уровень погружения в подкаталоги</param>
        /// <param name="deepstep">количество произведенных рекурсий вызова. Для первого вызова метода необходимо указать - 0 </param>
        static void Show_Tree(string thisPath, int deepLvl, int deepstep)
        {

            DirectoryInfo StartFolder = new DirectoryInfo(thisPath);
            FileSystemInfo[] FileFolderList = StartFolder.GetFileSystemInfos();
            deepstep++;
            for (int i = 0; i < FileFolderList.Length; i++)
            {
                if (elemCount != 0 && elemCount % elemsPerPage == 0)
                {
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"Конец страницы - {elemCount / elemsPerPage}. Для продолжения нажмите Enter или введите любой символ для завершения вывода и выбора дргуой команды");
                    if (!string.IsNullOrEmpty(Console.ReadLine()))
                    {
                        return;
                    }

                }

                try
                {
                    string itemPath;
                    if (i == FileFolderList.Length - 1)
                    {
                        itemPath = "       └──" + (FileFolderList[i].Name.Contains(' ') ? ("'" + FileFolderList[i].Name + "'") : FileFolderList[i].Name);
                    }
                    else
                    {
                        itemPath = "       ├──" + (FileFolderList[i].Name.Contains(' ') ? ("'" + FileFolderList[i].Name + "'") : FileFolderList[i].Name);
                    }
                    if (deepstep == 1)
                    {
                        elemCount++;
                        Console.WriteLine(itemPath);
                    }
                    if (deepstep > 1)
                    {
                        int count = 0;
                        while (count < deepstep - 1)
                        {
                            Console.Write("       │");
                            count++;
                        }
                        elemCount++;
                        Console.WriteLine(itemPath);
                    }
                    if (deepstep < deepLvl && FileFolderList[i].GetType().ToString().Contains("DirectoryInfo"))
                    {
                        Show_Tree(FileFolderList[i].FullName, deepLvl, deepstep);
                    }
                }
                catch (UnauthorizedAccessException)//Отсутствие доступа к каталогу и/или файлу
                {
                }

            }
        }

    }
}

using System;

using System.IO;

namespace Final_FileMan
{
    class Change_Directory
    {
        /// <summary>
        /// Изменение активного каталога
        /// </summary>
        /// <param name="cmdArray">КМассив строк(команда раделенная на аргументы)</param>
        /// <param name="thisPath">Путь активного каталога</param>
        /// <returns>string новый путь активного каталога</returns>
        public static string Cd(string[] cmdArray, string thisPath)
        {
            try
            {
                string strToThisPath = Path.GetFullPath(thisPath);
                string strToCmdArray = Path.GetFullPath(cmdArray[1]);
                switch (cmdArray.Length)
                {
                    case 2:
                        // Подъем на папку вверх
                        if (cmdArray[1] == "..")
                        {
                            DirectoryInfo di = Directory.GetParent(strToThisPath + @"\");
                            string newPath = di.Parent.FullName;
                            SaveLastPath(newPath);
                            return newPath;

                        }
                        // Переход в корневой каталог
                        else if (cmdArray[1] == "~")
                        {
                            SaveLastPath(Path.GetPathRoot(strToThisPath));
                            return Path.GetPathRoot(strToThisPath);
                        }

                        // Переход в указанный каталог
                        else if (Directory.Exists(strToCmdArray))
                        {
                            SaveLastPath(strToCmdArray);
                            return strToCmdArray;
                        }

                        // Переход в указанный подкаталог активного каталога
                        else if (Directory.Exists(Path.Combine(thisPath, cmdArray[1])))
                        {
                            SaveLastPath(Path.Combine(thisPath, cmdArray[1]));
                            return Path.Combine(thisPath, cmdArray[1]);
                        }
                        Console.WriteLine($"Указанный путь {cmdArray[1]} не найден либо является файлом, укажите директорию");
                        return strToThisPath;
                    default:
                        Console.WriteLine($"Неверное количество аргументов комадны cd (введите scmd для вывода списока доступных команд и аргументов");
                        return strToThisPath;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Путь задан некорректно. Повторите ввод");
                return thisPath;
            }

            catch (NullReferenceException)
            {
                Console.WriteLine("Вы уже в корневом каталоге");
                return thisPath;
            }
        }


        static void SaveLastPath(string pathToSave)
        {
            Properties.Settings.Default.LastActivePath = pathToSave;
            Properties.Settings.Default.Save();
        }

    }

}

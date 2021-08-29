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
            switch (cmdArray.Length)
            {
                case 2:
                    // Подъем на папку вверх
                    if (cmdArray[1] == "..")
                    {
                        return TryPathException(thisPath);
                    }
                    // Переход в корневой каталог
                    else if (cmdArray[1] == "~")
                    {
                        SaveLastPath(Directory.GetDirectoryRoot(thisPath));
                        return Directory.GetDirectoryRoot(thisPath);
                    }
                    // Переход в указанный каталог
                    else if (Directory.Exists(cmdArray[1]))
                    {
                        SaveLastPath(cmdArray[1]);
                        return cmdArray[1];
                    }
                    // Переход в указанный подкаталог активного каталога
                    else if (Directory.Exists(thisPath + @"\" + cmdArray[1]))
                    {
                        if (!thisPath.EndsWith(@"\"))
                        {
                            SaveLastPath(thisPath + @"\" + cmdArray[1]);
                            return thisPath + @"\" + cmdArray[1];
                        }
                        else
                        {
                            SaveLastPath(thisPath + cmdArray[1]);
                            return thisPath + cmdArray[1];
                        }
                    }
                    Console.WriteLine($"Указанный путь {cmdArray[1]} не найден либо является файлом, укажите директорию") ;
                    return thisPath;
                default:
                    Console.WriteLine($"Неверное количество аргументов комадны cd (введите scmd для вывода списока доступных команд и аргументов");
                    return thisPath;
            }
        }

        static string TryPathException(string thisPath)
        {
            string newPath = "";
            try
            {
                if (thisPath.EndsWith(@"\"))
                {
                    DirectoryInfo di = Directory.GetParent(thisPath);
                    newPath = di.Parent.FullName;
                }
                else
                {
                    DirectoryInfo di = Directory.GetParent(thisPath+ @"\");
                    newPath = di.Parent.FullName;
                }
            }
            catch
            {

                return thisPath; 
            }
            SaveLastPath(newPath);
            return newPath;

        }
        static void SaveLastPath(string pathToSave)
        {
            Properties.Settings.Default.LastActivePath = pathToSave;
            Properties.Settings.Default.Save();
        }

    }

}

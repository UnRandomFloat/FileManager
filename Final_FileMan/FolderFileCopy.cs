using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Final_FileMan
{
    class FolderFileCopy
    {
        /// <summary>
        /// Метод определяет что предстоит скопировать(файл или папку) и передает управление соотвествующему методу
        /// </summary>
        /// <param name="cmdArray">Массив строк(команда раделенная на аргументы)</param>
        /// <param name="thisPath">Активная директория</param>
        public static void WhatWeHaveToCopy(string[] cmdArray, string thisPath)
        {
            try
            {
                switch (cmdArray.Length)
                {
                    case 2:
                        FolderCopy(thisPath, cmdArray[1]);
                        break;
                    case 3:
                        isFileOrFolder(cmdArray, thisPath);
                        break;
                    default:
                        Console.WriteLine("Неправильное количество аргументов для команды cp (список доступных команд и аргументов - scmd)");
                        break;
                }

            }
            catch
            {

            }
        }

        static void isFileOrFolder(string[] cmdArray, string thisPath)
        {
            if (Directory.Exists(cmdArray[1]))
            {
                FolderCopy(cmdArray[1], cmdArray[2]);

            }
            else if (Directory.Exists(Path.Combine(thisPath, cmdArray[1])))
            {
                FolderCopy(Path.Combine(thisPath, cmdArray[1]), cmdArray[2]);

            }
            else if (File.Exists(cmdArray[1]))
            {
                FileCopy(cmdArray[1], cmdArray[2]);

            }
            else if (File.Exists(Path.Combine(thisPath, cmdArray[1])))
            {
                FileCopy(Path.Combine(thisPath, cmdArray[1]), cmdArray[2]);

            }

        }

        static void FolderCopy(string sourcePath, string targetPath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] file = dir.GetFiles();
            if (Directory.Exists(sourcePath) && targetPath.Contains(@":\"))
            {
                Directory.CreateDirectory(targetPath);
                foreach (FileInfo fi in file)
                {
                    string tempPath = Path.Combine(targetPath, fi.Name);
                    fi.CopyTo(tempPath, false);
                }
                foreach (DirectoryInfo folders in dirs)
                {
                    string tempPath = Path.Combine(targetPath, folders.Name);
                    FolderCopy(folders.FullName, tempPath);
                    Console.WriteLine($"{sourcePath} успешно скопирован {targetPath}.");
                }
            }
            else 
            {
                Console.WriteLine($"Путь {targetPath} введен некорректно. Повторите ввод!");
            }
        }

        static void FileCopy(string sourcePath, string targetPath)
        {
            try
            {

                File.Copy(sourcePath, targetPath, false);
                Console.WriteLine($"{sourcePath} успешно скопирован {targetPath}.");
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }
    }






}



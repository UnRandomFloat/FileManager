using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_FileMan
{
    /// <summary>
    /// Класс команды удаления файла или папки
    /// </summary>
    class RemoveFolderFile
    {
        /// <summary>
        /// Удаляет файл или папку с подтверждением, предварительно выводит информацию об объекте
        /// </summary>
        /// <param name="cmdArray">Массив строк(команда раделенная на аргументы)</param>
        /// <param name="thisPath">Путь активная директория</param>
        public static void removeFolderOrFile(string[] cmdArray, string thisPath)
        {
            string decision = "";
            bool escapeRemove = false;
            Console.WriteLine("==================================================");
            try
            {
                string whatToDel = Info.File_Info(cmdArray, thisPath);

                //Случае отсутсвия файла или папки по указаному пути прервать команду удаления
                if (string.IsNullOrEmpty(whatToDel))
                {
                    return;
                }

                while (!escapeRemove || decision == "д")
                {

                    Console.WriteLine("==================================================");
                    Console.WriteLine("Вы действительно желаете удалить?\nДа - введите д \nНет - введите н");
                    decision = Console.ReadLine();
                    if (decision == "д")
                    {
                        if (Directory.Exists(whatToDel))
                        {
                            if (whatToDel == thisPath)
                            {
                                string[] gotoRot = { " ", "~" };
                                Program.curentPath = Change_Directory.Cd(gotoRot, thisPath);
                                Directory.Delete(whatToDel, true);
                                Console.WriteLine($"{whatToDel} успешно удален!");
                                return;
                            }
                            else
                            {
                                Directory.Delete(whatToDel, true);
                                Console.WriteLine($"{whatToDel} успешно удален!");
                                return;
                            }
                        }
                        else if (File.Exists(whatToDel))
                        {
                            File.SetAttributes(whatToDel, FileAttributes.Archive);
                            File.Delete(whatToDel);
                            Console.WriteLine(whatToDel +" успешно удален ");
                            return;
                        }
                        return;
                    }
                    else if (decision == "н")
                    {
                        escapeRemove = true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Недостаточно прав для удаления каталога или файла");
                return;
            }
            catch
            {
                Console.WriteLine("Что то пошло не так, обратитесь к разработчику");
                return;
            }

        }
    }
}


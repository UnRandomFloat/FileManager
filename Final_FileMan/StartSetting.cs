using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;


namespace Final_FileMan
{
    /// <summary>
    /// Класс для настройки некоторых параметров приложения
    /// </summary>
    class Setups
    {
        public static void StartSetting()
        {
            
            bool nextstep = false;
          
                Console.WriteLine("Файловый менеджер имеет следующие настройки:");
            while (!nextstep)
            {
                string lastPath = Properties.Settings.Default.LastActivePath;
                int elemPerPege = Properties.Settings.Default.ElemPerPage;
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                string statusPath = Directory.Exists(lastPath) ? "   >>>  Статус - доступна" : ">>>  Статус - не доступна";
                Console.WriteLine("1 - Последняя активная директория: " +lastPath + statusPath);
                Console.WriteLine();
                Console.WriteLine("2 - Количество элементов выводимых командой ls - " + Properties.Settings.Default.ElemPerPage);
                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                Console.WriteLine("Что бы редактировать введите сооответствующую цифру или нажмите Enter для продолжения с текущими настройками!\nВ случае недоступной директрории она будет установлена по умолчанию!");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        Properties.Settings.Default.LastActivePath = PathSetup();
                        Properties.Settings.Default.Save();
                        Program.lastPath = Properties.Settings.Default.LastActivePath;
                        break;
                    case "2":
                        Properties.Settings.Default.ElemPerPage = ElemPerPageSetup();
                        Properties.Settings.Default.Save();
                        Show_File_Folder_Tree.elemsPerPage = Properties.Settings.Default.ElemPerPage;
                        break;
                    case "":
                        if (Directory.Exists(lastPath))
                        {
                            Program.curentPath = lastPath;
                            Properties.Settings.Default.LastActivePath = lastPath;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Program.curentPath = Directory.GetCurrentDirectory();
                            Properties.Settings.Default.LastActivePath = Directory.GetCurrentDirectory();
                            Properties.Settings.Default.Save();

                        }
                        if (elemPerPege < 1)
                        {
                            Properties.Settings.Default.ElemPerPage = 5;
                            Properties.Settings.Default.Save();
                            Show_File_Folder_Tree.elemsPerPage = 5;
                        }
                        nextstep = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестный выбор. Повторите ввод: ");
                        nextstep =false;
                        break;
                }
            }

        }
        static string PathSetup()
        {
            Console.WriteLine(@"Введите стартовый путь (например C:\Program Files):");
            string newpath = Console.ReadLine();
            while (!Directory.Exists(newpath) || string.IsNullOrEmpty(newpath))
            {
                Console.WriteLine("Введен некорректный(несуществующий или у вас недостаточно прав) путь.\n Введите стартовый путь или нажмите Enter чтобы выбрать путь по умолчанию:");
                newpath = Console.ReadLine();
                
            }
            return newpath;
        }
        static int ElemPerPageSetup()
        {
            Console.WriteLine("Укажите максимальное количество элементов выводимых командой ls на одной странице (от 1 до 100)");
            string newCount = Console.ReadLine();
            int setCount;
            while (!int.TryParse(newCount, out setCount) || setCount<1 || setCount>100)
            {
                Console.WriteLine("Число элементов введено не корректно. Повторите ввод (используйте только целое чило , больше 0 но менее 100)");
                newCount = Console.ReadLine();
            }
            return setCount;
        }

    }
}


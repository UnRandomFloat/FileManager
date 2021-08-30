using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Final_FileMan
{
    class Program
    {
        public static string curentPath;
        public static string lastPath;


        static void Main(string[] args)

        {
            Setups.StartSetting();

       
            Console.WriteLine("Введите команду (для вывода доступных команд введите scmd):");
            Console.WriteLine();
            bool repit = true;
            while (repit)
            {
                Console.Write(curentPath + " > ");
                Show_File_Folder_Tree.elemCount = 0;
                string cmd = Console.ReadLine();
                Cmd_Dispetcher.StringToCommand(cmd, curentPath, out repit);
            }

        }
    }
}

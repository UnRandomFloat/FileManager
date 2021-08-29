using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_FileMan
{
    /// <summary>
    /// Класс формирования собственных исключений
    /// </summary>
    [Serializable]
    public class WrongDeepLvl : Exception
    {
        public WrongDeepLvl(string messaga)
        {
            Console.WriteLine(messaga);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">>> ");
                string choice = Console.ReadLine();
                CommandHandler handler = new CommandHandler();
                handler.Handle(choice);
            }
        }
    }
}

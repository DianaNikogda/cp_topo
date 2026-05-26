using System;

class Program
{
    
    static void Main(string[] args)
    {
        WatcherService watcher = new WatcherService();
        //SessionService _session = new SessionService();
        Console.WriteLine("WATCHER");

        while (true)
        {
            Console.WriteLine("1 - Сохранить метрики");
            Console.WriteLine("2 - Показать статистику");
            Console.WriteLine("3 - Просмотр активных пользователей");
            Console.WriteLine("4 - Авто режим");

            Console.Write(">>> ");

            string input = Console.ReadLine();

            try
            {
                switch (input)
                {
                    case "1":
                        watcher.SaveMetrics();
                        break;

                    case "2":
                        watcher.ShowStats();
                        break;

                    case "3":
                        Console.WriteLine(watcher.GetUsers());
                        break;
                    case "4":
                        watcher.AutoMode();
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}
using System;

namespace MyDocuWithCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("command prompt");
                string inputcommand = Console.ReadLine();
                try
                {
                    ICommand cmd = CommandFactory.GetCommand(inputcommand);
                    cmd.Execute();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

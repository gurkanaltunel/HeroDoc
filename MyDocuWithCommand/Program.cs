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
                    object[] parameters = CommandFactory.GetParameters(inputcommand);
                    cmd.Execute(parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

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
                    string[] parameters = CommandFactory.GetParameters(inputcommand);
                    if (cmd.GetType().Name=="Dir"||cmd.GetType().Name=="Exit")
                    {
                        cmd.Execute();
                    }
                    else if (cmd.GetType().Name == "Md")
                    {
                        cmd.ExecuteWithParameter(parameters[1]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

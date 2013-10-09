
namespace MyDocuWithCommand
{
    public class InputCommand
    {
        public string _commandName;
        public string _paremeter1;
        public string _parameter2;

        public InputCommand(string commandName)
        {
            _commandName = commandName;
        }
        public InputCommand(string commandName, string parameter1)
        {
            _commandName = commandName;
            _paremeter1 = parameter1;
        }
        public InputCommand(string commandName, string parameter1, string parameter2)
        {
            _commandName = commandName;
            _paremeter1 = parameter1;
            _parameter2 = parameter2;
        }
    }
}

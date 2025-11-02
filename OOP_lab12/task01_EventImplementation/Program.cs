using System;

namespace Dispatcher
{
    public delegate void NameChangeEventHandler(object sender, NameChangeEventArgs args);
    public class NameChangeEventArgs : EventArgs
    {
        public string Name { get; private set; }

        public NameChangeEventArgs(string name)
        {
            Name = name;
        }
    }

    public class Dispatcher
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnNameChange(new NameChangeEventArgs(value));
                }
            }
        }

        public event NameChangeEventHandler? NameChange;

        protected void OnNameChange(NameChangeEventArgs args)
        {
            NameChange?.Invoke(this, args);
        }
    }

    public class Handler
    {
        public void OnDispatcherNameChange(object sender, NameChangeEventArgs args)
        {
            Console.WriteLine($"Dispatcher's name changed to {args.Name}.");
        }
    }

    public class Program
    {
        public static void Main()
        {
            var dispatcher = new Dispatcher();
            var handler = new Handler();

            dispatcher.NameChange += handler.OnDispatcherNameChange;

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                    break;

                dispatcher.Name = input;
            }
        }
    }
}

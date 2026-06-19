namespace Clicker
{
    public class KeyboardAction : Action
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Id + ": Keyboard ; " + Period + " ; " + Text;
        }
    }
}

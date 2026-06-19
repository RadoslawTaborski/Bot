namespace Clicker
{
    public class SubSequenceAction : Action
    {

        public string FileName { get; set; }
        public int Iterations { get; set; }

        public override string ToString()
        {
            return Id + ". " + FileName + " x " + Iterations + " ; " + Period;
        }
    }
}

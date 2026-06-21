using System.Drawing;

namespace Clicker
{
    public class KeyboardAction : Action
    {
        public string Text { get; set; }

        public override string ToString()
        {
            var tag = string.IsNullOrEmpty(Tag) ? "---" : Tag;
            var description = string.IsNullOrWhiteSpace(Description) ? $"{Text}" : Description;
            return $"{Id}. {tag} ; Text ; {description} ; {Period}";
        }
    }
}

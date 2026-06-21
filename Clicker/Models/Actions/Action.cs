using System;

namespace Clicker
{
    [Serializable]
    public abstract class Action
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public Actions Type { get; set; }
    }
}

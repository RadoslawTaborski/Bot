using System;

namespace Clicker
{
    [Serializable]
    public abstract class Action
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public Actions Type { get; set; }
    }
}

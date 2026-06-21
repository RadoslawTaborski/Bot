using System;
using System.Text.Json.Serialization;

namespace Clicker
{
    public abstract class Action
    {
        public string Id { get; set; }
        public int Period { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public Actions Type { get; set; }

        public Action Clone()
        {
            var clone = (Action)MemberwiseClone();
            clone.Guid = Guid.NewGuid();
            return clone;
        }

        [JsonIgnore]
        internal Guid Guid { get; set; } = Guid.NewGuid();
    }
}

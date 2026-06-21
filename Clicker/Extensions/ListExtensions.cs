using System.Collections.Generic;

namespace Clicker.Extensions
{
    public static class ListExtensions
    {
        public static void ReplaceWithRange<T>(this List<T> list, int index, IEnumerable<T> items)
        {
            list.RemoveAt(index);

            foreach (var item in items)
            {
                list.Insert(index++, item);
            }
        }
    }
}
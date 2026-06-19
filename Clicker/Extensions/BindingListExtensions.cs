using System.Collections.Generic;
using System.ComponentModel;

namespace Clicker.Extensions
{
    public static class BindingListExtensions
    {
        public static void ReplaceWithRange<T>(this BindingList<T> list, int index, IEnumerable<T> items)
        {
            list.RemoveAt(index);

            foreach (var item in items)
            {
                list.Insert(index++, item);
            }
        }
    }
}
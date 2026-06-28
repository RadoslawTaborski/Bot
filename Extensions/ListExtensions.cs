namespace Clicker.Extensions;

public static class ListExtensions
{
    public static void ReplaceWithRange<T>(this List<T> list, int index, IEnumerable<T> items)
    {
        list.RemoveAt(index);

        foreach (T? item in items)
        {
            list.Insert(index++, item);
        }
    }
}
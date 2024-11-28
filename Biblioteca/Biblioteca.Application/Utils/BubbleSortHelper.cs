namespace Biblioteca.Application.Utils
{

    public static class BubbleSortHelper
    {
        public static IEnumerable<T> BubbleSort<T>(IEnumerable<T> items, string sortField)
        {
            var list = items.ToList();
            var property = typeof(T).GetProperty(sortField);

            if (property == null) return list;

            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    var left = property.GetValue(list[j]);
                    var right = property.GetValue(list[j + 1]);

                    if (Comparer<object>.Default.Compare(left, right) > 0)
                    {
                        (list[j], list[j + 1]) = (list[j + 1], list[j]);
                    }
                }
            }

            return list;
        }
    }
}
namespace Biblioteca.Application.Utils
{
    public static class QuickSortHelper
    {
        public static IEnumerable<T> QuickSort<T>(IEnumerable<T> items, string sortField)
        {
            var list = items.ToList();
            if (list.Count <= 1) return list;

            var pivot = list.First();
            var property = pivot.GetType().GetProperty(sortField);
            if (property == null) return list;

            var pivotValue = property.GetValue(pivot);

            var less = list.Skip(1).Where(x => Comparer<object>.Default.Compare(property.GetValue(x), pivotValue) <= 0);
            var greater = list.Skip(1)
                .Where(x => Comparer<object>.Default.Compare(property.GetValue(x), pivotValue) > 0);

            return QuickSort(less, sortField).Concat(new[] { pivot }).Concat(QuickSort(greater, sortField));
        }
    }
}
namespace Biblioteca.Application.Utils
{
    public static class MergeSortHelper
    {
        public static IEnumerable<T> MergeSort<T>(IEnumerable<T> items, string sortField)
        {
            var list = items.ToList();
            if (list.Count <= 1) return list;

            var mid = list.Count / 2;
            var left = list.Take(mid);
            var right = list.Skip(mid);

            return Merge(MergeSort(left, sortField), MergeSort(right, sortField), sortField);
        }

        private static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right, string sortField)
        {
            var result = new List<T>();
            var property = typeof(T).GetProperty(sortField);

            if (property == null) return result;

            var leftList = left.ToList();
            var rightList = right.ToList();

            while (leftList.Any() && rightList.Any())
            {
                var leftValue = property.GetValue(leftList.First());
                var rightValue = property.GetValue(rightList.First());

                if (Comparer<object>.Default.Compare(leftValue, rightValue) <= 0)
                {
                    result.Add(leftList.First());
                    leftList.RemoveAt(0);
                }
                else
                {
                    result.Add(rightList.First());
                    rightList.RemoveAt(0);
                }
            }

            result.AddRange(leftList);
            result.AddRange(rightList);

            return result;
        }
    }
}
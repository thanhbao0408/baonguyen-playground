namespace BN.Common
{
    public static class CollectionExtensions
    {
        public static bool IsListEqual<T>(this List<T> firstList, List<T> secondList)
        {
            var firstNotSecond = firstList.Except(secondList).ToList();
            var secondNotFirst = secondList.Except(firstList).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}
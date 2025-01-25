using System.Collections.Generic;

namespace GGJ2025
{
    public static class ListUtils
    {
        public static int FindRangeIndexBinarySearch(this List<float> sortedList, float value)
        {
            int left = 0;
            int right = sortedList.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (mid < sortedList.Count - 1 && 
                    value >= sortedList[mid] && 
                    value <= sortedList[mid + 1])
                {
                    return mid;
                }

                if (value < sortedList[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return -1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions
{
    public static class HashSetExtensions
    {
        public static bool AddRange<T>(this HashSet<T> @this, IEnumerable<T> items)
        {
            bool allAdded = true;
            foreach (T item in items)
            {
                allAdded &= @this.Add(item);
            }
            return allAdded;
        }
    }
}

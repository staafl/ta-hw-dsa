namespace SortingHomework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SelectionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(IList<T> collection)
        {
            for (int ii = 0; ii < collection.Length; ++ii)
            {
                collection[ii] = collection.Skip(ii).Min();
            }
        }
    }
}

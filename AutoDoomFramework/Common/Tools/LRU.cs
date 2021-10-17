using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Common.Tools
{
    class LRU<T>
    {
        private HashSet<T> valueSets = new HashSet<T>();
        private LinkedList<T> values = new LinkedList<T>();

        public LRU()
        {
            valueSets = new HashSet<T>();
            values = new LinkedList<T>();
        }

        public void Put(T value)
        { 
            if (valueSets.Contains(value))
            {
                values.Remove(value);
                values.AddFirst(value);
            }
            else
            {
                valueSets.Add(value);
                values.AddFirst(value);
            }
        }

        public void Clear()
        {
            valueSets.Clear();
            values.Clear();
        }

        public LinkedList<T> Values { get => values; private set { } }
    }
}

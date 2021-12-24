using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class Item<T>
    {
        public T Data { get; set; }
        public Item<T> Previous { get; set; }
        public Item<T> Next { get; set; }

        public Item() {
            Previous = null;
            Next = null;
            Data = default(T);
        }

        public Item(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString();
        }

        //public bool Equals(Item<T> other)
        //{
        //    return other.CompareTo(this) == 0;
        //}

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }
    }
}

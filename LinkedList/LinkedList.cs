using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class LinkedList<T> : ICollection<T>
    {
        
        #region Properties
        public Item<T> Head { get; set; }
        public Item<T> Tail { get; set; }
        public int Count { get; set; }
        public bool IsReadOnly { get; set; }
        #endregion

        #region Events
        public event EventHandler OnClear;
        public event EventHandler<T> OnAdd;
        public event EventHandler<T> OnRemove;
        #endregion

        #region Constructors
        public LinkedList() {
            Head = null;
            Tail = null;
            Count = 0;
            IsReadOnly = false;
        }

        public LinkedList(T data)
        {
            var item = new Item<T>(data);
            Head = item;
            Tail = item;
            Count = 1;
            IsReadOnly = false;
        }
        public LinkedList(T[] datas)
        {
            Head = null;
            Tail = null;
            Count = 0;
            IsReadOnly = false;
            for (int i = 0; i < datas.Length; i++)
            {
                T data = datas[i];
                this.Add(data);
            }
        }
        #endregion

        #region Metods
        public void Add(T data)
        {
            if(IsReadOnly)
            {
                throw new NotSupportedException();
            }
            var item = new Item<T>(data);

            if (Count == 0)
            {
                Head = item;
                Tail = item;
                Count = 1;
                return;
            }

            Tail.Next = item;
            item.Previous = Tail;
            Tail = item;
            Count++;
            OnAdd?.Invoke(this, data);
        }

        public bool Remove(T data)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }
            var current = Head;

            if (Head.Data.Equals(data))
            {
                T temp = current.Data;
                Head.Next.Previous = null;
                Head = Head.Next;
                Count--;
                //Event onClear when count of item equial 0
                OnRemove?.Invoke(this, temp);
                return true;
            }
            
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else if (current.Next == null)
                    {
                        Tail = current.Previous;
                    }
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    Count--;
                    OnRemove?.Invoke(this, current.Data);
                    return true;
                }
                current = current.Next;
            }
            
            return false;
        }

        /// <summary>
        /// Reverse collection
        /// </summary>
        /// <returns>Reversed collection</returns>
        public LinkedList<T> Reverse()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }
            var result = new LinkedList<T>();
            var current = Tail;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Previous;
            }
            return result;
        }
        
        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }

        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }
            //Clear our collection
            Head = null;
            Tail = null;
            Count = 0;
            //Event onClear when count of item equial 0
            OnClear?.Invoke(this, null);
            
        }

        public bool Contains(T item)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }
            if (arrayIndex < 0)
            {
                throw new IndexOutOfRangeException();  
            }
            
            var current = Head;
            int ai = arrayIndex;
            for (int i = 0; i < Count; i++)
            {
                array[ai] = current.Data;
                current = current.Next;
                ai++;
            }
        }

        /// <summary>
        /// Indexator
        /// </summary>
        /// <param name="num">Number of item of List</param>
        /// <returns>T data of num's item of List</returns>
        public T this[int num]
        {
            get
            {
                if (num > Count - 1 || num < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                var current = Head;
                for(int i = 0; i < num; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException();
                }
                if (num > Count - 1 || num < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                var current = Head;
                for (int i = 0; i < num; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }
        #endregion

        //#region Overide methods
        //public override string ToString()
        //{
        //    string list = "{ ";
        //    foreach(Item<T> item in this)
        //    {
        //        list += item.ToString() + ", ";
        //    }
        //    list.Remove(list.Length - 2);
        //    list += "}";
        //    return list;
        //}

            
        //#endregion
    }
}

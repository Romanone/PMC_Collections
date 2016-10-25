using System;
using System.Collections;
using System.Collections.Generic;

namespace PMC_Library
{
    public abstract class PMCCollection<T> : ICollection<T>, IEnumerator<T>, IEnumerable<T>, IDisposable
    {
        protected ArrayList collectionList;
        protected bool isReadOnly;
        private int position = -1;

        public PMCCollection()
        {
            collectionList = new ArrayList();
        }

        public virtual T this[int index]
        {
            get { return (T)collectionList[index]; }
            set { collectionList[index] = value; }
        }

        public virtual int Count
        {
            get { return collectionList.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return isReadOnly; }
        }

        /// <summary>
        /// Abstract method for collection adding
        /// </summary>
        /// <param name="item">Collection item of <T></param>
        public abstract void Add(T item);

        public void Clear()
        {
            collectionList.Clear();
        }

        public virtual bool Contains(T collectionItem)
        {
            foreach (var item in collectionList)
            {
                if (item.Equals(collectionList))
                {
                    return true;
                }
            }
            return false;
            //return collectionList.Contains(collectionItem); //LINQ
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collectionList.CopyTo(array, arrayIndex);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < collectionList.Count; i++)
            {
                if (collectionList[i].Equals(item))
                {
                    collectionList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        T IEnumerator<T>.Current
        {
            get { return (T)collectionList[position]; }
        }

        object IEnumerator.Current
        {
            get { return collectionList[position]; }
        }

        void IDisposable.Dispose()
        {
            ((IEnumerator)this).Reset();
        }

        bool IEnumerator.MoveNext()
        {
            if (position < collectionList.Count - 1)
            {
                position++;
                return true;
            }
            return false;
        }

        void IEnumerator.Reset()
        {
            position = -1;
        }

    }
}

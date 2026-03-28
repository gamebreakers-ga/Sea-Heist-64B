using System.Collections;
using System.Collections.Generic;

public class Collection<T> : ICollection<T>
{
    T[] a;
    public int Count => a.Length;
    public bool IsReadOnly = true;

    public bool Remove(T Item)
    {
        return true;
    }

    public void CopyTo(T[] Item, int s)
    {

    }

    public bool Contains(T Item)
    {
        return true;
    }

    public void Clear()
    {

    }

    public void Add(T Item)
    {

    }


}
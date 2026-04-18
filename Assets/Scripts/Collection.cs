using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector<T> : ICollection<T> where T : class
{
    private T[] _Items;
    private Dictionary<Type, int> _Locations;

    private T _Selected;
    public T Selected => _Selected;
    private int _SelectedNum;
    public int SelectedNum => _SelectedNum;

    public Selector(int Capacity)
    {
        _Items = new T[Capacity];
        _Locations = new(Capacity);
    }

    public T Select(int location)
    {
        try
        {
            _Selected = _Items[location];
            _SelectedNum = location;
            return Selected;
        } catch
        {
            return null;
        }
        
    }

    private int _Count = 0;
    public int Count
    {
        get => _Count;
        set
        {
            _Count = value;
        }
    }

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        if (_Locations.ContainsKey(item.GetType())) return;
        EnsureMinimumCapacity(Count + 1);
        _Items[Count] = item;
        _Locations.Add(item.GetType(), Count);
        Count++;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return (IEnumerator<T>)_Items.GetEnumerator();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_Items).GetEnumerator();
    }

    public void EnsureMinimumCapacity(int NeededCapacity)
    {
        if (NeededCapacity >= _Items.Length)
        {
            Array.Resize(ref _Items, (NeededCapacity) * 2);
        }
    }
}
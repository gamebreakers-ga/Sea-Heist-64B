using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System;

[Serializable]
public class Selection<T> : IEnumerable<T> where T : class
{
    private T[] _Items;
    private Dictionary<Type, int> _ItemsLocation;
    public int Count { get; private set; }

    private int _Current;
    public int Current
    {
        get => _Current;
        set
        {
            if (value < 0) throw new IndexOutOfRangeException("Index must be greater than or equal to 0.");
            if (value >= Count) throw new IndexOutOfRangeException("Index must be less than Count.");
            _Current = value;
        }
    }
    public T Selected => _Items[_Current];

    public Selection(int Capacity = 0)
    {
        Capacity = Capacity < 0 ? 0 : Capacity;
        _Items = new T[Capacity];
        _ItemsLocation = new(Capacity);

    }

    public T this[int Index]
    {
        get
        {
            if (Index < 0) throw new IndexOutOfRangeException("Index must be greater than or equal to 0.");
            if (Index >= Count) throw new IndexOutOfRangeException("Index must be less than Count.");
            return _Items[Index];
        }
        set
        {
            if (Index < 0) throw new IndexOutOfRangeException("Index must be greater than or equal to 0.");
            if (Index >= Count) throw new IndexOutOfRangeException("Index must be less than Count.");
            _Items[Index] = value;
            _ItemsLocation[value.GetType()] = Index;
        }
    }

    public void Add(T item) => TryAdd(item);

    public bool TryAdd(T item)
    {
        if (_ItemsLocation.ContainsKey(item.GetType())) return false;
        _ItemsLocation.Add(item.GetType(), Count);
        if (Count + 1 >= _Items.Length)
        {
            Array.Resize(ref _Items, (Count + 1) * 2);
        }
        _Items[Count] = item;
        Count++;
        return true;
    }

    public void SetCurrentToRandom()
    {
        _Current = UnityEngine.Random.Range(0, Count);
    }

    public bool TrySelect(int index)
    {
        try
        {
            Current = index;
            return true;
        } catch
        {
            return false;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in _Items) yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}


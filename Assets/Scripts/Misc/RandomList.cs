using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomList<T> : List<T>
{
    public RandomList() { }
    public RandomList(IEnumerable<T> list) : base(list) { }

    public T GetRandom()
    {
        return this[Random.Range(0, Count)];
    }
}

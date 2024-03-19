using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions
{
    public static T GetRandomItem<T>(this ICollection<T> collection)
    {
        int randomIndex = Random.Range(0, collection.Count);

        int i = 0;
        foreach (var item in collection)
        {
            if (i == randomIndex)
                return item;
            i++;
        }

        throw new System.IndexOutOfRangeException();
    }
}

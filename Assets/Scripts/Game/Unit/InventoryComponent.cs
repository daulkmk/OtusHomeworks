using System;
using System.Collections.Generic;

public class InventoryComponent
{
    private HashSet<string> _items = new();

    public bool Add(string item) => _items.Add(item);
    public bool Remove(string item) => _items.Remove(item);
    public bool Contains(string item) => _items.Contains(item);
}

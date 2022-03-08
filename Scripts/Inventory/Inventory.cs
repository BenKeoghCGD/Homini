using System.Collections.Generic;

static class Inventory
{
    public static int invSlots, capacity;
    public static List<Item> items = new List<Item>();
    public static List<ItemSlot> itemSlots = new List<ItemSlot>();

    public static bool addItem(Item item)
    {
        if (items.Contains(item) && 
            items.Find(x => x.Equals(item)).currentStack == items.Find(x => x.Equals(item)).maxStack) 
            return false;
        if (items.Count == capacity) return false;
        items.Add(item);
        return true;
    }
}
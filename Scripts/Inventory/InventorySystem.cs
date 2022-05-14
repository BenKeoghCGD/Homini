using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryKey : System.Object
{
    public InventoryItemData iid;
    public InventoryItem iitem;

    public InventoryKey(InventoryItemData _iid, InventoryItem _ii)
    {
        iid = _iid;
        iitem = _ii;
    }
}

public class InventorySystem : MonoBehaviour
{

    public List<InventoryKey> Inventory { get; private set; } = new List<InventoryKey>();
    public int MaxCapaxity { get; private set; } = 3;

    public void IncrementCapacity(int amount)
    {
        if (amount > 0) MaxCapaxity += amount;
    }

    public bool Add(InventoryItemData iid)
    {
        foreach(InventoryKey i in Inventory)
        {
            if(i.iid == iid && i.iitem.StackSize != iid.maxStack)
            {
                i.iitem.IncreaseStack();
                return true;
            }
        }

        if (Inventory.Count == MaxCapaxity) return false;

        InventoryItem nI = new InventoryItem(iid);
        Inventory.Add(new InventoryKey(iid, nI));
        return true;
    }

    public void Remove(InventoryItemData iid)
    {
        InventoryKey key = null;
        foreach(InventoryKey i in Inventory)
        {
            if(i.iid == iid)
            {
                if(key != null)
                {
                    if(key.iitem.StackSize > i.iitem.StackSize)
                        key = i;
                }
                else
                    key = i;
            }
        }

        if(key != null)
        {
            key.iitem.DecreaseStack();
            if (key.iitem.StackSize == 0) Inventory.Remove(key);
        }
    }

    public bool Get(InventoryItemData iid, out InventoryItem item)
    {
        foreach(InventoryKey i in Inventory)
        {
            if(i.iid == iid)
            {
                item = i.iitem;
                return true;
            }
        }
        item = null;
        return false;
    }
}
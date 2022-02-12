using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarView : InventoryView
{
    private void Start()
    {
        itemSlots = GetComponentsInChildren<ItemSlot>();
    }
    public override void updateDisplay()
    {
        foreach (ItemSlot it in itemSlots)
        {
            if (it.slotID == activeSlot)
                it.highlighted = true;
            else
                it.highlighted = false;
            it.Refresh();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
                activeSlot--;
            else activeSlot++;
        }

        if (activeSlot >= itemSlots.Length) 
            activeSlot = 0;
        else if (activeSlot < 0) 
            activeSlot = itemSlots.Length - 1;
    }

    public override void useKeys()
    {
        for (int i = 1; i < itemSlots.Length + 1; i++)
        {
            if (Input.GetKeyDown("" + i))
                activeSlot = i - 1;
        }
    }
}

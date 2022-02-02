using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryView : MonoBehaviour
{
    protected int slots = 0, activeSlot = 0;
    protected ItemSlot[] itemSlots;

    private void Update()
    {
        updateDisplay();
        useMouse();
        useKeys();
    }

    public int getActiveSlot() { return activeSlot; }
    public ItemSlot[] getContents() { return itemSlots; }

    // Use click-and-point, or use controls (i.e. scroll wheel)
    public virtual void useMouse() {}
    public virtual void useKeys() {}

    // Display
    public abstract void updateDisplay();
}
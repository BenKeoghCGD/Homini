[System.Serializable]
public class InventoryItem
{
    public int StackSize { get; private set; }
    public InventoryItemData IID { get; private set; }

    public InventoryItem(InventoryItemData iid)
    {
        IID = iid;
        IncreaseStack();
    }

    public void IncreaseStack() => StackSize++;
    public void DecreaseStack() => StackSize--;
}
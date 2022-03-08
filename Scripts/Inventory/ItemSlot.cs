using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int slotID = 0, itemAmount = 0, maxCapacity = 0;
    [HideInInspector] public bool highlighted = false;
    public Item item;

    public GameObject highlight, icon, amnt;

    public void Refresh()
    {
        if (Inventory.items.Count > 0)
             item = Inventory.items[0];
        else item = null;

        highlight.SetActive(highlighted);
        if (itemAmount < 2)
             amnt.SetActive(false);
        else amnt.SetActive(true);

        if (item != null)
             icon.GetComponent<Image>().sprite = item.icon;
        else icon.GetComponent<Image>().sprite = null;
        amnt.GetComponent<Text>().text = "x" + itemAmount;
    }
}
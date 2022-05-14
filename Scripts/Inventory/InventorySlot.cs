#pragma warning disable 0649

using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItemData IID = null;
    public bool highlighted = false;
    public GameObject Icon, Highlight, Amount;
    [HideInInspector] public int amnt;

    public void Refresh()
    {
        if (IID)
        {
            Icon.SetActive(IID.icon != null);
            Icon.GetComponent<Image>().sprite = IID.icon;
        }
        Highlight.SetActive(highlighted);

        Amount.GetComponent<Text>().text = amnt + "x";
        Amount.SetActive(amnt > 1);
    }
}

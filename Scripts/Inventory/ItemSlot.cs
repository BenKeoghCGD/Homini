using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int slotID = 0;
    [HideInInspector] public bool highlighted = false;

    public GameObject highlight, icon;

    public void Refresh()
    {
        highlight.SetActive(highlighted);
    }
}
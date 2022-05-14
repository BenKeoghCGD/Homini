using System.Collections.Generic;
using UnityEngine;
using NMaterial;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab = null;
    readonly List<InventorySlot> slotPool = new List<InventorySlot>();
    public InventoryKey currentItem;
    InventorySystem _iSys = null;
    bool errored = false;
    int selected = 0;

    private void Awake()
    {
        Mat.LoadPrefabs();
    }

    void Update()
    {
        _iSys = FindObjectOfType<InventorySystem>();

        //if((_iSys ?? false) && (slotPrefab ?? false))
        if ((_iSys != null) && (slotPrefab != null))
        {
            RefreshHotbar(); 
        }
        else if (!errored)
        {
            errored = true;
            Debug.LogError("[INV] Couldn't find InventorySystem.");
        }
    }

    void RefreshHotbar()
    {
        if(slotPool.Count < _iSys.MaxCapaxity)
        {
            while(slotPool.Count < _iSys.MaxCapaxity)
            {
                GameObject go = Instantiate(slotPrefab, transform);
                slotPool.Add(go.GetComponent<InventorySlot>());
            }
        }

        int index = 0;
        // 48 and 57 enclose all 1-0 alpha keys
        for(int i = 48; i <= 57; i++)
        {
            if (Input.GetKeyDown((KeyCode) i)) selected = index-1;
            index++;
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) selected++;
            else selected--;
        }

        if (selected >= slotPool.Count) selected = 0;
        else if (selected < 0) selected = slotPool.Count - 1;

        for(int i = 0; i < slotPool.Count; i++)
        {
            if (i <= (_iSys.Inventory.Count - 1))
            {
                slotPool[i].IID = _iSys.Inventory[i].iitem.IID;
                slotPool[i].amnt = _iSys.Inventory[i].iitem.StackSize;
                if (selected == i) currentItem = _iSys.Inventory[i];
            }
            else currentItem = null;
            
            slotPool[i].highlighted = selected == i;
            slotPool[i].Refresh();
        }
    }
}
using NMaterial;
using UnityEngine;

public class Tree : MonoBehaviour, IHarvestable, IInteractable
{
    STree _instance;
    private bool _CanInteract = false;
    public bool canInteract { get => _CanInteract; set => _CanInteract = value; }

    public void Init(STree baseInstance) => _instance = baseInstance;

    public void Harvest()
    {
        int yield = (int)((_instance.gameObject.transform.localScale.x / _instance.maxScale) * _instance.maxYield);
        
        InventoryItemData iid = Mat.getIID(Materials.WOOD);
        GameObject o = Instantiate(iid.prefab, transform.position + new Vector3(0f, 1f, 0f), new Quaternion());
        o.GetComponent<WoodItem>().Init(yield);
        FindObjectOfType<PlantManager>().HarvestTree(this.gameObject);
    }

    public void Interact()
    {
        Harvest();
    }
}
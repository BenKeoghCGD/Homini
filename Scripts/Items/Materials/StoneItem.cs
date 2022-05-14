using UnityEngine;
using NMaterial;
using System.Collections;

public class StoneItem : MonoBehaviour, IInteractable
{
    int yield = 0;
    private bool _CanInteract = true;
    public bool canInteract { get => _CanInteract; set => _CanInteract = value; }

    public void Init(int yield) => this.yield = yield;

    public void Interact() => StartCoroutine(interact());

    IEnumerator interact()
    {
        while (yield > 0)
        {
            if (FindObjectOfType<InventorySystem>().Add(Mat.getIID(Materials.STONE))) yield--;
            else break;
            yield return null;
        }

        if (yield == 0)
            Destroy(this.gameObject);
    }
}
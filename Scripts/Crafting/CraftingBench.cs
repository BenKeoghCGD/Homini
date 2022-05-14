using UnityEngine;
using NMaterial;
using System.Collections.Generic;

public class CraftingBench : MonoBehaviour, IInteractable
{
    bool _canInteract = true;
    public bool canInteract { get => _canInteract; set => _canInteract = value; }

    public CraftingRecipe currentRecipe;
    InventorySystem _isys;

    private void Start()
    {
        _isys = FindObjectOfType<InventorySystem>();
    }

    public void Interact()
    {
        if(currentRecipe != null && _isys != null)
        {
            bool canCraft = true;
            foreach(RecipeRequirements pair in currentRecipe.requirements)
            {
                InventoryItem ii;
                if(_isys.Get(pair.iid, out ii))
                {
                    if (ii.StackSize >= pair.amount) continue;
                    else canCraft = false;
                }
            }

            if(canCraft)
            {
                foreach (RecipeRequirements pair in currentRecipe.requirements)
                {
                    int iteration = 0;
                    while(iteration < pair.amount)
                    {
                        _isys.Remove(pair.iid);
                        iteration++;
                    }
                }

                _isys.Add(currentRecipe.output);
            }
        }
    }
}
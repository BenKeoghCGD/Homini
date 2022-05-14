using UnityEngine;
using NMaterial;
using System.Collections.Generic;

[System.Serializable]
public class RecipeRequirements : System.Object
{
    public InventoryItemData iid;
    public int amount;
}

[CreateAssetMenu(menuName = "Crafting Recipe (CR)")]
public class CraftingRecipe : ScriptableObject
{
    public List<RecipeRequirements> requirements;
    public InventoryItemData output;
}
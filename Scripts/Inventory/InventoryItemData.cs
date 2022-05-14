using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data (IID)")]
public class InventoryItemData : ScriptableObject
{
	public string id;
	public string displayName;
	public int maxStack;
	public Sprite icon;
	public GameObject prefab;
}
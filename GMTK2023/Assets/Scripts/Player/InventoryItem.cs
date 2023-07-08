using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public string Name { get; set; }

    public int MaxAmount { get; set; }

    public ItemCraftType CraftType { get; set; }
}

public enum ItemCraftType
{
    Forge,
    Workbench,
}
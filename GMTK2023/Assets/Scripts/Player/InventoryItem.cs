using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item")]
public class InventoryItem : ScriptableObject
{
    [SerializeField] private InventoryItem craftResult;
    [SerializeField] private string itemName;
    [SerializeField] private int maxAmount;
    [SerializeField] private ItemCraftType type;

    public string Name => itemName;

    public int MaxAmount => maxAmount;

    public ItemCraftType CraftType => type;

    public InventoryItem CraftResult => craftResult;
}

public enum ItemCraftType
{
    Forge,
    Workbench,
}
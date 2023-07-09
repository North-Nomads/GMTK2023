using System;
using UnityEngine;

public abstract class PlaceableBlock : MonoBehaviour
{
    [SerializeField] private int additionalStress;
    public Block ParentBlock { get; set; }
    
    public int AdditionalStress => additionalStress;

    public abstract void ProcessTick();

    public abstract ItemsPack PickItemsUp();
}

public readonly struct ItemsPack
{
    public int Count { get; }

    public ItemType Item { get; }

    public ItemsPack(ItemType item, int count)
    {
        Item = item;
        Count = count;
    }
}

[Serializable]
public enum ItemType
{
    Stone, 
    Iron
}
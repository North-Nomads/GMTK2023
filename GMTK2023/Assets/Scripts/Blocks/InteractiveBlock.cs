using UnityEngine;

public class InteractiveBlock : PlaceableBlock
{
    [SerializeField] private InventoryItem ironItem;
    [SerializeField] private int craftTime;
    private int _inputItemsAmount;
    private int _processedItemsAmount;
    private float _currentCraftTime;

    public bool IsCrafting => _inputItemsAmount > 0;
    
    public void PlaceItems(int amount)
    {
        _inputItemsAmount += amount;
    }

    public override ItemsPack PickItemsUp()
    {
        var item = new ItemsPack(ironItem, _processedItemsAmount);
        _inputItemsAmount -= _processedItemsAmount;
        return item;
    }

    public override void ProcessTick()
    {
        if (!IsCrafting) return;
        
        _currentCraftTime += Time.deltaTime;
        if (_currentCraftTime > craftTime)
        {
            _processedItemsAmount++;
            _inputItemsAmount--;
            _currentCraftTime = 0;
        }
    }
}
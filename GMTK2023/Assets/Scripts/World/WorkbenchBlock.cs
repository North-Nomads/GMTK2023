using UnityEngine;

public class WorkbenchBlock : PlaceableBlock
{
    private int _currentItemsAmount;
    private int _craftedAmount;

    public bool IsCrafting => CraftTime > 0;
    public float CraftTime { get; set; }

    public void PlaceItems(int amount)
    {
        _currentItemsAmount += amount;
        CraftTime = Random.Range(_currentItemsAmount, _currentItemsAmount * 3);
    }

    public override ItemsPack PickItemsUp()
    {
        int amount = _craftedAmount;
        _craftedAmount = 0;
        return new(ItemType.Iron, amount);
    }

    public override void ProcessTick()
    {
        if (IsCrafting)
        {
            CraftTime -= Time.deltaTime;
            if (CraftTime > 0)
            {
                _craftedAmount = _currentItemsAmount / 2;
                _currentItemsAmount = 0;
            }
        }
        else if (_currentItemsAmount > 0)
        {
            CraftTime = UnityEngine.Random.Range(_currentItemsAmount, _currentItemsAmount * 3);
        }
    }
}
using System;
using System.Linq;
using UnityEngine;

public class WorkbenchBlock : PlaceableBlock
{
    private int currentItemsAmount;
    private int craftedAmount;

    public int CraftResultAmount { get; set; }

    public bool IsCrafting => CraftTime > 0;

    public float CraftTime { get; set; }

    public int CraftAmount { get; set; }

    public void PlaceItems(int amount)
    {
        currentItemsAmount += amount;
        CraftTime = UnityEngine.Random.Range(currentItemsAmount, currentItemsAmount * 3);
    }

    public override ItemsPack PickItemsUp()
    {
        int amount = craftedAmount;
        craftedAmount = 0;
        return new(ItemType.Iron, amount);
    }

    public override void ProcessTick()
    {
        if (IsCrafting)
        {
            CraftTime -= Time.deltaTime;
            if (CraftTime > 0)
            {
                craftedAmount = currentItemsAmount / 2;
                currentItemsAmount = 0;
            }
        }
        else if (currentItemsAmount > 0)
        {
            CraftTime = UnityEngine.Random.Range(currentItemsAmount, currentItemsAmount * 3);
        }
    }
}
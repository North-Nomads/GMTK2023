using System;
using System.Linq;
using UnityEngine;

public class WorkbenchBlock : PlaceableBlock
{
    private int currentItemsAmount;
    private float currentCraftTime;
    private int craftedAmount;

    public InventoryItem CraftResult { get; set; }

    public int CraftResultAmount { get; set; }

    public bool IsCrafting => currentItemsAmount > CraftAmount;

    public int CraftTime { get; set; }

    public int CraftAmount { get; set; }

    public InventoryItem CraftResource { get; set; }

    public void PlaceItems(int amount)
    {
        currentItemsAmount += amount;
    }

    public override ItemsPack PickItemsUp()
    {
        int amount = craftedAmount;
        craftedAmount = 0;
        return new(CraftResult, amount);
    }

    public override void ProcessTick()
    {
        if (IsCrafting)
        {
            currentCraftTime += Time.deltaTime;
            if (currentCraftTime > CraftTime)
            {
                craftedAmount += CraftResultAmount;
                currentItemsAmount -= CraftAmount;
                currentCraftTime = 0;
            }
        }
    }
}
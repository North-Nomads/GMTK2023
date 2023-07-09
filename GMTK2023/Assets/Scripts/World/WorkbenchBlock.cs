using UnityEngine;

public class WorkbenchBlock : PlaceableBlock
{
    public bool IsCrafting => CraftTime > 0;
    public float CraftTime { get; set; }

    public void Start()
    {
        CraftTime = Random.Range(3, 10.0f);
    }
    public override ItemsPack PickItemsUp()
    {
        return new(ItemType.Iron, 1);
    }

    public override void ProcessTick()
    {
        if (IsCrafting)
        {
            CraftTime -= Time.deltaTime;
        }
    }
}
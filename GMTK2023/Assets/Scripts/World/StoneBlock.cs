using UnityEngine;

namespace World
{
    public class StoneBlock : PlaceableBlock
    {
        [SerializeField] private int amount;
        [SerializeField] private GameObject prefab;
        [SerializeField] private InventoryItem inventoryitem;
        
        public int Amount
        {
            get => amount;
            set => amount = value;
        }
        public GameObject Prefab => prefab;

        public override void ProcessTick()
        { }

        public override ItemsPack PickItemsUp()
        {
            return new ItemsPack(inventoryitem, amount);
        }
    }
}
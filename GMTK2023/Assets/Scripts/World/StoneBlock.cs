using UnityEngine;

namespace World
{
    public class StoneBlock : PlaceableBlock
    {
        private readonly ItemType inventoryItem = ItemType.Stone;

        [SerializeField] private int amount;
        
        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        public override void ProcessTick()
        { }

        public override ItemsPack PickItemsUp()
        {
            return new ItemsPack(inventoryItem, amount);
        }
    }
}
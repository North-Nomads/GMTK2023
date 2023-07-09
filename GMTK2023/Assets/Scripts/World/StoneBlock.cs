using UnityEngine;

namespace World
{
    public class StoneBlock : PlaceableBlock
    {
        private readonly ItemType inventoryItem = ItemType.Stone;

        [SerializeField] private int amount;
        [SerializeField] private GameObject prefab;
        
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
            return new ItemsPack(inventoryItem, amount);
        }
    }
}
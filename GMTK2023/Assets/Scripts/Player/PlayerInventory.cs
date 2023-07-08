using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory : MonoBehaviour
{
    private InventorySlot[] inventorySlots;

    public int Size
    {
        get => inventorySlots.Length;
        set => Array.Resize(ref inventorySlots, value);
    }

    public PlayerInventory(int size)
    {
        inventorySlots = new InventorySlot[size];
    }

    public void AddItems(ItemsPack item)
    {
        AddItems(item.Item, item.Count);
    }

    public void AddItems(InventoryItem item, int amount)
    {
        for (int i = 0; i < inventorySlots.Length && amount > 0; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot.Item == null || slot.Item == item)
            {
                amount = slot.AddItems(item, amount);
            }
        }
    }

    public int CountItems(InventoryItem item) => inventorySlots.Where(x => x.Item == item).Sum(x => x.ItemsAmount);

    public void RemoveItems(InventoryItem item, int amount)
    {
        for (int i = 0; i < inventorySlots.Length && amount > 0; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot.Item == item)
            {
                amount = slot.RemoveItems(amount);
            }
        }
    }

    public bool Contains(InventoryItem item) => inventorySlots.Any(x => x.Item == item);

    public class InventorySlot
    {
        private int itemsAmount;

        public InventoryItem Item { get; set; }

        public int ItemsAmount
        {
            get => itemsAmount;
            set
            {
                if (value == 0)
                {
                    Item = null;
                }
                else if (Item == null)
                    throw new ArgumentException("Item type cannot be null when setting amount of items.", nameof(Item));
                if (value > Item.MaxAmount)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be more than maximal allowed amount for this type of items.");
                itemsAmount = value;
            }
        }

        /// <summary>
        /// Performs items addition into a slot.
        /// </summary>
        /// <param name="item">Item type.</param>
        /// <param name="amount">Amount of items to add.</param>
        /// <returns>Amount of rest of items after addition.</returns>
        /// <exception cref="ArgumentException">Throws when item types don't match.</exception>
        public int AddItems(InventoryItem item, int amount)
        {
            if (Item == item)
            {
                // Get amount of items that will remain after adding.
                int remainder = amount - (Item.MaxAmount - ItemsAmount);
                // Add rest of items (as much as we can)
                ItemsAmount += amount - remainder;
                return remainder;
            }
            else if (Item == null)
            {
                Item = item;
                int remainder = Item.MaxAmount - amount;
                ItemsAmount += amount - remainder;
                return remainder;
            }
            else throw new ArgumentException("Item types don't match.", nameof(item));
        }

        /// <summary>
        /// Removes amount of items from the slot.
        /// </summary>
        /// <param name="amount">Amount of items to remove.</param>
        /// <returns>Amount of items that remains after removal. Set to <see langword="0"/> if slot can remove all of requested amount.</returns>
        public int RemoveItems(int amount)
        {
            ItemsAmount -= amount;
            if (ItemsAmount < 0)
            {
                int result = -ItemsAmount;
                ItemsAmount = 0;
                return result;
            }
            return 0;
        }
    }
}
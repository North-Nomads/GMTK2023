using System;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int stonesAmount;
    private int ironAmount;

    public int Capacity => 8;

    public int Stones
    {
        get => stonesAmount;
        set 
        {
            if (Capacity - ironAmount < value)
            {
                stonesAmount = Capacity - ironAmount;
                return;
            }
            stonesAmount = value;
        }
    }

    public int Iron
    {
        get => ironAmount;
        set
        {
            if (Capacity - stonesAmount < value)
            { 
                ironAmount = Capacity - stonesAmount;
                return;
            }
            ironAmount = value;
        }
    }

    public bool IsFull => stonesAmount + ironAmount == Capacity;

    public void Clear() => stonesAmount = ironAmount = 0;
}
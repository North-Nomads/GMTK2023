using System;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int _stonesAmount;
    private int _ironAmount;

    public int Capacity => 8;

    public int Stones
    {
        get => _stonesAmount;
        set 
        {
            if (Capacity - _ironAmount < value)
            {
                _stonesAmount = Capacity - _ironAmount;
                return;
            }
            _stonesAmount = value;
        }
    }

    public int Iron
    {
        get => _ironAmount;
        set
        {
            if (Capacity - _stonesAmount < value)
            { 
                _ironAmount = Capacity - _stonesAmount;
                return;
            }
            _ironAmount = value;
        }
    }

    public bool IsFull => _stonesAmount + _ironAmount == Capacity;

    public void Clear() => _stonesAmount = _ironAmount = 0;
}
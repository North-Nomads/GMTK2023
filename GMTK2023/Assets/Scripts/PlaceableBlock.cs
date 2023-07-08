using UnityEngine;

public abstract class PlaceableBlock : MonoBehaviour
{
    [SerializeField] private int additionalStress;

    public int AdditionalStress => additionalStress;

    public abstract void ProcessTick();
}
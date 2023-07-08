using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float defaultStressValue;
    [SerializeField] private Transform entityAnchor;
    private PlaceableBlock _entityBlock;
    private bool _isEnabledForPlayer = true;

    public float GetBlockStress => _entityBlock.AdditionalStress + defaultStressValue;

    public PlaceableBlock PlacedBlock
    {
        get => _entityBlock;
        set => _entityBlock = value;
    }

    public bool IsEnabledForPlayer
    {
        get => _isEnabledForPlayer;
        set => _isEnabledForPlayer = value;
    }

    public void ProcessEntityBlock()
    {
        _entityBlock.ProcessTick();
    }
}

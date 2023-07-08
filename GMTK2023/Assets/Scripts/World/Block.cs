using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float defaultStressValue;
    [SerializeField] private float scaleFactor;
    [SerializeField] private Transform entityAnchor;
    private Vector2Int _position;
    private PlaceableBlock _entityBlock;
    private bool _isEnabledForPlayer = true;

    public float GetBlockStress => _entityBlock.AdditionalStress + defaultStressValue;

    public float ScaleFactor => scaleFactor;

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

    public void SetPosition(Vector2Int position)
    {
        _position = position;
    }
}
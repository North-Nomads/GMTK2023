using UnityEngine;
using World;

public class Block : MonoBehaviour
{
    [SerializeField] private float defaultStressValue;
    [SerializeField] private float scaleFactor;
    [SerializeField] private Transform entityAnchor;
    private Vector2Int _position;
    private bool _isEnabled;
    private PlaceableBlock _placeableBlock;
    private bool _isEnabledForPlayer = true;
    private MeshRenderer _renderer;

    public Vector2Int Position => _position;
    public Transform EntityAnchor => entityAnchor;

    public bool IsEnabled 
    { 
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            if (_isEnabled)
                _renderer.material.color = new Color(0, 161, 0);
            else
                _renderer.material.color = new Color(161, 0, 0);
        }
    }

    public void Select()
    {
        if (_isEnabled)
            _renderer.material.color = Color.green;
        else
            _renderer.material.color = Color.red;
    }

    public void Hover()
    {
        
    }

    public float GetBlockStress => _placeableBlock.AdditionalStress + defaultStressValue;

    public float ScaleFactor => scaleFactor;

    public PlaceableBlock PlacedBlock
    {
        get => _placeableBlock;
        set => _placeableBlock = value;
    }

    public bool IsEnabledForPlayer
    {
        get => _isEnabledForPlayer;
        set => _isEnabledForPlayer = value;
    }

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void ProcessEntityBlock()
    {
        _placeableBlock.ProcessTick();
    }

    public void SetPosition(Vector2Int position)
    {
        _position = position;
    }

    public void SetPlaceableBlock(StoneBlock stonePrefab)
    {
        var stone = Instantiate(stonePrefab.Prefab, new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z), Quaternion.identity);
        PlacedBlock = stone.GetComponent<StoneBlock>();
        PlacedBlock.ParentBlock = this;
    }

    public void ClearOre()
    {
        Destroy(PlacedBlock.gameObject);
        BlockHolder.Blocks[Position[0], Position[1]].PlacedBlock = null;
    }
}

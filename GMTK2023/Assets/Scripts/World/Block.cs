using UnityEngine;
using World;

public class Block : MonoBehaviour
{
    [SerializeField] private float defaultStressValue;
    [SerializeField] private float scaleFactor;
    [SerializeField] private Transform entityAnchor;
    [SerializeField] private Texture enabledTexture;
    [SerializeField] private Texture enadbledSelectedTexture;
    [SerializeField] private Texture disadbledTexture;
    [SerializeField] private Texture disadbledSelectedTexture;
    private Vector2Int _position;
    private bool _isEnabled;
    private PlaceableBlock _entityBlock;
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
                _renderer.material.SetTexture("_MainTex", enabledTexture);
            else
                _renderer.material.SetTexture("_MainTex", disadbledTexture);
            print("getfuckedbozo");
        }
    }

    public void Select()
    {
        if (_isEnabled)
            _renderer.material.SetTexture("_MainTex", enadbledSelectedTexture);
        else
            _renderer.material.SetTexture("_MainTex", disadbledSelectedTexture);
    }

    public void Hover()
    {
        
    }

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

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void ProcessEntityBlock()
    {
        _entityBlock.ProcessTick();
    }

    public void SetPosition(Vector2Int position)
    {
        _position = position;
    }

    public void SetPlaceableBlock(StoneBlock stonePrefab)
    {
        var stone = Instantiate(stonePrefab.Prefab, new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z), Quaternion.identity);
        PlacedBlock = stone.GetComponent<StoneBlock>();
    }
}

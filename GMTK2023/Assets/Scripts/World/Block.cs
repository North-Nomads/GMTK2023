using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float defaultStressValue;
    [SerializeField] private float scaleFactor;

    [SerializeField] private Texture enabledTexture;
    [SerializeField] private Texture enadbledSelectedTexture;
    [SerializeField] private Texture disadbledTexture;
    [SerializeField] private Texture disadbledSelectedTexture;
    private Vector2Int _position;
    private bool _isEnabled;
    private PlaceableBlock _placeableBlock;
    private bool _isEnabledForPlayer = true;
    private MeshRenderer _renderer;
    public Vector2Int Position => _position;
    public MainStats Stats {get; set;}

    public bool IsEnabled 
    { 
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            if (_isEnabled){
                _renderer.material.SetTexture("_MainTex", enabledTexture);
                BlockHolder.ActiveBlock.Add(this);
                Stats.StressInput += defaultStressValue;
                if(_placeableBlock != null)
                    Stats.StressInput += _placeableBlock.AdditionalStress;
            }
            else{
                _renderer.material.SetTexture("_MainTex", disadbledTexture);
                BlockHolder.ActiveBlock.Remove(this);
                Stats.StressInput -= defaultStressValue;
                if(_placeableBlock != null)
                    Stats.StressInput -= _placeableBlock.AdditionalStress;
            }
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

    public void SetPlaceableBlock(PlaceableBlock stonePrefab)
    {
        var stone = Instantiate(stonePrefab, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.identity);
        PlacedBlock = stone.GetComponent<PlaceableBlock>();
        PlacedBlock.ParentBlock = this;
    }

    public void ClearOre()
    {
        Destroy(PlacedBlock.gameObject);
        BlockHolder.Blocks[Position[0], Position[1]].PlacedBlock = null;
    }
}

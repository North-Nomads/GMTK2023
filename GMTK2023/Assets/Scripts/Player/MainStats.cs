using UnityEngine;

public class MainStats : MonoBehaviour
{
    private float _currentRage;
    private float _currentStress;

    private float _stressInput;
    [SerializeField] private float maxRage; 
    [SerializeField] private float stressPoint; 
    [SerializeField] private MapGenerator mapGenerator;
    private PlayerBehavior player;
    
    public float CurrentRage { get => _currentRage; set => _currentRage = value; }
    public float CurrentStress { get => _currentStress; set => _currentStress = value; }
    public float StressInput { get => _stressInput; set => _stressInput = value; }

    void Start()
    {
        player = mapGenerator.Player;
    }

    void Update()
    {
        _currentStress =  _stressInput / 100;

        _currentRage += (_currentStress > stressPoint ? (_currentStress / 3) : -(_currentStress / 10)) * Time.deltaTime;
        
        if(player == null)
            player = mapGenerator.Player;
        
        //_currentRage += time furnaces did not work
        player.VisibleBlocks.ForEach(bl => {
            if(!BlockHolder.ActiveBlock.Contains(bl))
                _currentRage += 3 * Time.deltaTime;
        });

        if(_currentRage == maxRage)
            Application.Quit();
    }
}

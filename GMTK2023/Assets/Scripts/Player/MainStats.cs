using UnityEngine;

public static class StatsAcessor{
    public static MainStats Stats;

}

public class MainStats : MonoBehaviour
{
    private float _currentRage;
    private float _currentStress;

    private float _stressInput;
    [SerializeField] float maxRage; 
    public float CurrentRage { get => _currentRage; set => _currentRage = value; }
    public float CurrentStress { get => _currentStress; set => _currentStress = value; }
    public float StressInput { get => _stressInput; set => _stressInput = value; }

    void Update()
    {
        _currentStress = (_currentStress + _stressInput) / 2;

        _currentRage += _currentRage - maxRage / 3;

        //_currentRage += time furnaces did not work
        //_currentRage += unloadedTiles in player's vision

        if(_currentRage == maxRage)
            Application.Quit();
    }
}

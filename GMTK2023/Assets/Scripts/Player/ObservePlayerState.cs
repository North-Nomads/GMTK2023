using UnityEngine;
using World;

internal class ObservePlayerState : BasicPlayerState
{
    private float _scanTimer = 3f;
    private float _currentScanTimer;
    public ObservePlayerState(PlayerBehavior player) : base(player)
    {
        _currentScanTimer = _scanTimer;
    }

    public override void Update()
    {
        _currentScanTimer -= Time.deltaTime;
        if (_currentScanTimer >= 0) return;
        
        var blocks = Player.ScanAreaAroundPlayer<StoneBlock>();
        Debug.Log($"Scan + {blocks.Count}");

        var coords = Player.GetNextCoordTowards(blocks[0].Position);
        Debug.Log($"{blocks[0].Position}, {coords}");
        Player.MoveOnPoint(coords);

        
        _currentScanTimer = _scanTimer;

    }
}
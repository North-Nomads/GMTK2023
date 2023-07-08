using UnityEngine;

internal class BuildingPlayerState : BasicPlayerState
{
    private readonly InteractiveBlock _furnace;
    private readonly InteractiveBlock _chest;
    private bool _isFocused;
    private float _buildingTimeLeft;
    private float _movingTimeLeft;
    private int _furnacesLeft;
    private Block _focusBlock;
    private bool _hasReachedFocusPoint;

    public BuildingPlayerState(PlayerBehavior player, InteractiveBlock furnace, InteractiveBlock chest) : base(player)
    {
        _furnace = furnace;
        _chest = chest;
    }

    public override void OnStateEnter()
    {
        _furnacesLeft = Random.Range(1, 2);
        _movingTimeLeft = .5f;
    }


    public override void Update()
    {
        // Если печек больше нет, то выходим из состояния
        if (_furnacesLeft == 0)
        {
            Player.SwitchState<IdlePlayerState>();
            return;
        }

        // Если нет фокуса, то найти ближайшую пустую ячейку
        if (!_isFocused)
        {
            var emptyBlocks = Player.ScanAreaAroundPlayer();
            var nearestBlock = Player.GetClosetBlock(emptyBlocks);
            _focusBlock = nearestBlock;
            _isFocused = true;
        }
        // Если ячейка найдена - идем к ней с промежутком в movingTimeLeft секунд
        else if (_isFocused && !_hasReachedFocusPoint)
        {
            _movingTimeLeft -= Time.deltaTime;
            if (_movingTimeLeft >= 0)
                return;
            
            var newPosition = Player.GetNextCoordTowards(_focusBlock.Position);
            Player.MoveOnPoint(newPosition);

            if (Player.PlayerPosition == _focusBlock.Position)
                _hasReachedFocusPoint = true;
        }
        else
        {
            _buildingTimeLeft -= Time.deltaTime;
            if (_buildingTimeLeft > 0)
                return;
            MapGenerator.InstantiateInteractableObject(_furnace, _focusBlock);
        }
        


    }

    
}
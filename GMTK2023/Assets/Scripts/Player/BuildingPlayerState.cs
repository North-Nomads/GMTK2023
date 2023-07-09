using UnityEngine;

internal class BuildingPlayerState : BasicPlayerState
{
    private readonly WorkbenchBlock _furnace;
    private readonly ChestBlock _chest;
    private bool _isFocused;
    private float _buildingTimeLeft;
    private float _movingTimeLeft;
    private int _furnacesLeft;
    private int _chestLeft;
    private Block _focusBlock;
    private bool _hasReachedFocusPoint;

    public BuildingPlayerState(PlayerBehavior player, WorkbenchBlock furnace, ChestBlock chest) : base(player)
    {
        _furnace = furnace;
        _chest = chest;
    }

    public override void OnStateEnter()
    {
        _furnacesLeft = Random.Range(0, 3);
        _chestLeft = Random.Range(0, 3);
        _movingTimeLeft = .5f;
    }


    public override void Update()
    {
        // Если печек больше нет, то выходим из состояния
        if (_furnacesLeft == 0 && _chestLeft == 0)
        {
            Player.SwitchState<IdlePlayerState>();
            Player.Inventory.Clear();
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
        else if (!_hasReachedFocusPoint)
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
            // Определяем, что ставить
            bool craftChest = false;
            if (_chestLeft > 0)
            {
                if (Random.value > 0.5) craftChest = true;
            }
            if (_furnacesLeft == 0)
            {
                craftChest = true;
            }
            // Ставим блок
            if (craftChest)
            {
                _focusBlock.SetPlaceableBlock(_chest);
                _chestLeft--;
            }
            else
            {
                MapGenerator.InstantiateInteractableObject(_furnace, _focusBlock);
                _furnacesLeft--;
            }
            _focusBlock = null;
            _isFocused = false;
            _hasReachedFocusPoint = false;
        }
    }

    
}
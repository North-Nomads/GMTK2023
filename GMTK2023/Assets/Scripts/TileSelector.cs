using System;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField] MapGenerator mapGenerator;
    private int _startX;
    private int _startY;
    
    private float AbsFloor(float fl)
    {
        if (fl < 0)
            return (float)Math.Floor(fl);
        return (float)Math.Ceiling(fl);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
            {
                if (hit.collider.gameObject.TryGetComponent<Block>(out var target))
                {
                    _startX = target.Position.x;
                    _startY = target.Position.y;
                    target.Select();
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            int endX = 0, endY = 0;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
            {
                if (hit.collider.gameObject.TryGetComponent<Block>(out var target))
                {
                    endX = target.Position.x;
                    endY = target.Position.y;
                }
            }
            

                for (int x = Math.Min(endX, _startX); x < Math.Max(endX, _startX) + 1; x++)
                    for (int y = Math.Min(endY, _startY); y < Math.Max(endY, _startY) + 1; y++){
                        try{
                           BlockHolder.Blocks[x, y].IsEnabled = !BlockHolder.Blocks[x, y].IsEnabled;
                        }
                        catch(Exception){
                            print($"{x}, {y}");
                        }
            }
        }

    }
}

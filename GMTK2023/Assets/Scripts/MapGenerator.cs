using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private int mapSize;
    [SerializeField] private int offset;
    [SerializeField] private GameObject tile;

    void Start()
    {
        for(int i = -1 * mapSize / 2; i < mapSize / 2; i++)
            for (int j = -1 * mapSize / 2; j < mapSize / 2; j++)
                Instantiate(tile, new Vector3(i * offset, 0.5f * offset, j * offset), new Quaternion());
        
    }


    void Update()
    {
        
    }
}

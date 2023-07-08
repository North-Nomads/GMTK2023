using UnityEngine;

namespace World
{
    public class StoneBlock : PlaceableBlock
    {
        [SerializeField] private int size;
        [SerializeField] private GameObject prefab;
        public int Size
        {
            get => size;
            set => size = value;
        }
        public GameObject Prefab { get => prefab; }
        public override void ProcessTick()
        { }
    }
}
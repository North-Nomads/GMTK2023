using System.Linq;

namespace World
{
    public class StoneBlock : PlaceableBlock
    {
        public override IGrouping<InventoryItem, int> PickItemsUp()
        {
            throw new System.NotImplementedException();
        }

        public override void ProcessTick()
        { }
    }
}
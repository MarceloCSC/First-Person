using An01malia.FirstPerson.InventoryModule.Items;

namespace An01malia.FirstPerson.InventoryModule
{
    public interface IContainer
    {
        void SetStartingItems();

        void ClearAllItems();

        void AddItems(Item item);

        void AddToStack(Item item);

        void DivideIntoStacks(Item item);

        void AddRemaining(Item item);

        int CountItems(Item item);
    }
}
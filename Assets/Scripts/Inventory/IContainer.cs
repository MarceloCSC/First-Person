using An01malia.FirstPerson.ItemModule.Items;

namespace An01malia.FirstPerson.InventoryModule
{
    public interface IContainer
    {
        bool IsOpen { get; set; }

        void SetStartingItems();

        void ClearAllItems();

        void AddItems(InventoryItem item);

        void AddToStack(InventoryItem item);

        void DivideIntoStacks(InventoryItem item);

        void AddRemaining(InventoryItem item);

        int CountItems(InventoryItem item);
    }
}
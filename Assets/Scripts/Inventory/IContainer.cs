namespace An01malia.FirstPerson.Inventory
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

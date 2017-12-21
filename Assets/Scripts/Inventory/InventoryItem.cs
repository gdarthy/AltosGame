public class InventoryItem {

    public int ID { get; set; }
    public Item Item { get; set; }
    public int Value { get; set; }

    public InventoryItem(Item item)
    {
        ID = item.GetInstanceID();
        Item = item;
    }

    public InventoryItem(int ID, Item item)
    {
        this.ID = ID;
        Item = item;
    }

}

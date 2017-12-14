public class ItemHolder {

    public string itemName;
    public string itemDescription;
    public string itemResource;
    public ActionType actionType;
    public ItemType itemType;
    public EquipmentType equipmentType;

    public ItemHolder(string itemName, string itemDescription, string itemResource, ActionType actionType, ItemType itemType)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemResource = itemResource;
        this.actionType = actionType;
        this.itemType = itemType;
    }

    public ItemHolder(string itemName, string itemDescription, string itemResource, ActionType actionType, ItemType itemType, EquipmentType equipmentType)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemResource = itemResource;
        this.actionType = actionType;
        this.itemType = itemType;
        this.equipmentType = equipmentType;
    }

    public void SwitchEquipAction()
    {
        if (actionType == ActionType.Equip)
            actionType = ActionType.Unequip;
        else
            actionType = ActionType.Equip;
    }

    public static bool IsEquipable(ItemHolder item)
    {
        if (item.itemType == ItemType.Weapons || item.itemType == ItemType.Armor || item.itemType == ItemType.Tools) return true;
        else return false;
    }
}

public enum ActionType
{
    Eat,
    Equip,
    Unequip,
    Read,
    Drink
}

public enum ItemType
{
    Weapons,
    Armor,
    Tools,
    Potions,
    Books,
    Goods
}

using UnityEngine;

public class Equipment : Item
{

    public EquipmentType equipmentType;
    public bool isWeapon { get; protected set; }
    public bool isEquipped { get; protected set; }

}

public enum EquipmentType
{
    None,
    Head,
    Chest,
    Legs,
    Feet,
    LeftHand,
    RightHand,
    Arms,
    Lenght
}
using UnityEngine;

public class Weapon : Equipment
{
    [Header("Weapon")]
    public int attackStrength;
    public int attackSpeed;
    public bool longRangedWeapon;
    public bool twoHandedWeapon;
    public bool equippedHand { get; protected set; } //0 left, 1 right

    public void SetHand(EquipmentType equippedHand)
    {
        if (equippedHand == EquipmentType.RightHand || equippedHand == EquipmentType.LeftHand)
        {
            equipmentType = equippedHand;
            this.equippedHand = equippedHand == EquipmentType.RightHand;
        }
        else
        {
            Debug.Log("Type is not a hand!");
        }
    }

}

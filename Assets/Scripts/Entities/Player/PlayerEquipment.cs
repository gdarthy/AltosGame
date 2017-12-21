using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment Instance { get; private set; }

    GameObject[] equipment;

    bool twoHandedEquipped = false;
    EquipmentType twoHandedSlot;

    void Awake()
    {
        //Singelton
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        equipment = new GameObject[(int)EquipmentType.Lenght];
    }

    public void Equip(Armor item)
    {

    }

    public void Equip(Weapon item, EquipmentType hand)
    {

        EquipmentType otherHand = hand == EquipmentType.RightHand ? EquipmentType.LeftHand : EquipmentType.RightHand;

        if (item.twoHandedWeapon)
        {
            EquipTwoHanded(item, hand);
        }
        else
        {
            //Checks if two handed weapon is equipped, if true then it unequips it
            if (twoHandedEquipped) Unequip(equipment[(int)twoHandedSlot].GetComponent<Weapon>());

            // Checks if equipping hand is empty, if not unequips current one handed weapon
            else if (GetEquipmentParent(hand).childCount > 0) Unequip(equipment[(int)hand].GetComponent<Weapon>());

            // Checks if weapon is equipped in other hand, if true, unequips weapon
            if (item.isEquipped && item.equipmentType == otherHand) Unequip(item);

            // Equipping (if item is already equipped, equipping will be ignored)
            if (!item.isEquipped)
            {/*
                item.SetHand(hand);
                item.isEquipped = true;
                GameObject _weapon = Instantiate(item.itemPrefab, GetEquipmentParent(hand));
                PlayerInventoryHolder.Instance.DestroyReference(item.oldId);
                _weapon.GetComponent<ItemPhysics>().Alive(true);
                equipment[(int)hand] = _weapon;
                _weapon.GetComponent<Equipment>().isEquipped = true;
                _weapon.GetComponent<Weapon>().SetHand(hand);
                PlayerInventoryHolder.Instance.RemoveFromStack(item);
                _weapon.GetComponent<Weapon>().itemName = item.itemName;*/
            }
        }
    }

    void EquipTwoHanded(Weapon weapon, EquipmentType hand)
    {
        EquipmentType otherHand = hand == EquipmentType.RightHand ? EquipmentType.LeftHand : EquipmentType.RightHand;

        // Checks if weapon is equipped in other hand, if true, unequips weapon
        if (weapon.isEquipped && weapon.equipmentType == otherHand) Unequip(weapon);
        else
        {
            // Unequips all hands if there are any weapons
            if (equipment[(int)hand] != null) Unequip(equipment[(int)hand].GetComponent<Weapon>());
            if (equipment[(int)otherHand] != null) Unequip(equipment[(int)otherHand].GetComponent<Weapon>());
        }

        if (!weapon.isEquipped)
        {/*
            weapon.SetHand(hand);
            twoHandedSlot = hand;
            weapon.isEquipped = true;
            twoHandedEquipped = true;
            GameObject _weapon = Instantiate(weapon.itemPrefab, GetEquipmentParent(hand));
            PlayerInventoryHolder.Instance.DestroyReference(weapon.oldId);
            _weapon.GetComponent<ItemPhysics>().Alive(true);
            equipment[(int)hand] = _weapon;
            _weapon.GetComponent<Equipment>().isEquipped = true;
            _weapon.GetComponent<Weapon>().SetHand(hand);
            PlayerInventoryHolder.Instance.RemoveFromStack(weapon);
            _weapon.GetComponent<Weapon>().itemName = weapon.itemName;*/
        }
    }

    public void Unequip(Equipment item)
    {
        if ((item.itemType == ItemType.Weapons || item.itemType == ItemType.Tools) && ((Weapon)item).twoHandedWeapon)
        {
            twoHandedEquipped = false;
        }
        Destroy(GetEquipmentParent(item.equipmentType).transform.GetChild(0).gameObject);
        equipment[(int)item.equipmentType] = null;
        PlayerInventoryHolder.Instance.AddToStack(item);
        //item.isEquipped = false;
    }

    public bool HasTool(ToolType toolType)
    {
        if (equipment[(int)EquipmentType.RightHand] == null && equipment[(int)EquipmentType.LeftHand] == null)
        {
            return false;
        }
        else if (equipment[(int)EquipmentType.RightHand].GetComponent<Tool>().toolType == toolType)
        {
            return true;
        }
        if (equipment[(int)EquipmentType.LeftHand].GetComponent<Tool>().toolType == toolType)
        {
            return true;
        }
        return false;
    }

    public bool IsEquipped(Item item)
    {
        return (item.isEquipment && ((Equipment)item).isEquipped);
    }

    public void UnequipIfNecessary(Item item)
    {
        if (IsEquipped(item))
        {
            Unequip((Equipment)item);
        }
    }

    Transform GetEquipmentParent(EquipmentType type)
    {
        if (type == EquipmentType.None)
        {
            Debug.Log("No parent");
            return null;
        }
        else
            return GameObject.Find(type.ToString()).transform;
    }
}
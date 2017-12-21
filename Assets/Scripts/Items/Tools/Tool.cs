using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Weapon
{
    public ToolType toolType;
}

public enum ToolType
{
    None,
    Axe,
    Pickaxe
}
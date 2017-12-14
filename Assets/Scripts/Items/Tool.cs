using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    [Header("Tool")]
    public ToolType toolType;
}

public enum ToolType
{
    Axe,
    Pickaxe
}
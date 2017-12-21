using UnityEngine;
using UnityEditor;

public class PickaxeAsset
{
    [MenuItem("Assets/Create/Item/Tools/Pickaxe")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Pickaxe>();
    }
}
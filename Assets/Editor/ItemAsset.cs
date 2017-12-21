using UnityEngine;
using UnityEditor;

public class ItemAsset
{
    [MenuItem("Assets/Create/Item/Item")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Item>();
    }
}
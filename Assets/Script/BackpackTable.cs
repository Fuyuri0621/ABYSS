using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Backpack/backpackTable",fileName ="BackpackTable")]
public class BackpackTable : ScriptableObject
{
    public List<BackpackTableItem> DataList = new List<BackpackTableItem>();
}

[Serializable]
public class BackpackTableItem
{
    public string name;

    public int type;

    public string description;

    public string iconPath;
}
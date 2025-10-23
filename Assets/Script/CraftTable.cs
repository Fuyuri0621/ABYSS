using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft/CraftTable", fileName = "CraftTable")]
public class CraftTable : ScriptableObject
{
    public List<CraftTableItem> DataList = new List<CraftTableItem>();
}

[Serializable]
public class CraftTableItem
{

    public string name;

    public string description;

    public string iconPath;

    public List<CraftNeedItem> needItems;

}

[Serializable]
public class CraftNeedItem
{

    public string itemName;

    public int require;

        


}

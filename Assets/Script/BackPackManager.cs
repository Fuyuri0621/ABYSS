using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackManager : MonoBehaviour
{

    [SerializeField]BackpackContainer backpack;
    [SerializeField] BackpackTable backpackTable;
    [SerializeField] CraftTable craftTable;


    private static BackPackManager _instance;
    public static BackPackManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AddItem(string tragetName, int count)
    {
        BackpackItem traget = backpack.items.Find(x => x.itemName == tragetName);
        if (traget != null)
        {
            traget.itemcount += count;
        }
        else
        {
            AddNewItem(tragetName,count);
        }
    }

    private void AddNewItem(string tragetName, int count)
    {
        backpack.items.Add(new BackpackItem(tragetName, count));
    }

    int GetItemCount(string tragetName)
    {
        BackpackItem traget = backpack.items.Find(x => x.itemName == tragetName);
        if (traget != null)
            return traget.itemcount;
        else return 0;
    }

    public List<BackpackItem> GetBackPackLocalData()
    {
        return backpack.items;
    }
   public BackpackTableItem GetBackpackTableItemByName(string tragetname) 
    {
        BackpackTableItem t = backpackTable.DataList.Find(x => x.name == tragetname);
        if (t != null) return t;
        else
        return null ; 
    }
    public CraftTable GetCraftTable()
    {
        return craftTable;
    }
    public int GetBackpackItemCountByName(string tragetname)
    {
        BackpackItem t = backpack.items.Find(x => x.itemName == tragetname);
        if (t != null) return t.itemcount;
        else
            return 0;
    }
}

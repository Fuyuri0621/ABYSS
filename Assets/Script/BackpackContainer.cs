using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
[CreateAssetMenu]

public class BackpackContainer : ScriptableObject
{
    [SerializeField] public List<BackpackItem> items;
}



[Serializable]
public class BackpackItem
{
    public string itemName;
    public int itemcount;

    public BackpackItem(string Name, int Count)
    {
        itemName = Name;
        itemcount = Count;
    }
}

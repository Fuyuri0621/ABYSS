using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IPickUpObject
{

   [SerializeField] string itemName;
   [SerializeField] int count;
    public void OnPickUP(BackPackManager backpack)
    {
        backpack.AddItem(itemName,count);
    }
}

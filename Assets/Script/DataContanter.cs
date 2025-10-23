using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Datacontainer", menuName = "Scriptable Objects/Datacontainer")]
public class Datacontainer : ScriptableObject
{
    public bool coopMode;

    public List<bool> levelclear;

    public List<string> craftedWeapon;
}


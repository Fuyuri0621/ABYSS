using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewComboConfig", menuName = "ComboSystem/CreateNewComboConfig")]
public class ComboConfig : ScriptableObject
{
    public string animatorStateName;
    public string effectName;
    public int damagerate;
    public float knockbackrate;
}

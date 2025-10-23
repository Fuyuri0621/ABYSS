using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MovementSetting : ScriptableObject
{
    [SerializeField, Range(0f, 20f)] public float maxSpeed = 15f;
    [SerializeField, Range(0f, 100f)] public float maxAcceleration = 60f;
    [SerializeField, Range(0f, 100f)] public float maxDecceleration = 40f;
    [SerializeField, Range(0f, 100f)] public float maxTurnSpeed = 30f;
    [SerializeField, Range(0f, 100f)] public float maxAirAcceleration=10f;
    [SerializeField, Range(0f, 100f)] public float maxAirDeceleration=5f;
    [SerializeField, Range(0f, 100f)] public float maxAirTurnSpeed = 10f;
    [SerializeField, Range(0f, 100f)] public float jumpHeight = 5f;
    [SerializeField, Range(0, 2)] public int maxAirJumps = 1;
    [Range(0.4f, 1.25f)][Tooltip("到最高點的時間")] public float timeToJumpApex =0.4f;

}

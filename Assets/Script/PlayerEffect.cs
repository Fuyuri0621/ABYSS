using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    PlayerMovent playerMovent;
    Character character;

   


    private void Awake()
    {
        playerMovent = GetComponent<PlayerMovent>();
        character = GetComponent<Character>();
    }

    public void SpeedUP(float amout,float duration)
    {
        playerMovent.maxSpeed += amout;

        StartCoroutine(WaitForSeconds(duration, () => { playerMovent.maxSpeed -= amout; }));
    }
    public void AddShield(float duration)
    {
        character.haveShield =true;

        StartCoroutine(WaitForSeconds(duration, () => { if (character.haveShield) character.haveShield = false; }));
    }

    public void Invincible(float duration)
    {
        character.invincible = true;

        StartCoroutine(WaitForSeconds(duration, () => { character.invincible=false; }));
    }

    public void SlowFall(float amout, float duration)
    {
        playerMovent.downwardGravity -= amout;

        StartCoroutine(WaitForSeconds(duration, () => { playerMovent.downwardGravity += amout; }));
    }
    public static IEnumerator WaitForSeconds(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }

    internal void AddTime(float v)
    {
       FindObjectOfType<StageTime>().time += v;
    }
}

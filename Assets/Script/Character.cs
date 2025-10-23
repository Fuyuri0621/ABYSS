using System;
using System.Collections;
using  System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
   public bool haveShield =false;
  public  bool invincible = false;
    public int maxHp = 10;
    public int currentHp = 10;

    Rigidbody2D rb;
    //public float hpRegenerationRate = 1f;
    //public float hpRegenerationTimer;
    [SerializeField] UIBar hpBar;

    public GameObject failpanel;

    private bool isDead = false;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        hpBar.UpdateSlider("HpBar", currentHp,maxHp);
    }



    public void TakeDamage(int damage, Transform attacker)
    {
        if (isDead == true) { return; }
        if (invincible) { return; }
        if (haveShield) { haveShield = false;return; }


        currentHp -= damage;
        knockback(attacker);
        if (currentHp <= 0)
        {
            isDead=true;
            StartCoroutine(WaitForSeconds(3, () =>
            {
                GameManager.Instance.Gameover();
            }));

            
        }
        hpBar.UpdateSlider("HpBar", currentHp,maxHp);
    }

    private void knockback(Transform attacker)
    {
        if (attacker == null) { return; }
        Vector2 knockbackDirection = (transform.position.x < attacker.position.x)
                     ? new Vector2(-1.5f, 0.5f)  // 往左上彈
                     : new Vector2(1.5f,0.51f);  // 往右上彈

        knockbackDirection.Normalize();

        rb.velocity = Vector2.zero;

        rb.AddForce(knockbackDirection * 15, ForceMode2D.Impulse);
    }

    public static IEnumerator WaitForSeconds(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }


    public void Heal(int amount) 
    {
        if (currentHp <= 0) { return; }

        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBar.UpdateSlider("HpBar", currentHp, maxHp);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[RequireComponent(typeof(UIBar))]
[RequireComponent(typeof(BattleAI))]
public class BattleEnemy : MonoBehaviour ,IDamageable
{
   public BattleEnemyState state;

   public EnemyManger manger;

    [SerializeField] private float knockbackForce = 5f;
    Animator animator;
    Rigidbody2D rb;
    UIBar uIBar;
    BattleAI aI;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uIBar = GetComponent<UIBar>();
        aI = GetComponent<BattleAI>();

    }
    void Start()
    {

        uIBar.UpdateSlider("HP", state.currenthp, state.maxhp);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage,Transform attacker)
    {
        if(state.currenthp <=0) return;

        animator.Play("damaged");

        state.currenthp-=damage;
        if(state.currenthp <= 0){ aI.enabled = false; animator.Play("dead"); }

        uIBar.UpdateSlider("HP",state.currenthp,state.maxhp);

        knockback(attacker);

    }

    private void knockback(Transform attacker)
    {
        Vector2 knockbackDirection = (transform.position.x < attacker.position.x)
                     ? new Vector2(-1, 1.2f)  // 往左上彈
                     : new Vector2(1, 1.2f);  // 往右上彈

        knockbackDirection.Normalize();


        rb.velocity = Vector2.zero;

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }


    public void dystroyself()
    {
        manger.enemycount--;
        Destroy(gameObject);
    }

}
[Serializable]
public class BattleEnemyState
{
    public EnemyAIstate state;
    public int maxhp;
    public int currenthp;
}

public enum EnemyAIstate { idle,pursuit, attack,dead}

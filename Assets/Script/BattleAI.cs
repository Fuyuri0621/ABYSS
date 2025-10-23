
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BattleAI : MonoBehaviour
{
    Transform target;
    BattleEnemyState state;

    [Header("移動與攻擊屬性")]
    public bool allowmoving =true;
    [SerializeField] float speed = 2f;
    [SerializeField] float attackRange = 1f;    // 近戰攻擊距離
    [SerializeField] float rangeToShoot = 3f;   // 遠距離攻擊最小距離
    [SerializeField] float pursuitOffset = 2f;  // 接近追擊位置的偏移

    [Header("冷卻")]
    [SerializeField] float attackCD = 1f;
    float currentCD = 0f;

    Animator animator;

    [Header("攻擊動畫列表")]
    [SerializeField] List<string> meleeAttackList;
    [SerializeField] List<string> rangeAttackList;



    void Start()
    {
        animator = GetComponent<Animator>();

        state = GetComponent<BattleEnemy>().state;

        var go = GameObject.FindGameObjectWithTag("Player");
        if (go != null) target = go.transform;
        else Debug.LogError("BattleAI: 找不到 Tag 為 Player 的物件");
    }

    // Update is called once per frame
    void Update()
    {
        currentCD = Mathf.Max(0f, currentCD - Time.deltaTime);

        switch (state.state)
        {
            case EnemyAIstate.idle:
                IdleMode();
                break;
            case EnemyAIstate.pursuit:
                PursuitMode();
                break;
            case EnemyAIstate.attack:
                AttackMode();
                break;
            case EnemyAIstate.dead:
                HandleDeath();
                break;
        }

        UpdateFlip();
    }
private void HandleDeath()
    {
        this.enabled = false;
    }

    private void AttackMode()
    {
        

        float dx = target.position.x - transform.position.x;
        float absDx = Mathf.Abs(dx);

            if (absDx <= attackRange)
            {
                animator.Play(RandomFromList(meleeAttackList));
                currentCD = attackCD;
                state.state = EnemyAIstate.pursuit;
            }
            else
            {
                animator.Play(RandomFromList(rangeAttackList));
                currentCD = attackCD;
                state.state = EnemyAIstate.pursuit;
            }

        
    }

    

    private void IdleMode()
    {
        float dx = Mathf.Abs(target.position.x - transform.position.x);
        if (dx < rangeToShoot * 1.5f)  // 發現距離
        {
            state.state = EnemyAIstate.pursuit;
        }
    }

    private void PursuitMode()
    {
        speed = 2f;
        if (target != null)
        {
            float dx = target.position.x - transform.position.x;
            float absDx = Mathf.Abs(dx);

            if (currentCD <= 0f)
            {
                 state.state = EnemyAIstate.attack;
                return;
            }

            float dir = Mathf.Sign(dx);
            Vector2 desiredPos = (Vector2)target.position - Vector2.right * dir * pursuitOffset;
            float distanceToDesired = Mathf.Abs(desiredPos.x - transform.position.x);
            if (distanceToDesired > 0.2f)
            {
                float moveDir = Mathf.Sign(desiredPos.x - transform.position.x);
                MoveTowardsDirection(moveDir);
            }
            else
            {
                // 停止移動，可能加 idle 動畫
            }


        }
    }

    private void MoveTowardsDirection(float direction)
    {
        // direction = -1 向左, +1 向右
        if (allowmoving)
        {
            Vector2 vel = new Vector2(direction * speed, 0f);
            transform.Translate(vel * Time.deltaTime);
        }
    }

    private string RandomFromList(List<string> list)
    {
        if (list == null || list.Count == 0) return null;
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    private void UpdateFlip()
    {
        if (target == null) return;
        var sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            sr.flipX = (target.position.x < transform.position.x);
    }

    public void SwitchMove(float time)
    {
        allowmoving = false; StartCoroutine(WaitForSeconds(time, () =>  allowmoving = true));

    }

    
    public static IEnumerator WaitForSeconds(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField][Range(1, 10)] int damage = 1;
    
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable traget = collision.gameObject.GetComponent<IDamageable>();

        if (traget != null)
        {
            if (animator != null) { animator.Play("§ðÀ»"); }
            traget.TakeDamage(damage);
        }

    }
}

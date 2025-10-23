using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum AttackType { player, enemy };
public class AttackCheck : MonoBehaviour
{
  [SerializeField] AttackType attackType;
    AudioSource m_AudioSource;
   [SerializeField] AudioClip hitSFX;
   [SerializeField] AudioClip unhitSFX;
    private void OnEnable()
    {
        if (m_AudioSource == null)m_AudioSource = GetComponentInParent<AudioSource>();

        CheckOverlap();
    }


    void CheckOverlap()
    {
        Collider2D selfCollider;
         selfCollider = GetComponent<Collider2D>();

            ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true; 
        filter.SetLayerMask(Physics2D.DefaultRaycastLayers);
        filter.useLayerMask = true;

        List<Collider2D> results = new List<Collider2D>();
        int count = selfCollider.OverlapCollider(filter, results);

        if (attackType == AttackType.player)
        {

            foreach (var col in results)
            {
                if (col.gameObject == gameObject)
                    continue;

                IDamageable target = col.GetComponent<IDamageable>();

                int damageAmount = GetComponentInParent<PlayerBattle>().currentDamageRate;

                if (target != null)
                {
                    target.TakeDamage(damageAmount, transform.parent);
                    Debug.Log($"對 方塊 傷害 {damageAmount} 點！");
                }
                else count--;
            }
        }
        else {
            foreach (var col in results)
            {
                if (col.gameObject == gameObject)
                    continue;

                IDamageable target = col.GetComponent<IDamageable>();

                int damageAmount = 1;

                if (target != null)
                {
                    if (col.CompareTag("Player"))
                    {
                        target.TakeDamage(damageAmount, transform.parent);
                        Debug.Log($"對 玩家 傷害 {damageAmount} 點！");
                    }
                }
                
                else count--;
            }
        }

        if (count != 0) { m_AudioSource.PlayOneShot(hitSFX); }
        else { m_AudioSource.PlayOneShot(unhitSFX); }
    }
}

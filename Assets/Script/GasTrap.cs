using System.Collections;
using UnityEngine;

public class GasTrap : MonoBehaviour
{
    [Header("毒氣設定")]
    [SerializeField][Range(1, 10)] int damage = 1;      
    [SerializeField][Range(0.1f, 10f)] float delay = 2; // 進入後等待幾秒開始
    [SerializeField][Range(0.1f, 5f)] float interval = 1; // 每幾秒扣一次血

    Coroutine damageCoroutine;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.gameObject.GetComponent<IDamageable>();

        if (target != null)
        {
            damageCoroutine = StartCoroutine(DamageOverTime(target));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageOverTime(IDamageable target)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            target.TakeDamage(damage);

            yield return new WaitForSeconds(interval);
        }
    }
}
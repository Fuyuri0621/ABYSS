using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public int damage=10;
    public float speed=2;

    float timer;
    void Start()
    {
        timer = Time.time+5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= Time.time) { Destroy(gameObject); }

        Vector3 dir = transform.localScale.x < 0 ? Vector3.left : Vector3.right;

        transform.Translate(dir*Time.deltaTime*speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        if(collision.GetComponent<IDamageable>() == null) return;

        IDamageable d = collision.GetComponent<IDamageable>();

        d.TakeDamage(damage,transform);

        Destroy(gameObject);
    }


}

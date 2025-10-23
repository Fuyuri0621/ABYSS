using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockobject : MonoBehaviour
{
    [SerializeField] float destoryAfterTime;
    float timer;
    void Start()
    {
        timer = Time.time + destoryAfterTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
}

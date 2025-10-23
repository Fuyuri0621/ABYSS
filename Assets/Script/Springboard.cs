using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{

    [SerializeField][Range(1, 10)] int height = 5;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        PlayerMovent traget = collision.gameObject.GetComponent<PlayerMovent>();

        if (traget != null)
        {
        //    traget.bounceUp(height);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovent traget = collision.gameObject.GetComponent<PlayerMovent>();

        if (traget != null)
        {
            traget.bounceUp(height);
        }
    }
}

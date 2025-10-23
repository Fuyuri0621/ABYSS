using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropspikeDestroyr : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}

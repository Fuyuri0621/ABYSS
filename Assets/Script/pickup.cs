using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    float pickuptime;
    private void Awake()
    {
        pickuptime = Time.time+0.6f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        BackPackManager c=BackPackManager.Instance;
        if (c != null&&Time.time>pickuptime)
        {
            GetComponent<IPickUpObject>().OnPickUP(BackPackManager.Instance);
            Destroy(gameObject);
        }
    }
}

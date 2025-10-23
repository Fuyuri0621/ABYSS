using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControl : MonoBehaviour
{
    [SerializeField]GameObject mask;
    private void Awake()
    {
        mask = GetComponentInChildren<SpriteMask>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (mask != null&& collision.tag== "Foreground") {mask.SetActive(true);}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mask != null && collision.tag == "Foreground") { mask.SetActive(false); }
    }
}

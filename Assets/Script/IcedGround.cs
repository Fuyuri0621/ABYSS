using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedGround : MonoBehaviour
{
    [SerializeField] MovementSetting movementdata;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovent playerMovent;
        if (collision.GetComponent<PlayerMovent>() != null)
        {
            playerMovent = collision.GetComponent<PlayerMovent>();
            playerMovent.SetMoveMentSetting(movementdata);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovent playerMovent;
        if (collision.GetComponent<PlayerMovent>() != null)
        {
            playerMovent = collision.GetComponent<PlayerMovent>();
            playerMovent.ResetMovementSetting();
        }
    }
}

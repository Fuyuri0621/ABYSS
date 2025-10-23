using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] private bool onGround;

    [SerializeField] private float groundLength = 0.95f;
    [SerializeField] private Vector3 colliderOffset;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
    }

    private void OnDrawGizmos()
    {

        if (onGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    public bool GetOnGround() { return onGround; }
}

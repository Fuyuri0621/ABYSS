using UnityEngine;

public class VineSwing : MonoBehaviour
{
    public float swingForce = 10f; // 擺盪力度
    public float swingDistance = 5f;
    float cd=0f;
    private Rigidbody2D playerrb;
    DistanceJoint2D distanceJoint;


    private bool isSwinging = false;


    Transform pos;
    Transform player;
    Animator animator;
    private void Awake()
    {
        distanceJoint=GetComponent<DistanceJoint2D>();

        animator = GetComponentInChildren<Animator>();
        pos = transform.Find("pos").transform;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.Space) && !isSwinging&&cd<Time.time)
        {
            if (other.CompareTag("Player"))
            {
                player = other.transform;
                playerrb = player.GetComponent<Rigidbody2D>();
                distanceJoint.connectedBody = playerrb;
                distanceJoint.distance = swingDistance;
                isSwinging = true;
                animator.SetBool("isSwim",isSwinging);

            }
        }
    }

    

    void Update()
    {
        if (isSwinging&&player!=null)
        {

            pos.position = player.position;
            Vector3 targetPos = new Vector3(pos.position.x, pos.position.y+1, transform.position.z);
            Vector3 direction = targetPos - transform.position;
            transform.up = -direction; // 讓物件的「上方向」對準目標


            if (Input.GetKeyDown(KeyCode.Space) && isSwinging)
            {
              //  player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                distanceJoint.connectedBody = null;
                player =null;

                isSwinging = false;
                animator.SetBool("isSwim", isSwinging);
                cd =Time.time+1;

                transform.rotation = Quaternion.identity;
               
            }

          
        }
    }
}
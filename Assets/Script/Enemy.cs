using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField][Range(1, 10)] int damage = 1;
    [SerializeField][Range(1, 10)] float playerBounceamount = 1;
    // Start is called before the first frame update
    [Header("移動設定")]
    [Tooltip("水平移動速度")]
    public float speed = 2f;
    [Tooltip("最小隨機切換方向時間 (秒)")]
    public float minChangeInterval = 1f;
    [Tooltip("最大隨機切換方向時間 (秒)")]
    public float maxChangeInterval = 3f;

    [Header("邊緣檢測")]
    [Tooltip("從腳底向前檢測地面的長度")]
    public float edgeCheckDistance = 0.5f;
    [Tooltip("判定為地面的 Layer")]
    public LayerMask groundLayer;
    [Tooltip("腳底檢測點相對於物件中心的偏移")]
    public Vector2 footOffset = new Vector2(0.5f, -0.5f);

    private int direction = 1;   
    private float nextChangeTime;
    bool onGround;

    void Start()
    {
        direction = -1;
        ScheduleNextDirectionChange();
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);


        // 檢測腳底前方是否還有地面
        Vector2 origin = (Vector2)transform.position + new Vector2(footOffset.x * direction, footOffset.y);
        Vector2 leftorigin = (Vector2)transform.position + new Vector2(-footOffset.x * direction, footOffset.y);
        onGround = Physics2D.Raycast(origin, Vector2.down, edgeCheckDistance, groundLayer)&& Physics2D.Raycast(leftorigin, Vector2.down, edgeCheckDistance, groundLayer);
        Debug.DrawRay(origin, Vector2.down * edgeCheckDistance, Color.red);


        if (!onGround)
        {
            direction *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            ScheduleNextDirectionChange();
        }

        if (Time.time >= nextChangeTime)
        {
            direction *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            ScheduleNextDirectionChange();
        }
    }


    private void ScheduleNextDirectionChange()
    {
        float interval = Random.Range(minChangeInterval, maxChangeInterval);
        nextChangeTime = Time.time + interval;
    }

    void OnDrawGizmosSelected()
    {
        if (onGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }

        Vector2 origin = (Vector2)transform.position + new Vector2(footOffset.x * direction, footOffset.y);
        Vector2 leftorigin = (Vector2)transform.position + new Vector2(-footOffset.x * direction, footOffset.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * edgeCheckDistance);
        Gizmos.DrawLine(leftorigin, leftorigin + Vector2.down * edgeCheckDistance);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovent>()!=null)
        {
            PlayerMovent player = collision.GetComponent<PlayerMovent>();
            if (collision.transform.position.y > transform.position.y + 0.2f) // 如果玩家從上方踩到
            {
                player.bounceUp(playerBounceamount);
                Destroy(gameObject);
            }
            else
            {
                IDamageable traget = collision.gameObject.GetComponent<IDamageable>();
                if (traget != null)
                {
                    traget.TakeDamage(damage);
                }
            }
        }
    }

}

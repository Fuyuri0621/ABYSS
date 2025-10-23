using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovent : MonoBehaviour
{
    [SerializeField] bool flying=false;
    bool allowmoving=true;
    [SerializeField] MovementSetting GroundmovementSetting;
    public float speedLimit=100;
    [SerializeField, Range(0f, 20f)] public float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] public float maxAcceleration = 52f;
    [SerializeField, Range(0f, 100f)] public float maxDecceleration = 52f;
    [SerializeField, Range(0f, 100f)] public float maxTurnSpeed = 80f;
    [SerializeField, Range(0f, 100f)] public float maxAirAcceleration;
    [SerializeField, Range(0f, 100f)] public float maxAirDeceleration;
    [SerializeField, Range(0f, 100f)] public float maxAirTurnSpeed = 80f;
    float directionX;
    float directionY;
    bool isPressingKey;
    bool onGround;
    Vector2 currentVelocity;

    private float acceleration;
    private float deceleration;
    private float turnSpeed;
    private float maxSpeedChange;
    PlayerGroundDetector ground;
    [HideInInspector]
    public Vector2 MoveVector;

    Rigidbody2D rgbd2d;

    public float jumpHeight = 7.3f;
   [Range(0.4f, 1.25f)][Tooltip("到最高點的時間")] public float timeToJumpApex;

    [Range(0f, 5f)][Tooltip("上升重力")] public float upwardGravtity = 1f;
    [Range(0.1f, 10f)][Tooltip("下降重力")] public float downwardGravity = 1f;
    public float jumpCutOff=1;
    public float jumpSpeed;
    public float gravMultiplier;
    public int maxAirJumps = 0;
    public float jumpBuffer = 0.15f;
    public bool canJumpAgain = false;
    public bool variablejumpHeight;
    private bool desiredJump;
    private float jumpBufferCounter;
    private bool pressingJump;
    private bool currentlyJumping;
    private int jumpsRemaining;
    Animator animator;

    Character character;

    void Awake()
    {
        character = GetComponent<Character>();
        rgbd2d = GetComponent<Rigidbody2D>();
        ground = GetComponent<PlayerGroundDetector>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
            onGround = ground.GetOnGround();

            if (jumpBuffer > 0)
            {
                if (desiredJump)
                {
                    jumpBufferCounter += Time.deltaTime;

                    if (jumpBufferCounter > jumpBuffer)
                    {
                        desiredJump = false;
                        jumpBufferCounter = 0;
                    }
                }
            }

        if (onGround)
        {

            jumpsRemaining = maxAirJumps;
            currentlyJumping = false;
        }



        if (directionX != 0)
            {
                transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
                isPressingKey = true;
                animator.SetBool("isMove", true);
            }
            else
            {
                isPressingKey = false;
                animator.SetBool("isMove", false);
            }
            if (transform.position.y < -20) { transform.position = transform.position + Vector3.up * 60f; }



            MoveVector = flying ? new Vector2(directionX, directionY) * maxSpeed: new Vector2(directionX, 0f) * maxSpeed;

        
    }

    private void FixedUpdate()
    {
        setPhysics();

        currentVelocity = rgbd2d.velocity;

        if (desiredJump)
        {
            DoAJump();
        }

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDecceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed;

        if (isPressingKey)
        {
            
            if (Mathf.Sign(directionX) != Mathf.Sign(currentVelocity.x))
            {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else
            {
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            maxSpeedChange = deceleration * Time.deltaTime;
        }

        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, MoveVector.x, maxSpeedChange);

        if (MoveVector.y > 0f) currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, MoveVector.y, maxSpeedChange);

        if (character.currentHp > 0&&allowmoving)
        {
            rgbd2d.velocity = currentVelocity;
            calculateGravity();
        }
        else { rgbd2d.velocity = Vector2.zero; }
    }
    

    public void OnMove(InputValue value)
    {
            directionX = value.Get<Vector2>().x;

        directionY = value.Get<Vector2>().y;


    }

    public void OnJump(InputValue value)
    {


            if (value.isPressed)
            {
                desiredJump = true;
                pressingJump = true;
            }
            else
                pressingJump = false;

    }

    private void DoAJump()
    {

        if (onGround || jumpsRemaining > 0)
        {
            desiredJump = false;
            jumpBufferCounter = 0;

            if (!onGround)
                jumpsRemaining--;


            //canJumpAgain = (maxAirJumps == 1 && canJumpAgain == false);

            jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * rgbd2d.gravityScale * jumpHeight);

            if (currentVelocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - currentVelocity.y, 0f);
            }
            else if (currentVelocity.y < 0f)
            {
                jumpSpeed += Mathf.Abs(rgbd2d.velocity.y);
            }

            currentVelocity.y += jumpSpeed;
            currentlyJumping = true;
        }

        if (jumpBuffer == 0)
        {
            desiredJump = false;
        }
    }

    public void bounceUp(float bounceAmount)
    {
        rgbd2d.velocity=new Vector2(rgbd2d.velocity.x,0);
        rgbd2d.AddForce(Vector2.up * bounceAmount*10, ForceMode2D.Impulse);
    }
    private void setPhysics()
    {
        Vector2 newGravity = new Vector2(0, (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex));
        rgbd2d.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravMultiplier;
    }

    private void calculateGravity()
    {
        if (rgbd2d.velocity.y > 0.01f)
        {
            if (onGround)
            {
                gravMultiplier = 1;
            }
            else
            {
                if (variablejumpHeight)
                {
                    if (pressingJump && currentlyJumping)
                    {
                        gravMultiplier = upwardGravtity;
                    }
                    else
                    {
                        gravMultiplier = jumpCutOff;
                    }
                }
                else
                {
                    gravMultiplier = upwardGravtity;
                }
            }
        }

        else if (rgbd2d.velocity.y < -0.01f)
        {
            if (onGround)
            {
                gravMultiplier = 1;
            }
            else if(pressingJump)
            {
                gravMultiplier = downwardGravity;
            }
            else
            {
                gravMultiplier = jumpCutOff;
            }
        }
        else
        {
            if (onGround)
            {
                currentlyJumping = false;
            }
            gravMultiplier = 1;
        }

        rgbd2d.velocity = new Vector2(currentVelocity.x, Mathf.Clamp(currentVelocity.y, -speedLimit, 100));
    }

   public void SetMoveMentSetting(MovementSetting movementSetting)
    {
        maxSpeed = movementSetting.maxSpeed;
        maxAcceleration = movementSetting.maxAcceleration;
        maxDecceleration = movementSetting.maxDecceleration;
        maxTurnSpeed = movementSetting.maxTurnSpeed;
        jumpHeight = movementSetting.jumpHeight;
        maxAirAcceleration = movementSetting.maxAirAcceleration;
        maxAirDeceleration = movementSetting.maxAirDeceleration;
        maxAirTurnSpeed = movementSetting.maxAirTurnSpeed;
        maxAirJumps = movementSetting.maxAirJumps;
    }
    public void ResetMovementSetting()
    {
        maxSpeed = GroundmovementSetting.maxSpeed;
        maxAcceleration = GroundmovementSetting.maxAcceleration;
        maxDecceleration = GroundmovementSetting.maxDecceleration;
        maxTurnSpeed = GroundmovementSetting.maxTurnSpeed;
        jumpHeight = GroundmovementSetting.jumpHeight;
        maxAirAcceleration = GroundmovementSetting.maxAirAcceleration;
        maxAirDeceleration = GroundmovementSetting.maxAirDeceleration;
        maxAirTurnSpeed = GroundmovementSetting.maxAirTurnSpeed;
        maxAirJumps = GroundmovementSetting.maxAirJumps;
    }

    public void AllowMoving(bool set)
    {
        allowmoving = set;
    }

    public void OnPauseGame()
    {
        GameManager.Instance.PauseGame();
    }
}

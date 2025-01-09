using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Specs")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping Specs")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float fallSpeed;
    private bool readyToJump;
    bool sprinting;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;


    [Header("Keybinds")]
    public KeyCode jumpKey;
    public KeyCode  sprintKey;
    public KeyCode crouchKey;

    [Header("Ground Check Specs")]
    public float playerHeight; //might hardcode this
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Animation")]
    public Animator animator;

    [Header("References")]
    public Transform orientation; //might change this, idk if I like it
    public Transform camera;
    public TMP_Text spedometer;
    public TMP_Text state;
    public TMP_Text groundedText;

    //private variables
    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;
    private Rigidbody rb;
    private MovementState movementState;

    private enum MovementState 
    {
        Walking,
        Sprinting,
        Air,
        Crouching,
        Idle
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .05f, whatIsGround);
        if(groundedText != null)
        {
            groundedText.text = "Grounded: " + grounded.ToString();
        }
        
        InputCheck();
        StateHandler();
        
        //drag
        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        if(spedometer != null)
        {
            spedometer.text = rb.velocity.magnitude.ToString();
        }
        

    }

    private void FixedUpdate()
    {
        SpeedControl();
        MovePlayer();
    }


    private void StateHandler()
    {
        //crouching
        if(Input.GetKey(crouchKey))
        {
            moveSpeed = crouchSpeed;
            movementState = MovementState.Crouching;
        } //sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            moveSpeed = sprintSpeed;
            movementState = MovementState.Sprinting;
            animator.SetInteger("State", 2);
        }//walking

        else if (grounded && rb.velocity.magnitude == 0)
        {
            movementState = MovementState.Idle;
            animator.SetInteger("State", 0);
        }
        else if (grounded)
        {
            moveSpeed = walkSpeed;
            movementState = MovementState.Walking;
            animator.SetInteger("State", 1);
        } //in air/falling
        else{
            movementState = MovementState.Air;
            animator.SetInteger("State", 2);
        }
        if(state != null)
        {
            state.text = movementState.ToString();
        }
        
    }

    void InputCheck()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //jump
        if(readyToJump && grounded && Input.GetKey(jumpKey))
        {
            readyToJump = false;
            Jump();
            StartCoroutine(JumpCooldown());
        }

        //start crouching
        if(Input.GetKeyDown(crouchKey)) //CURRENTLY NO CHECK FOR GROUNDED 
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            //instead of scaling it down, lets just manually move the camera object down a little bit?
            //camera.
            //slight downward force to push them onto the ground
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        
        //stop crouching
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            //dont need to upward force, unity physics will figure it out :)
            
        }
    }

    private void MovePlayer()
    {
        //this is a tricky one
        // = the forward direction we are looking (will always be flat) * the up and down. this controls the movement direction on a vertical scale
        // + the direction to our right (will always be 90 degrees rotated on y axis) * the left and right. This controls movement direction on horizontal scale.
        movementDirection = orientation.forward * verticalInput +  orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            //we need to change the direction of our movement force to match the slope we are on, so we arent moving into the slope -> /
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            if(rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        //this is if we're on flat ground
        if(grounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed, ForceMode.Force);
        }
        else if(!grounded)
        {
            //add movement in a horizontal direction, slows down over time
            rb.AddForce(movementDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
            //add movement downward, this accelerates the fall a little bit because unity gravity is weird
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
        }

        rb.useGravity = !OnSlope();
    }



    void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        readyToJump = true;
        exitingSlope = false;
    }

    private void SpeedControl()
    {
        //limit movement speed on sloped ground.
        if(OnSlope() && !exitingSlope)
        {
            //if we are moving faster than our movement speed, thats illegal. set it to max movement speed instead.
            if(rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        } 
        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            //if we are moving faster than our movement speed, thats illegal.
            if(flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * .5f + .2f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    }

    private void CameraAnimation()
    {

    }

}

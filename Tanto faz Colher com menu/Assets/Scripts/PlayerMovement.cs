using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    
    public BoxCollider2D triggerDist;
    //variables
    public Rigidbody2D rb;
    //basic movement vars

    public float speed = 10;
    private float moveInput;
    public float jumpForce;
   
    //crouch
   
    [Range(0, 1)] public float crouchSpeed = .36f;
    [SerializeField] private Collider2D crouchDisableCollider;


    //plataform check vars

    private bool isGrounded = true;
    [SerializeField] private Transform feetPos;
    [SerializeField] private Transform ceilingCheck;
    const float groundCheckRadius = .5f;    //radius of the overlap circle that checks if the player is on ground
    const float ceilingCheckRadius = .5f;   //radius of the overlap circle that checks if the player can stand up
    [SerializeField] private LayerMask whatIsGround;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool isCrouching = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  //makes reference to the rb component in the editor
        triggerDist = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");   //gets and store the input value
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundCheckRadius, whatIsGround); //checks if the player is on ground

        if (isGrounded == true && Input.GetButtonDown("Jump") && isCrouching == false && !Input.GetButton("Telecinesia"))
        {

            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }
        if(isGrounded == false)
        {
            animator.SetBool("IsJumping", true);
        }

    }

    private void FixedUpdate()
    {
        
        if (!Input.GetButton("Telecinesia"))
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            
            if (isGrounded) //&& rb.velocity.y == 0)
            {
                OnLandEvent.Invoke();
                animator.SetBool("IsJumping", false);
            }
            if (Input.GetButton("Crouch") && isGrounded)
            {

                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }
            if (isCrouching)
            {

                rb.velocity *= crouchSpeed;
                if (crouchDisableCollider != null)
                {
                    crouchDisableCollider.enabled = false;
                }
            }
            else if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsGround))
            {
                Debug.Log("BATATA");
                isCrouching = true;
            }
            else
            {
                if (crouchDisableCollider != null)
                {
                    crouchDisableCollider.enabled = true;
                }
            }
            Flip(); //just flips the player depending on his direction
        }
        else if (Input.GetButton("Telecinesia") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }




        

        




    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(triggerDist.bounds.center, triggerDist.bounds.size);
    }
    void Flip()
    {
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
  
}

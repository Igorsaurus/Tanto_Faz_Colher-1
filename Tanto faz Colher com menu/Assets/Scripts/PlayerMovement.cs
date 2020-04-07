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

    //public BoolEvent OnCrouchEvent;
    private bool isCrouching;
    bool jump;
    bool usandoTelecinesia;
    bool crouch;
    public GerenciadorDeFase GerenciadorDeFase;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //makes reference to the rb component in the editor
        triggerDist = GetComponent<BoxCollider2D>();
        isCrouching = false;
        jump = false;
        usandoTelecinesia = false;
        crouch = false;
        GerenciadorDeFase = FindObjectOfType<GerenciadorDeFase>();
    }
    private void FixedUpdate()
    {
        if(transform.position.y < -15)
        {
            GerenciadorDeFase.RespawnPlayer();
        }
        
        print(jump);

        if (usandoTelecinesia)
        {
          usandoTelecinesia = false;
          if (isGrounded)
          {
                isGrounded = false;
             rb.velocity = Vector2.zero;
             animator.SetFloat("Speed", 0);
          }
        }
        else
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            Flip();
            if (isGrounded)
            {
                //print("CACHORRO");
                isGrounded = false;
                OnLandEvent.Invoke();
                animator.SetBool("IsJumping", false);


                if (jump)
                {
                    jump = false;
                    rb.velocity = Vector2.up * jumpForce;
                    isGrounded = false;

                }
                if (crouch)
                {
                    crouch = false;
                    isCrouching = true;
                }
                else
                {
                    isCrouching = false;
                }
                if (isCrouching)
                {
                    animator.SetBool("IsCrouching", true);
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
                    animator.SetBool("IsCrouching", false);
                    if (crouchDisableCollider != null)
                    {
                        crouchDisableCollider.enabled = true;
                    }
                }
            }
            else
            {
                animator.SetBool("IsJumping", true);
            }
        }
        
    }
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");   //gets and store the input value
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundCheckRadius, whatIsGround); //checks if the player is on ground
        if(Input.GetButtonDown("Jump")) jump = true;
        if(Input.GetButton("Telecinesia")) usandoTelecinesia = true;
       if(Input.GetButton("Crouch"))
            crouch = true;
        isCrouching = true;

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
    //public void OnCrouching()
    //{
       // animator.SetBool("IsCrouching", true);
    //}
  
}

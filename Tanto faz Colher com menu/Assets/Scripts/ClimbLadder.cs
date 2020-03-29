using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClimbLadder : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    //ladder
    public float distance;
    [SerializeField] private LayerMask whatIsLadder;
    private bool isClimbing;
    private float inputVertical;
    public float climbSpeed;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool isCrouching = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if(hitInfo.collider != null)
        {
            //Debug.DrawRay(hitInfo.centroid, hitInfo.direction, Color blue);
            if (Input.GetButtonDown("Vertical"))
            {
                isClimbing = true;
            }else if(Input.GetButton("Horizontal"))
            {
                isClimbing = false;
            }
        }
        else
        {
            isClimbing = false;
        }
        if (isClimbing == true)
        {
           
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.position.x, inputVertical * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 4;
        }
    }
}

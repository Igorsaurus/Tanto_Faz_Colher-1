using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuloMelhorado : MonoBehaviour
{
    public float aceleraçãoQueda = 4.5f;
    public float forçaPuloPequeno = 4f;
    public float gravidadePlayer = 3f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = aceleraçãoQueda;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = forçaPuloPequeno;
        }
        else
        {
            rb.gravityScale = gravidadePlayer;
        }
    }
}

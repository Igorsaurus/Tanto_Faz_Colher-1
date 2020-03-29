using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    public Rigidbody2D rbCaixa;
    public SpriteRenderer srCaixa;
    private PlayerMovement playerMovementScript;
    float playerSpeed = 0;
    public float telecinese = 500;
    PlayerMovement player;
    public float horizontalInput;
    private Vector2 horizontalMove;
    private bool range;

    // Start is called before the first frame update
    void Start()
    {
        //playerSpeed = playerMovementScript.rb.velocity.x;
        range = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("HorizontalArrow");
        horizontalMove = new Vector2(horizontalInput, 0);
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Telecinesia"))
        {
            if (range)
            { 
                srCaixa.color = new Color(255, 120, 64);
                rbCaixa.AddForce(horizontalMove * telecinese, ForceMode2D.Force);
                if (gameObject.CompareTag("caixaLeve"))
                {
                    rbCaixa.AddForce(Vector2.up* 2, ForceMode2D.Impulse);
                    if (Input.GetKey(KeyCode.UpArrow))
                        rbCaixa.AddForce(Vector3.up * telecinese, ForceMode2D.Force);
                }
                Debug.Log("PIPOCA");
                if (Input.GetButtonDown("interage"))
                {
                    rbCaixa.isKinematic = true;
                    rbCaixa.velocity *= 0;
                }
               
            }
        }
        if (Input.GetButtonUp("interage"))
        {
            rbCaixa.isKinematic = false;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //usar um for pra ficar checando se ocorre uma colisão com o circlecast(?), dai se colidir, e pressionar a tecla, adicionar uma força na caixa, como as massas são diferentes, a aceleração será diferente;
        if (collision.gameObject.CompareTag("Player"))
        {
            telecinese = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        telecinese = 370;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            range = true;
            //for na posição i é igual ao objeto que colidiu

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        range = false;
        rbCaixa.isKinematic = false;
        // srCaixa.color = new Color(255, 255, 255);for na posição i é igual a null
    }
}

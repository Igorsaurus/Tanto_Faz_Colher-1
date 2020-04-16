using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    public Rigidbody2D rbCaixa;
    public SpriteRenderer srCaixa;
    //private PlayerMovement playerMovementScript;
    //float playerSpeed = 0;

    public float telecinese = 500;

    //PlayerMovement player;

    public float horizontalInput;
    private Vector2 horizontalMove;
    private bool range;
    bool telecinesia;
    bool interage;
    public bool flutuar;
    private Vector2 posInicial;
    public GerenciadorDeFase GerenciadorDeFase;

    // Start is called before the first frame update
    void Start()
    {
        //playerSpeed = playerMovementScript.rb.velocity.x;
        range = false;
        telecinesia = false;
        interage = false;
        flutuar = false;
        posInicial = transform.position;
        GerenciadorDeFase = FindObjectOfType<GerenciadorDeFase>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -15 || GerenciadorDeFase.respawn == true)
        {
            flutuar = false;
            transform.position = posInicial;                                    //reseta a caixa no mapa
        }
        
        horizontalInput = Input.GetAxisRaw("HorizontalArrow");
        horizontalMove = new Vector2(horizontalInput, 0);
        if(Input.GetButton("Telecinesia")) telecinesia = true;
        if (Input.GetButtonDown("interage")) interage = true;
        SwitchFlutuar();
    }
    private void FixedUpdate()
    {
        
        //print("flutuar= " + flutuar);
        if (telecinesia)
        {
            telecinesia = false;
            if (range)
            {
                print("ALELUIA");
                range = false;
                srCaixa.color = new Color(255, 120, 64);
                rbCaixa.AddForce(horizontalMove * telecinese, ForceMode2D.Force);
                if (gameObject.CompareTag("caixaLeve"))
                {
                    rbCaixa.AddForce(Vector2.up* 2, ForceMode2D.Impulse);
                    if (Input.GetKey(KeyCode.UpArrow))
                        rbCaixa.AddForce(Vector3.up * telecinese, ForceMode2D.Force);
                }
                Debug.Log("PIPOCA");
                if (flutuar == true)
                {
                    rbCaixa.isKinematic = true;
                    rbCaixa.velocity *= 0;
                }else if(flutuar == false)
                {
                    rbCaixa.isKinematic = false;
                }
            }
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
        if (collision.name == "AreaTelecinese")
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
    void SwitchFlutuar()
    {
        if (interage)
        {
            interage = false;
            if (range && telecinesia)
            {
                flutuar = !flutuar;
            }
        }
    }
}

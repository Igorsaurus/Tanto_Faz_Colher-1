using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSentinela : MonoBehaviour
{
    public Rigidbody2D rbRobo;                   //robo fica flutuando, se movimenta num movimento de patrol e se o player estiver na sua range, ataca.
    public Animator animator;
    private GameObject player;
    public float followDist = 5;
 
    [SerializeField] private LayerMask whatIsGround;

    public float comprimentoRay;
    [SerializeField] private float distPlataforma;    //comprimento da distancia entre o raycast e a plataforma
    public Transform groundDetection;     //empty que serve de sensor de plataforma

    private Vector2 horizontalMove;    //pra fazer a velocidade dele
    private float speed = 2;
    
    private bool isFacingRight;
    
    private bool playerInRange;   //se colidir com o trigger do player, fica true
    RaycastHit2D hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        rbRobo = GetComponent<Rigidbody2D>();
        playerInRange = false;  // vai ficar true se ele entrar na range do collider trigger do player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        //vai checar se ele não vai cair da borda da plataforma e virar ele quando ele chegar nela
        hitInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, comprimentoRay);
        print(hitInfo.distance);
        distPlataforma = hitInfo.distance;
        if(distPlataforma < 2)
        {
            rbRobo.AddForce(Vector2.up * 5, ForceMode2D.Force);
        } 
        if (distPlataforma > 2)
        {
            rbRobo.AddForce(Vector2.down * 5, ForceMode2D.Force);
        }
        if(distPlataforma == 2)
        {
            rbRobo.isKinematic = true;
        }else
        {
            rbRobo.isKinematic = false;
        }
       

        if(hitInfo.collider == null)
        {
            if(isFacingRight == true)
            {
                Flip();
            }
            else
            {
                Flip();
                
            }
             
        }
    }
    private void FixedUpdate()
    {
        if (!playerInRange)
        {
            //faz um patrol pelas plataformas de boas
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
   
         //ataca o player
         print("ALA ELE!");
         transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followDist * Time.deltaTime);
           
             
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flip();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;

       // Vector3 rbScale = transform.localScale;
       // rbScale.x *= -1;
       // transform.localScale = rbScale;
        if (isFacingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }
}

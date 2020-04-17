using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoLaser : MonoBehaviour
{
    public float distance;
    public GerenciadorDeFase GerenciadorDeFase;
    public LayerMask Colidivel;
    public LineRenderer lineOfSight;
    public Gradient corEscura;
    public Gradient corClara;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitRaycast = Physics2D.Raycast(transform.position, transform.right, distance,Colidivel);
        if(hitRaycast.collider != null){
            Debug.DrawLine(transform.position, hitRaycast.point, Color.red);
            lineOfSight.SetPosition(1, hitRaycast.point);
            lineOfSight.colorGradient = corEscura;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.yellow);
            lineOfSight.SetPosition(1, transform.position + transform.right * distance);
            lineOfSight.colorGradient = corClara;
        }
        if (hitRaycast.collider.CompareTag("Player"))
        {
            GerenciadorDeFase.RespawnPlayer();
        }
        lineOfSight.SetPosition(0, transform.position);
    }
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        GerenciadorDeFase = FindObjectOfType<GerenciadorDeFase>();
    }
}

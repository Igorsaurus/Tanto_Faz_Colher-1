using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleLaser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform hitLaser;
    public GerenciadorDeFase GerenciadorDeFase;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        GerenciadorDeFase = FindObjectOfType<GerenciadorDeFase>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance);
        Debug.DrawLine(transform.position, hit.point);
        hitLaser.position = hit.point;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hitLaser.position);
       
        if (hit.collider.name == "colher")
        {
            GerenciadorDeFase.RespawnPlayer();
        }

    }
}

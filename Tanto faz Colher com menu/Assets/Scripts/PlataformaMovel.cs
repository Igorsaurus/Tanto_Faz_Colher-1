using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    public float speed;
    public Transform[] moveSpots;
    private int atualSpot;
    
    // Start is called before the first frame update
    void Start()
    {
        atualSpot = 0;//Random.Range(0, moveSpots.Length);
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[atualSpot].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpots[atualSpot].position) < 0.2f){
            if(waitTime <= 0)
            {
                MudaSpot();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    void MudaSpot()
    {
        if(atualSpot == 1)
        {
            atualSpot = 0;
        }else if(atualSpot == 0){
            atualSpot = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEmFrente : MonoBehaviour
{
    public float speed = 40;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
       // Invoke("Destroy(gameObject)", 0.1f);
    }
   private void OnCollisionEnter2D(Collision2D other)
    {
        
       // if(other.collider.gameObject.layer == (double)Globais.LayerMasks.MovingBlock)
       // {
            Destroy(gameObject);
            print("CUUUUUUU");
      //  }
        

    }
}

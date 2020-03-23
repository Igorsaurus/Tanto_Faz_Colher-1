using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorDialoga : MonoBehaviour
{
    public bool EstaInteragindo { get; set; }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interage"))
        {
            EstaInteragindo = true;
        }
        else
        {
            EstaInteragindo = false;
        }
        
    }
}

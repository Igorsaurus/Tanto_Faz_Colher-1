using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleLaser : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LançaLaser", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LançaLaser()
    {
        Instantiate(laser, transform.position, laser.transform.rotation);
    }
}

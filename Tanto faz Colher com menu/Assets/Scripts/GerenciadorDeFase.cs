using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeFase : MonoBehaviour
{
    public GameObject checkpointAtual;
    private PlayerMovement player;
    public bool respawn;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        respawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnPlayer()
    {
        respawn = true;
        Debug.Log("Player Respawn");
        player.transform.position = checkpointAtual.transform.position;
        Invoke("RespawnFalso", 1);
    }
    void RespawnFalso()
    {
        respawn = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteragirComBotão : MonoBehaviour
{
    [SerializeField]
    private JogadorDialoga _jogadorDialoga;

    [SerializeField]
    private UnityEvent _botaoApertado;

    private bool _podeExecutar;

    void Update()
    {
      if (_podeExecutar)
        {
            if (_jogadorDialoga.EstaInteragindo == true)
            {
                _botaoApertado.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _podeExecutar = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _podeExecutar = false;
    }

}

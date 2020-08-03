using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 30;
    private Rigidbody _rigidbodyBala;
    public AudioClip SomDeMorteZumbi;


    void Start()
    {
        _rigidbodyBala = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidbodyBala.MovePosition(_rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        int dano = 1;
        switch (objetoDeColisao.tag)
        {
            case Tags.Inimigo:
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(dano);
                break;
            case Tags.ChefeZumbi:
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(dano);
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}

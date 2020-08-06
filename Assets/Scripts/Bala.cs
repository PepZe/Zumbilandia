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
        if (objetoDeColisao.tag == Tags.Inimigo || objetoDeColisao.tag == Tags.ChefeZumbi)
        {
            var inversaoDoSentidoDeRotacao = Quaternion.LookRotation(-transform.forward);
            objetoDeColisao.GetComponent<IMatavel>().TomarDano(dano);
            objetoDeColisao.GetComponent<IMatavel>().Sangrar(inversaoDoSentidoDeRotacao);

        }

        Destroy(gameObject);
    }
}

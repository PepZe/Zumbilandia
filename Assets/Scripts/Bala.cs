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
        if (objetoDeColisao.tag == Tags.Inimigo)
        {
            int dano = 1;
            objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(dano);
        }
        Destroy(gameObject);
    }
}

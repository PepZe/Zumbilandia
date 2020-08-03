using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaPersonagem : MonoBehaviour
{
    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        _rigidbody.MovePosition(_rigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
    }
    public void Rotacionar(Vector3 direcao)
    {
        var novaRotacao = Quaternion.LookRotation(direcao);
        _rigidbody.MoveRotation(novaRotacao);
    }
    public void Morrer()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
}

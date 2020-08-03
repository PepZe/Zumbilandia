using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAnimacao : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Atacar(bool estado)
    {
        _animator.SetBool("Atacando", estado);
    }
    public void AnimarMovimento(Vector3 direacao)
    {
        _animator.SetFloat("Movendo", direacao.magnitude);
    }
    public void Morrer()
    {
        _animator.SetTrigger("Morreu");
    }
}

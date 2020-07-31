using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int _tempoVida = 5;
    private int _quantidadeDeCura = 15;

    private void Start()
    {
        Destroy(gameObject, _tempoVida);
    }

    private void OnTriggerEnter(Collider colisor)
    {
        if (colisor.tag == Tags.Jogador)
        {
            colisor.GetComponent<ControlaJogador>().CurarVida(_quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}

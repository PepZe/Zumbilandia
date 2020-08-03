using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeZumbi : MonoBehaviour
{
    public GameObject ChefePrefab;
    private float _tempoMaximoGeracao = 20;
    private float _tempoParaProximaGeracao;

    void Start()
    {
        _tempoParaProximaGeracao = _tempoMaximoGeracao;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad >= _tempoParaProximaGeracao)
        {
            Instantiate(ChefePrefab, transform.position, Quaternion.identity);
            _tempoParaProximaGeracao = Time.timeSinceLevelLoad + _tempoMaximoGeracao;
        }
    }
}

using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeZumbi : MonoBehaviour
{
    private ControlaInterface _scriptControlaInterface;
    public Transform[] TransformPosicoesPossiveisDeCriacao;
    public GameObject ChefePrefab;
    private GameObject _jogador;
    public float _tempoMaximoGeracao = 20;
    private float _tempoParaProximaGeracao;

    void Start()
    {
        _tempoParaProximaGeracao = _tempoMaximoGeracao;
        _jogador = GameObject.FindWithTag(Tags.Jogador);
        _scriptControlaInterface = FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= _tempoParaProximaGeracao)
        {
            var posicaoGerarChefe = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoGerarChefe, Quaternion.identity);
            _scriptControlaInterface.AparecerTextoChefeCriado();
            _tempoParaProximaGeracao = Time.timeSinceLevelLoad + _tempoMaximoGeracao;
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        var posicaoDeMaiorDistancia = Vector3.zero;
        var maiorDistancia = 0f;
        foreach (var possivelPosicao in TransformPosicoesPossiveisDeCriacao)
        {
            var distanciaPosicaoJogador = Vector3.Distance(possivelPosicao.position, _jogador.transform.position);
            if(distanciaPosicaoJogador > maiorDistancia)
            {
                maiorDistancia = distanciaPosicaoJogador;
                posicaoDeMaiorDistancia = possivelPosicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }
}

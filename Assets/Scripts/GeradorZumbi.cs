using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GeradorZumbi : MonoBehaviour
{
    private GameObject _jogador;
    public GameObject Zumbi;
    public float TempoGerarZumbi = 1;
    private float _contadorTempo;
    public LayerMask LayerZumbi;
    private float _distanciaDeGeracao = 3;
    private float _distanciaDoJogadorParaGeracao = 20;

    private void Start()
    {
        _jogador = GameObject.FindWithTag(Tags.Jogador);
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,_jogador.transform.position) >= _distanciaDoJogadorParaGeracao)
        _contadorTempo += Time.deltaTime;

        if (_contadorTempo >= TempoGerarZumbi)
        {
            StartCoroutine(GerarNovoZumbi());
            _contadorTempo = 0;
        }
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        int raioDeVerificacao = 1;
        var posicaoCriacao = AleatorizarPosicao();
        var colisor = Physics.OverlapSphere(posicaoCriacao, raioDeVerificacao, LayerZumbi);

        while (colisor.Length > 0)
        {
            posicaoCriacao = AleatorizarPosicao();
            colisor = Physics.OverlapSphere(posicaoCriacao, raioDeVerificacao, LayerZumbi);
            yield return null;
        }

        if (colisor.Length <= 0)
        {
            Instantiate(Zumbi, posicaoCriacao, transform.rotation);
        }

    }

    private Vector3 AleatorizarPosicao()
    {
        var posicaoCriacao = Random.insideUnitSphere * _distanciaDeGeracao;
        posicaoCriacao += transform.position;
        posicaoCriacao.y = transform.position.y;
        return posicaoCriacao;
    }
}

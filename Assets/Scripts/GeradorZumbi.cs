using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GeradorZumbi : MonoBehaviour
{
    private GameObject _jogador;
    public GameObject PrefabZumbi;
    public float TempoGerarZumbi = 1;
    private float _contadorTempo;
    public LayerMask LayerZumbi;
    private float _distanciaDeGeracao = 3;
    private float _distanciaDoJogadorParaGeracao = 20;
    private int _qntMaximaZumbis = 2;
    private int _qntZumbis;
    private float _tempoMaxAumentarQntZumbis = 15;
    private float _tempoAumentarQntZumbi;

    private void Start()
    {
        _jogador = GameObject.FindWithTag(Tags.Jogador);

        for (int i = 0; i < _qntMaximaZumbis; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
        _tempoAumentarQntZumbi = _tempoMaxAumentarQntZumbis;
    }
    void Update()
    {
        var possoGerarZumbi = Vector3.Distance(transform.position, _jogador.transform.position) >= _distanciaDoJogadorParaGeracao;
        if (possoGerarZumbi &&
            _qntZumbis < _qntMaximaZumbis)

            _contadorTempo += Time.deltaTime;

        if (_contadorTempo >= TempoGerarZumbi)
        {
            StartCoroutine(GerarNovoZumbi());
            _contadorTempo = 0;
        }

        if(Time.timeSinceLevelLoad > _tempoAumentarQntZumbi)
        {
            _qntMaximaZumbis++;
            _tempoAumentarQntZumbi = Time.timeSinceLevelLoad + _tempoMaxAumentarQntZumbis;
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

        var zumbi = Instantiate(PrefabZumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.MeuGerador = this;

        _qntZumbis++;
    }

    private Vector3 AleatorizarPosicao()
    {
        var posicaoCriacao = Random.insideUnitSphere * _distanciaDeGeracao;
        posicaoCriacao += transform.position;
        posicaoCriacao.y = transform.position.y;
        return posicaoCriacao;
    }

    public void AtualizarQntZumbiVivos()
    {
        _qntZumbis--;
    }
}

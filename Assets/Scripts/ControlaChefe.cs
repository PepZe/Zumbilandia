using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : ControlaZumbi, IMatavel
{
    public Slider SliderBarraDeVida;
    public Image SliderImageVida;
    public Color ColorVidaMaxima, ColorVidaMinima;

    private GameObject _jogador;
    private NavMeshAgent _navChefe;
    private Rigidbody _rigidbody;

    private ControlaInterface _scriptControlaInterface;
    private ControlaAnimacao _scriptControlaAnimacao;
    private MovimentarChefe _scriptMovimentaChefe;
    private Status _scriptStatusChefe;

    private float _porcentagemGerarKitMedico = 0.8f;

    private void Start()
    {
        _scriptControlaInterface = FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        _scriptControlaAnimacao = GetComponent<ControlaAnimacao>();
        _scriptMovimentaChefe = GetComponent<MovimentarChefe>();
        _scriptStatusChefe = GetComponent<Status>();

        _rigidbody = GetComponent<Rigidbody>();
        _jogador = GameObject.FindWithTag(Tags.Jogador);
        _navChefe = GetComponent<NavMeshAgent>();

        _navChefe.speed = _scriptStatusChefe.Velocidade;
        SliderBarraDeVida.value = SliderBarraDeVida.maxValue = _scriptStatusChefe.VidaMaxima;
        SliderImageVida.color = ColorVidaMaxima;
    }

    private void Update()
    {
        _navChefe.SetDestination(_jogador.transform.position);
        _scriptControlaAnimacao.AnimarMovimento(_navChefe.velocity);

        if (_navChefe.hasPath)
        {
            bool pertoSuficiente = _navChefe.remainingDistance <= _navChefe.stoppingDistance;
            if (pertoSuficiente)
            {
                _scriptControlaAnimacao.Atacar(true);
                var direcao = _jogador.transform.position - _rigidbody.transform.position;


                _scriptMovimentaChefe.Rotacionar(direcao);
            }
            else
            {
                _scriptControlaAnimacao.Atacar(false);
            }
        }
    }
    private void AtacaJogador()
    {
        Quaternion inversaoDoSentidoDeRotacao = Quaternion.LookRotation(-transform.forward);
        int dano = Random.Range(30, 50);
        _jogador.GetComponent<ControlaJogador>().TomarDano(dano);
        _jogador.GetComponent<ControlaJogador>().Sangrar(inversaoDoSentidoDeRotacao);
    }

    public void TomarDano(int dano)
    {
        _scriptStatusChefe.Vida -= dano;
        AtualiazrBarraDeVida();

        if (_scriptStatusChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        _scriptControlaAnimacao.Morrer();
        _scriptMovimentaChefe.Morrer();
        _scriptControlaInterface.AtualizarQndZumbisMortos();

        GerarKitMedico(_porcentagemGerarKitMedico);

        _navChefe.enabled = false;
        _rigidbody.isKinematic = false;
        enabled = false;
        Destroy(gameObject, 2f);
    }

    private void AtualiazrBarraDeVida()
    {
        SliderBarraDeVida.value = _scriptStatusChefe.Vida;
        var porcentagemVida = _scriptStatusChefe.Vida / _scriptStatusChefe.VidaMaxima;
        SliderImageVida.color = Color.Lerp(ColorVidaMinima, ColorVidaMaxima, porcentagemVida);
    }
}

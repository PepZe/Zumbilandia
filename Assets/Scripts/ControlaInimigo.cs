using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    [HideInInspector]
    public GameObject Jogador;
    [HideInInspector]
    public Status _statusInimigo;

    public GameObject KitMedico;
    public Vector3 _direcao;
    private Vector3 _posicaoAleatoria;
    private Rigidbody _rigidbodyInimigo;
    private ControlaAnimacao _scriptControlaAnimacao;
    private MovimentaPersonagem _scriptMovimentaInimigo;
    private float _contadorVagar;
    private float _tempoEntrePosicoesAleatorias = 4;
    private float _porcentagemGerarKitMedico = 0.1f;

    public AudioClip SomDeMorte;

    private void Start()
    {
        Jogador = GameObject.FindWithTag(Tags.Jogador);

        _rigidbodyInimigo = GetComponent<Rigidbody>();

        _statusInimigo = GetComponent<Status>();
        _scriptControlaAnimacao = GetComponent<ControlaAnimacao>();
        _scriptMovimentaInimigo = GetComponent<MovimentaPersonagem>();

        AleatorizarZumbi();
    }
    void FixedUpdate()  
    {
        float distancia = Vector3.Distance(_rigidbodyInimigo.position, Jogador.transform.position);

        _scriptControlaAnimacao.AnimarMovimento(_direcao);
        _scriptMovimentaInimigo.Rotacionar(_direcao);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            _direcao = Jogador.transform.position - _rigidbodyInimigo.position;

            _scriptMovimentaInimigo.Movimentar(_direcao, _statusInimigo.Velocidade);
            _scriptControlaAnimacao.Atacar(false);
        }
        else
        {
            _direcao = Jogador.transform.position - _rigidbodyInimigo.position;
            _scriptControlaAnimacao.Atacar(true);
        }
    }

    void Vagar()
    {
        _contadorVagar -= Time.deltaTime;

        if (_contadorVagar <= 0)
        {
            _posicaoAleatoria = AleatorizarPosicao();
            _contadorVagar += _tempoEntrePosicoesAleatorias;
        }
        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, _posicaoAleatoria) <= 0.1;

        if (!ficouPertoOSuficiente)
        {
            _direcao = _posicaoAleatoria - transform.position;
            _scriptMovimentaInimigo.Movimentar(_direcao, _statusInimigo.Velocidade);
        }
    }
    Vector3 AleatorizarPosicao()
    {
        var posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
    void AtacaJogador()
    {
        int dano = Random.Range(20, 31);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }
    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        _statusInimigo.Vida -= dano;
        if (_statusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        GerarKitMedico(_porcentagemGerarKitMedico);

        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
    }

    private void GerarKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedico, transform.position, Quaternion.identity);
        }
    }
}

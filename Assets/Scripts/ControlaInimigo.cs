using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    [HideInInspector]
    public GameObject Jogador;
    [HideInInspector]
    public Status StatusInimigo;
    [HideInInspector]
    public GeradorZumbi MeuGerador;

    public GameObject KitMedico;
    public Vector3 Direcao;
    private Vector3 _posicaoAleatoria;

    private ControlaInterface _scriptControlaInterface;
    private ControlaAnimacao _scriptControlaAnimacao;
    private MovimentaPersonagem _scriptMovimentaInimigo;

    private float _contadorVagar;
    private float _tempoEntrePosicoesAleatorias = 4;
    private float _porcentagemGerarKitMedico = 0.1f;

    public AudioClip SomDeMorte;

    private void Start()
    {
        Jogador = GameObject.FindWithTag(Tags.Jogador);

        StatusInimigo = GetComponent<Status>();
        _scriptControlaAnimacao = GetComponent<ControlaAnimacao>();
        _scriptMovimentaInimigo = GetComponent<MovimentaPersonagem>();
        _scriptControlaInterface = FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        AleatorizarZumbi();
    }
    void FixedUpdate()  
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        _scriptControlaAnimacao.AnimarMovimento(Direcao);
        _scriptMovimentaInimigo.Rotacionar(Direcao);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            Direcao = Jogador.transform.position - transform.position;

            _scriptMovimentaInimigo.Movimentar(Direcao, StatusInimigo.Velocidade);
            _scriptControlaAnimacao.Atacar(false);
        }
        else
        {
            Direcao = Jogador.transform.position - transform.position;
            _scriptControlaAnimacao.Atacar(true);
        }
    }

    void Vagar()
    {
        _contadorVagar -= Time.deltaTime;

        if (_contadorVagar <= 0)
        {
            _posicaoAleatoria = AleatorizarPosicao();
            _contadorVagar += _tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }
        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, _posicaoAleatoria) <= 0.1;

        if (!ficouPertoOSuficiente)
        {
            Direcao = _posicaoAleatoria - transform.position;
            _scriptMovimentaInimigo.Movimentar(Direcao, StatusInimigo.Velocidade);
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
        StatusInimigo.Vida -= dano;
        if (StatusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        GerarKitMedico(_porcentagemGerarKitMedico);
        _scriptControlaInterface.AtualizarQndZumbisMortos();
        _scriptControlaAnimacao.Morrer();
        _scriptMovimentaInimigo.Morrer();

        MeuGerador.AtualizarQntZumbiVivos();
        Destroy(gameObject, 2f);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        enabled = false;
    }

    private void GerarKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedico, transform.position, Quaternion.identity);
        }
    }
}

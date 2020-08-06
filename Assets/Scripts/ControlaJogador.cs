using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    [HideInInspector]
    public Status _statusJogador;
    private GameObject _particulaSangue;
    private ControlaAnimacao _scriptControlaAnimacao;
    private MovimentaJogador _scriptMovimentaJogador;
    private ControlaInterface _scriptControlaInterface;
    public LayerMask MascaraChao;
    private Vector3 _direcao;
    public AudioClip SomDeDano;

    private const string PATH_SANGUE_JOGADOR = "Assets/Prefabs/ParticleSangueJogador.prefab";

    private void Start()
    {
        _particulaSangue = (GameObject)AssetDatabase.LoadAssetAtPath(PATH_SANGUE_JOGADOR, typeof(GameObject));

        _statusJogador = GetComponent<Status>();
        _scriptMovimentaJogador = GetComponent<MovimentaJogador>();
        _scriptControlaAnimacao = GetComponent<ControlaAnimacao>();
        _scriptControlaInterface = FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    void Update()
    {
        var eixoX = Input.GetAxis("Horizontal");
        var eixoZ = Input.GetAxis("Vertical");

        _direcao = new Vector3(eixoX, 0, eixoZ);
        _scriptControlaAnimacao.AnimarMovimento(_direcao);

    }
    private void FixedUpdate()
    {
        _scriptMovimentaJogador.Movimentar(_direcao, _statusJogador.Velocidade);
        _scriptMovimentaJogador.RotacionarJogador(MascaraChao);
    }
    public void TomarDano(int dano)
    {
        _statusJogador.Vida -= dano;
        _scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (_statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        _scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        _statusJogador.Vida += quantidadeDeCura;
        if (_statusJogador.Vida > _statusJogador.VidaMaxima)
        {
            _statusJogador.Vida = _statusJogador.VidaMaxima;
        }
        _scriptControlaInterface.AtualizarSliderVidaJogador();
    }

    public void Sangrar(Quaternion rotacao)
    {
        Instantiate(_particulaSangue, transform.position, rotacao);
    }
}

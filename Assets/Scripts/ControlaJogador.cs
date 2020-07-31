using Assets.Scripts;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    [HideInInspector]
    public Status _statusJogador;

    private ControlaAnimacao _scriptControlaAnimacao;
    private MovimentaJogador _scriptMovimentaJogador;
    public ControlaInterface ScriptControlaInterface;
    public LayerMask MascaraChao;
    private Vector3 _direcao;
    public AudioClip SomDeDano;

    private void Start()
    {
        _statusJogador = GetComponent<Status>();
        _scriptMovimentaJogador = GetComponent<MovimentaJogador>();
        _scriptControlaAnimacao = GetComponent<ControlaAnimacao>();
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
        ScriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (_statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        ScriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        _statusJogador.Vida += quantidadeDeCura;
        if(_statusJogador.Vida > _statusJogador.VidaMaxima)
        {
            _statusJogador.Vida = _statusJogador.VidaMaxima;
        }
        ScriptControlaInterface.AtualizarSliderVidaJogador();
    }
}

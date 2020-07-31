using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador _scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoFimDeJogo;
    public Text TextoPontuacaoMaxima;
    private float _tempoMaximoSobrevivencia;
    private const string MAXIMA_PONTUACAO = "Maxima Pontuacao";

    void Start()
    {
        _scriptControlaJogador = GameObject.FindWithTag(Tags.Jogador).GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = _scriptControlaJogador._statusJogador.Vida;
        AtualizarSliderVidaJogador();
        _tempoMaximoSobrevivencia = PlayerPrefs.GetFloat(MAXIMA_PONTUACAO);
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = _scriptControlaJogador._statusJogador.Vida;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        PainelDeGameOver.SetActive(true);

        (int min, int seg) = TempoDeJogo(Time.timeSinceLevelLoad);
        TextoFimDeJogo.text = $"Você sobreviveu por {min}min e {seg}s";
    }
    public Tuple<int, int> TempoDeJogo(float tempoDeJogo)
    {
        int min = (int)tempoDeJogo / 60;
        int seg = (int)tempoDeJogo % 60;

        return new Tuple<int, int>(min, seg);
    }

    void AjustarTempoMaximo()
    {
        if (Time.timeSinceLevelLoad > _tempoMaximoSobrevivencia)
        {
            _tempoMaximoSobrevivencia = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat(MAXIMA_PONTUACAO, Time.timeSinceLevelLoad);
        }

        (int min, int seg) = TempoDeJogo(_tempoMaximoSobrevivencia);
        TextoPontuacaoMaxima.text = $"Seu melhor tempo é {min}min e {seg}s";
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Zumbilandia");
    }
}

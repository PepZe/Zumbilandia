using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaMenu : MonoBehaviour
{
    public Button ButtonSair;
    void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        ButtonSair.gameObject.SetActive(true);
#endif
    }

    public void JogarJogo()
    {
        SceneManager.LoadScene("Zumbilandia");
    }

    public void SairDoJogo()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}

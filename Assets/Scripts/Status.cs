using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [HideInInspector]
    public float Vida;
    public float VidaMaxima = 100;
    public float Velocidade = 5;

    private void Awake()
    {
        Vida = VidaMaxima;
    }
}

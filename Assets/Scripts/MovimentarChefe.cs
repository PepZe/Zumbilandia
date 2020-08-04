using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarChefe : MovimentaPersonagem
{
    public new void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, Time.deltaTime * 10);
    }
}

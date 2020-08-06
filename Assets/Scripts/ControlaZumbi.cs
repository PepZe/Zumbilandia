using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{
    private const string PATH_KIT_MEDICO = "Assets/Prefabs/Itens/KitPrimeirosSocorros.prefab";
    private const string PATH_PARTICULA_SANGUE = "Assets/Prefabs/ParticleSangueZumbi.prefab";
    private GameObject _kitMedico;
    private GameObject _particulaSangue;

    void Awake()
    {
        _kitMedico = (GameObject)AssetDatabase.LoadAssetAtPath(PATH_KIT_MEDICO, typeof(GameObject));
        _particulaSangue = (GameObject)AssetDatabase.LoadAssetAtPath(PATH_PARTICULA_SANGUE, typeof(GameObject));
    }

    protected void GerarKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(_kitMedico, transform.position, Quaternion.identity);
        }
    }

    public void Sangrar(Quaternion rotacao)
    {
        Instantiate(_particulaSangue, transform.position, rotacao);
    }
}

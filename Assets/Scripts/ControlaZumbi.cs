using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{
    private const string PATH_KIT_MEDICO = "Assets/Prefabs/Itens/KitPrimeirosSocorros.prefab";
    private GameObject _kitMedico;

    void Awake()
    {
        _kitMedico = (GameObject)AssetDatabase.LoadAssetAtPath(PATH_KIT_MEDICO, typeof(GameObject));
    }

    protected void GerarKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(_kitMedico, transform.position, Quaternion.identity);
        }
    }
}

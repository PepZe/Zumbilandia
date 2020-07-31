using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaJogador : MovimentaPersonagem
{
    public void RotacionarJogador(LayerMask mascaraChao)
    {
        var raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(raio, out var impacto, 100, mascaraChao))
        {
            var posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Rotacionar(posicaoMiraJogador);
        }
    }
}

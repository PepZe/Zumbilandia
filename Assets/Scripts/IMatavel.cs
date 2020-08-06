using UnityEngine;

namespace Assets.Scripts
{
    public interface IMatavel
    {
        void TomarDano(int dano);
        void Morrer();
        void Sangrar(Quaternion rotacao);
    }
}

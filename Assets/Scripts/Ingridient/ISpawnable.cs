using UnityEngine;

namespace Ingridient
{
    public interface ISpawnable
    {
        void Spawn(Transform origin);
        bool Active();
    }
}
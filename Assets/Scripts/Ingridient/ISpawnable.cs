using UnityEngine;

namespace Ingridient
{
    public interface ISpawnable
    {
        void Spawn(Transform origin);
        void BackToPool();
        bool Active();
    }
}
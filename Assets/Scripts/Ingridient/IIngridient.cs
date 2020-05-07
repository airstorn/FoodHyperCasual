using UnityEngine;

namespace Ingridient
{
    public interface IIngridient
    {
        void Place(Vector3 pos);
        float GetHeight();
    }
}
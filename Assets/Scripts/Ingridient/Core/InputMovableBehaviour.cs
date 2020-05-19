using System;
using System.Collections;
using UnityEngine;

namespace Ingridient
{
    public class InputMovableBehaviour : MonoBehaviour, ISelectable, IPhysicable
    {
        protected Rigidbody _body;

        private IEnumerator move;

        protected virtual void Start()
        {
            _body = GetComponent<Rigidbody>();
            _body.isKinematic = true;
        }
        public void Move(Vector3 pos)
        {
            transform.position = pos;
            _body.velocity = Vector3.zero;
            
        }

        public void Rotate(Vector3 euler)
        {
            transform.rotation = Quaternion.Euler(euler);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Rigidbody GetBody()
        {
            return _body;
        }
    }
}
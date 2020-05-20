using System;
using System.Collections;
using UnityEngine;

namespace Ingridient
{
    public class InputMovableBehaviour : MonoBehaviour, ISelectable, IPhysicable
    {
        public Action<bool> OnSelectedAction { get; set; }

        protected bool _selected;
        protected IInteractableZone _currentZone;
        private Rigidbody _body;

        protected virtual void Start()
        {
            _body = GetComponent<Rigidbody>();
            _body.isKinematic = true;
        }
        
        public bool SetInteractableZone(IInteractableZone zone)
        {
            _currentZone?.InteractWith(this, InteractableZoneArgs.Remove);
            
            var added = zone?.InteractWith(this, InteractableZoneArgs.Add);
            
            Debug.Log(added);
            if (added != null)
            {
                if (added == true)
                {
                    _currentZone = zone;
                }
                return added == true;
            }

            return false;
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

        public Vector3 GetRotation()
        {
            return transform.rotation.eulerAngles;
        }



        public Rigidbody GetBody()
        {
            return _body;
        }
    }
}
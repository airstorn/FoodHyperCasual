using UnityEngine;

namespace Ingridient
{
    public abstract class InputMovableBehaviour : MonoBehaviour, ISelectable
    {
        [SerializeField] private DefaultIngridientZone _zone;
        protected Quaternion _rotation;
        protected Vector3 _position;

        protected Vector3 _originPos;
        protected Quaternion _originRot;
        
        protected virtual void Start()
        {
            _originPos = transform.position;
            _originRot = transform.rotation;
            _position = _originPos;
            _rotation = _originRot;
        }
        
        public virtual void OnSelected(bool selected)
        {
            if (selected)
            {
                InputHandler.OnTouchMoved += Move;
            }
            else
            {
                InputHandler.OnTouchMoved -= Move;
                _position = _originPos;
            }
        }
        protected virtual void Move(Vector3 pos)
        {
            _position = new Vector3(pos.x,pos.y, transform.position.z);  
        }
        protected virtual void Update()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                _rotation, 10 * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, _position, 20 * Time.deltaTime);
        }
    }
}
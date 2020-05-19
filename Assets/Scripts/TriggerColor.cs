using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class TriggerColor : MonoBehaviour
{
   [SerializeField] private Color _drawColor;
   private BoxCollider _collider;

   private void Start()
   {
      _collider = GetComponent<BoxCollider>();
   }

   private void OnDrawGizmos()
   {
      if(_collider == null)
         return;
      var matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.color = _drawColor;
      Gizmos.DrawCube(_collider.center, _collider.size);
   }
}

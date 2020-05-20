using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    void Move(Vector3 pos);
    void Rotate(Vector3 euler);
    Vector3 GetPosition();
    Vector3 GetRotation();
    Action<bool> OnSelectedAction { get; set; }
    bool SetInteractableZone(IInteractableZone zone);
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public void SetVisible(bool visible)
    {
        _anim.SetBool("visible", visible);
    }
}

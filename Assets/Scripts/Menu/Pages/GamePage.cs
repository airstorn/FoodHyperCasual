using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameStates
{
    public class GamePage : PageBasement, IMenuPagable
    {
        private void Start()
        {
            InputHandler.OnTouchMoved += CheckUI;
        }

        private void CheckUI(Vector3 obj)
        {
        }
    }
}

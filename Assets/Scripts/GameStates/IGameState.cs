using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
   void Activate(Action activatAction);
   void Deactivate(Action callback);   
}

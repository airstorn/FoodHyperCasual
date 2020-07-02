using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
   void Activate(Action activateAction);
   void Deactivate(Action callback);   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates;

public class MenuPage : PageBasement, IMenuPagable
{
   public void StartGameButton()
   {
      GameLogic.Instance.ChangeState<PlayState>();
   }
}

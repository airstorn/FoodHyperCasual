using System.Collections;
using System.Collections.Generic;
using Core;
using GameStates;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndPage : PageBasement, ILevelListener
{
    [SerializeField] private Image _bg;
    [SerializeField] private Text _levelPassedText;
    [SerializeField] private Text _levelCompletionText;
    [SerializeField] private Image _levelCompleteness;

   public override void Show<T>(T args)
   {
      base.Show(args);
      StartCoroutine(AnimateStart());
   }

   private IEnumerator AnimateStart()
   {
       MovingUtility.FloatLerpContainer bg = new MovingUtility.FloatLerpContainer()
       {
           Duration = 1,
           StartValue = 0,
           TargetValue = 1
       }; 
       
       MovingUtility.FloatLerpContainer completeness = new MovingUtility.FloatLerpContainer()
       {
           Duration = 2f,
           StartValue = 0,
           TargetValue = (float)LevelStatus.Instance.SuccessfulCustomers / (float)LevelStatus.Instance.TotalCustomers
       };
       

       MovingUtility.LerpFloat(completeness, LevelFillAmount);
       MovingUtility.LerpFloat(completeness, PercentsAmount);
       yield return MovingUtility.LerpFloat(bg, SetBackgroundAlpha);
   }

   private void PercentsAmount(float obj)
   {
       _levelCompletionText.text = Mathf.Round(obj * 100) + "% satisfied";
   }

   private void LevelFillAmount(float obj)
   {
       _levelCompleteness.fillAmount = obj;
   }

   private void SetBackgroundAlpha(float obj)
   {
       _bg.color = new Color(1,1,1, obj);
   }

   public void NextLevelButton()
   {
       Debug.Log("Changed");
       GameLogic.Instance.ChangeState<PlayState>();
   }

   public void SetLevel(int level)
   {
       _levelPassedText.text = "Level " + (level - 1) + " passed!";
   }
}

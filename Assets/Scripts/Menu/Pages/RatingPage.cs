using System;
using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

public class RatingPage : PageBasement
{
    [SerializeField] private GameLogic _logic;
    [SerializeField] private CanvasGroup _bg;
    [SerializeField] private Image _rating;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Text _percentsText;

    public override void Show<T>(T args)
    {
        base.Show(args);
        _bg.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);

        _rating.fillAmount = 0;
        _percentsText.text = 0 + "%";
        
        var f = float.Parse(args.ToString());

        StartCoroutine(AnimateRating(f));
        
    }

    public void ContinueButton()
    {
         _logic.ChangeState<PlayState>();
    }

    private IEnumerator AnimateRating(float rating)
    {
        MovingUtility.FloatLerpContainer bg = new MovingUtility.FloatLerpContainer()
        {
            Duration = 1,
            StartValue = 0,
            TargetValue = 1
        }; 
        
        MovingUtility.FloatLerpContainer lerp = new MovingUtility.FloatLerpContainer()
        {
            Duration = 1,
            StartValue = 0,
            TargetValue = rating
        };
        
        yield return MovingUtility.LerpFloat(bg, BackGroundAlpha);
        _continueButton.gameObject.SetActive(true);
        yield return MovingUtility.LerpFloat(lerp, Rating);

    }

    private void Rating(float obj)
    {
        _rating.fillAmount = obj;
        _percentsText.text = (int)(obj * 100) + "%";
    }

 

    public override void Hide()
    {
        base.Hide();
        _bg.gameObject.SetActive(false);
        
    }

    private void BackGroundAlpha(float alpha)
    {
        _bg.alpha = alpha;
    }
}

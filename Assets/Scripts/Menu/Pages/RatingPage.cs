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
    [SerializeField] private Text _moneyText;
    struct RatingPayload
    {
        public float Percent;
        public int Money;
    }

    public override void Show<T>(T args)
    {
        base.Show(args);

        _bg.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);

        _rating.fillAmount = 0;
        _percentsText.text = 0 + "%";
        _moneyText.text = "+0";
        
        var f = float.Parse(args.ToString());
        var money = (f * 100) / 2;
        
        GameLogic.Instance.MoneyData.AddMoney((int)money);

        StartCoroutine(AnimateRating(f, money));
        
    }

    public void ContinueButton()
    {
         _logic.ChangeState<PlayState>();
    }

    private IEnumerator AnimateRating(float rating, float money)
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
        
        MovingUtility.FloatLerpContainer lerpMoney = new MovingUtility.FloatLerpContainer()
        {
            Duration = 2,
            StartValue = 0,
            TargetValue = money
        };
        
        yield return MovingUtility.LerpFloat(bg, BackGroundAlpha);
        _continueButton.gameObject.SetActive(true);
        MovingUtility.LerpFloat(lerp, Rating);
        yield return new WaitForSeconds(0.5f);
        MovingUtility.LerpFloat(lerpMoney, LerpMoney);

    }

    private void LerpMoney(float obj)
    {
        _moneyText.text = "+" + (int)obj;
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

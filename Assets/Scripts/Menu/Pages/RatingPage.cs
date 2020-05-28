﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

public class RatingPage : PageBasement
{
    [SerializeField] private CanvasGroup _bg;
    [SerializeField] private Image _rating;

    public override void Show<T>(T args)
    {
        base.Show(args);
        _bg.gameObject.SetActive(true);
        var f = float.Parse(args.ToString());

        StartCoroutine(AnimateRating(f));
        
    }

    private IEnumerator AnimateRating(float rating)
    {
        yield return StartCoroutine(AnimateFloat(0, 1, 1, BackGroundAlpha));
        StartCoroutine(AnimateFloat(0, rating, 5, Rating));
    }

    private void Rating(float obj)
    {
        _rating.fillAmount = obj;
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

    private IEnumerator AnimateFloat(float value, float target, int duration, Action<float> callback)
    {
        float time = 0;
        while (time < duration)
        {
            value = Mathf.Lerp(value, target, time / duration);
            callback?.Invoke(value);
            
            time += Time.deltaTime;
            yield return null;
        }
    }
}

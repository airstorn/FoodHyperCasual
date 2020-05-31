using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovingUtility 
{
    public struct MovingContainer
    {
        public Vector3 OriginPos;
        public Vector3 TargetPos;
        public float Duration;
    }
    
    public struct RotationContainer
    {
        public Quaternion CurrentRotation;
        public Vector3 TargetRotation;
        public float Duration;
    }
    
    public struct FloatLerpContainer
    {
        public float StartValue;
        public float TargetValue;
        public float Duration;
    }
    
    public static IEnumerator MoveTo(MovingContainer data, Action<Vector3> callback)
    {
        float elapsed = 0;
        while (elapsed < data.Duration)
        {
            data.OriginPos = Vector3.Lerp(data.OriginPos, data.TargetPos, elapsed / data.Duration);
            
            callback?.Invoke(data.OriginPos);
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        data.OriginPos = data.TargetPos;
        callback.Invoke(data.OriginPos);
    }

    public static IEnumerator Rotate(RotationContainer data, Action<Quaternion> callback)
    {
        float elapsed = 0;
        while (elapsed < data.Duration)
        {
            data.CurrentRotation = Quaternion.Lerp(data.CurrentRotation, Quaternion.Euler(data.TargetRotation), elapsed / data.Duration);
            
            callback?.Invoke(data.CurrentRotation);
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        data.CurrentRotation = Quaternion.Euler(data.TargetRotation);
        callback?.Invoke(data.CurrentRotation);
    }

    public static IEnumerator LerpFloat(FloatLerpContainer data, Action<float> callback, Action endCallback = null)
    {
        float elapsed = 0;
        while (elapsed < data.Duration)
        {
            data.StartValue = Mathf.Lerp(data.StartValue, data.TargetValue, elapsed / data.Duration);
            
            callback.Invoke(data.StartValue);
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        data.StartValue = data.TargetValue;
        callback.Invoke(data.StartValue);
        endCallback?.Invoke();
    }
}

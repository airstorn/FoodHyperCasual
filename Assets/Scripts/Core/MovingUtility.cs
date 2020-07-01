using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovingUtility
{
    private static MonoBehaviour _routinesObject;
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

    public static Coroutine MoveTo(MovingContainer data, Action<Vector3> callback, Action endCallback = null)
    {
        CheckForObject();
        return _routinesObject.StartCoroutine(MoveToRoutine(data, callback, endCallback));
    }
    
    public static Coroutine Rotate(RotationContainer data, Action<Quaternion> callback)
    {
        CheckForObject();
        return _routinesObject.StartCoroutine(RotateRoutine(data, callback));
    } 
    
    public static Coroutine LerpFloat(FloatLerpContainer data, Action<float> callback, Action endCallback = null)
    {
        CheckForObject();
        return _routinesObject.StartCoroutine(LerpFloatRoutine(data, callback, endCallback));
    }

    private static void CheckForObject()
    {
        if (!_routinesObject)
        {
            _routinesObject = new GameObject().AddComponent<CoroutineHelper>();
        }
    }

    private static IEnumerator MoveToRoutine(MovingContainer data, Action<Vector3> callback, Action endCallback)
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
        endCallback?.Invoke();
    }

    private static IEnumerator RotateRoutine(RotationContainer data, Action<Quaternion> callback)
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

    private static IEnumerator LerpFloatRoutine(FloatLerpContainer data, Action<float> callback, Action endCallback = null)
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

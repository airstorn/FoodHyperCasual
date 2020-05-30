using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovingUtility 
{
    public struct MovingContainer
    {
        public Vector3 originPos;
        public Vector3 targetPos;
        public float duration;
    }
    public static IEnumerator MoveTo(MovingContainer data, Action<Vector3> callback)
    {
        float elapsed = 0;
        while (elapsed < data.duration)
        {
            data.originPos = Vector3.Lerp(data.originPos, data.targetPos, elapsed / data.duration);
            
            callback?.Invoke(data.originPos);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

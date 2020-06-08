using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Ingridient;
using UnityEngine;

public class SpawnableIngridient : InputMovableBehaviour, IIngridient, ISpawnable, IRatable
{  
    [SerializeField] private bool _placed = false;
    [SerializeField] private float _height = 0.05f;
    [SerializeField] private AnimationCurve _spawnAnim;
    [SerializeField] private AnimationCurve _depawnAnim;
    public float GetHeight()
    {
        return _height;
    }

    public void Spawn(Transform origin)
    {
        transform.position = origin.position;
        transform.rotation = origin.rotation;

        gameObject.SetActive(true);

        var spawnSize = new MovingUtility.FloatLerpContainer()
        {
            Duration = 1f,
            StartValue = 0,
            TargetValue = 1
        };
        
        MovingUtility.LerpFloat(spawnSize, SpawnAnimation);
    }

    public void Despawn()
    {
        var despawnAnim = new MovingUtility.FloatLerpContainer()
        {
            Duration = 1f,
            StartValue = 0,
            TargetValue = 1
        };
        
        Spawner.Instance.RemoveFromWaiting(this);
        
         MovingUtility.LerpFloat(despawnAnim, DespawnAnimation, ()  => Destroy(gameObject));
    }
    
    public virtual float GetRating()
    {
        return 1;
    }

    private void SpawnAnimation(float delta)
    {
        transform.localScale = Vector3.one * _spawnAnim.Evaluate(delta);
    }

    private void DespawnAnimation(float delta)
    {
        transform.localScale = Vector3.one * _depawnAnim.Evaluate(delta);
    }

  
}

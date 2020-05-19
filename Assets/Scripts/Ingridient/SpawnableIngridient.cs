using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class SpawnableIngridient : InputMovableBehaviour, IIngridient, ISpawnable
{  
    [SerializeField] private bool _placed = false;
    [SerializeField] private float _height = 0.05f;
    [SerializeField] private AnimationCurve _spawnAnim;
    public float GetHeight()
    {
        return _height;
    }

    public void Spawn(Transform origin)
    {
        transform.position = origin.position;
        transform.rotation = origin.rotation;

        gameObject.SetActive(true);

        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation()
    {
        float elapsed = 1;
        float time = 0;
        while (time < elapsed)
        {
            transform.localScale = Vector3.one * _spawnAnim.Evaluate(time / elapsed);
            
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
    }

    public bool Active()
    {
        return gameObject.activeInHierarchy;
    }
}

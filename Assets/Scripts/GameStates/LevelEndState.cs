using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class LevelEndState : MonoBehaviour, IGameState
    {
        [SerializeField] private GameObject _ui;
        private LevelManager _levelManager;
    
        public void Activate(Action activateAction)
        {
            StartCoroutine(ActivationAnimate(activateAction));
        }

        private IEnumerator ActivationAnimate(Action callback)
        {
            yield return null;

            _levelManager.SetLevel(_levelManager.CurrentLevel + 1);
            
            Menu.Instance.SwitchPage<LevelEndPage>();
            callback?.Invoke();
        }

        public void Deactivate(Action callback = null)
        {
            LevelStatus.Instance.ResetLevelData();
            _ui.SetActive(false);
            callback?.Invoke();
        }
    
        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }
    }
}

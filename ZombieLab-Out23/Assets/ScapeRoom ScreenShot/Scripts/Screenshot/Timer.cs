using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Timer : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float totalTime;

        [Header("Debug")]
        public int progress;
        public bool isDone;

        private Action _onFinishTimer;

        public void StartTimer(Action onFinishTimer)
        {
            _onFinishTimer = onFinishTimer;

            StartCoroutine(TimerProcess());
        }

        private IEnumerator TimerProcess()
        {
            float elapsedTime = totalTime;
            isDone = false;

            while(elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                progress = Mathf.RoundToInt(elapsedTime);
                yield return null;
            }

            isDone = true;
            _onFinishTimer?.Invoke();
        }
    }
}
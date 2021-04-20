using System.Collections;
using UnityEngine;
using TMPro;

namespace DefaultNamespace
{
    public class TimerUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject textGo;

        private Timer _timer;

        public void SetTimer(Timer timer)
        {
            _timer = timer;

            textGo.SetActive(true);

            StartCoroutine(UpdateTimerText());
        }

        private IEnumerator UpdateTimerText()
        {
            WaitForSeconds delay = new WaitForSeconds(1);

            while (!_timer.isDone)
            {
                timerText.text = _timer.progress.ToString();
                yield return delay;
            }

            timerText.gameObject.SetActive(false);
        }
    }
}
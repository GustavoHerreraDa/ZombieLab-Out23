using UnityEngine;

namespace DefaultNamespace
{
    public class ScreenshotFecade : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Timer timer;
        [SerializeField] private TimerUI timerUI;
        [SerializeField] private ScreenshotTaker screenshotTaker;
        [SerializeField] private Transform cam;

        public void TakeScreenshot()
        {
            LookatCamera[] looks = FindObjectsOfType<LookatCamera>();
            foreach (LookatCamera look in looks)
            {
                look.SetCaM(cam);
            }
            timer.StartTimer(screenshotTaker.TakeScreenshot);
            timerUI.SetTimer(timer);
        }
    }
}
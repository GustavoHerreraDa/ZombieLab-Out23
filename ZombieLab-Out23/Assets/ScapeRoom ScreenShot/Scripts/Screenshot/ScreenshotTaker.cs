using UnityEngine;

namespace DefaultNamespace
{
    public class ScreenshotTaker : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private ScreenshotConfigurationUI screenshotViewerUI;

        public void TakeScreenshot()
        {
            int width = renderTexture.width;
            int height = renderTexture.height;

            TextureFormat tFormat = TextureFormat.RGB24;
            Texture2D screenshot = new Texture2D(width, height, tFormat, false);

            RenderTexture.active = renderTexture;

            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();

            Sprite screenShotSprite = Sprite.Create(screenshot, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100.0f);

            screenshotViewerUI.Show(screenShotSprite);
        }
    }
}
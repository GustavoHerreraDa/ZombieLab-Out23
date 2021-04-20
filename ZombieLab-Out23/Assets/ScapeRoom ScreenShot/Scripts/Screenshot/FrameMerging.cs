using UnityEngine;

namespace DefaultNamespace
{
    public class FrameMerging : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private RenderTexture renderTexture;

        private Texture2D TakeScreenshot()
        {
            int width = renderTexture.width;
            int height = renderTexture.height;

            TextureFormat tFormat = TextureFormat.RGB24;
            Texture2D screenshot = new Texture2D(width, height, tFormat, false);

            RenderTexture.active = renderTexture;

            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();

            return screenshot;
        }

        public Texture2D GetTexture()
        {
            return TakeScreenshot();
        }
    }
}
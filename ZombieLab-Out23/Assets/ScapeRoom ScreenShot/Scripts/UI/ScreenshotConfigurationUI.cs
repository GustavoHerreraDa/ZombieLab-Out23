using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ScreenshotConfigurationUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Image screenshotImage;
        [SerializeField] private CertificateCreator certificateCreator;
        [SerializeField] private FrameSlots frameSlots;
        [SerializeField] private FrameMerging frameMerging;
        [SerializeField] private GameObject content;

        private void Awake()
        {
            content.SetActive(false);
        }

        public void Show(Sprite screenshotSprite)
        {
            frameMerging.gameObject.SetActive(true);
            screenshotImage.sprite = screenshotSprite;
            CursorControl.ShowCursor(true);
            content.SetActive(true);
        }

        public void DownloadCertificate()
        {
            certificateCreator.CreateCertificate(frameMerging.GetTexture());
        }
    }
}
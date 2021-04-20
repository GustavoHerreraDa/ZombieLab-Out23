using UnityEngine;
using TMPro;

namespace DefaultNamespace
{
	public class InfoScreenshotUI : MonoBehaviour
	{
		[Header("Config")]
		[SerializeField] private TextMeshProUGUI pathText;

		private string _lastScreenshotPath;

		public void Show(string path)
        {
			pathText.text = "La foto se guardo en: " + path;

			gameObject.SetActive(true);

			_lastScreenshotPath = path;
        }

		public void Open()
        {
			Application.OpenURL(_lastScreenshotPath);
        }

	}
}
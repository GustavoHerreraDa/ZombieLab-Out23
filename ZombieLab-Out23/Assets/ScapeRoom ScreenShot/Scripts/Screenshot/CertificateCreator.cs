using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
	public class CertificateCreator : MonoBehaviour
	{
		[Header("config")]
		[SerializeField] private InfoScreenshotUI infoScreenshotUI;

		public void CreateCertificate(Texture2D screenshot, Texture2D frame = null)
        {
			Texture2D certificate;
			if (null == frame)
				certificate = screenshot;
			else
				certificate = TextureMergering.MergeTextures(screenshot, frame);

			string certificateName = CertificateName();

			string path = GetPath(certificateName);

			byte[] bytes = certificate.EncodeToPNG();

			File.WriteAllBytes(path, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", path));

			infoScreenshotUI.Show(path);
		}

		public string CertificateName()
		{
			string cerName = "Certificado " + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
			cerName += ".png";
			return cerName;
		}

		public string GetPath(string cerName)
        {
			string path = "";
#if UNITY_EDITOR
			path = Application.streamingAssetsPath;
#else
			path = Application.persistentDataPath;
#endif

            if (!Directory.Exists(path))
            {
				Directory.CreateDirectory(path);
            }

			path = Path.Combine(path, cerName);

			return path;
	
		}
	}
}
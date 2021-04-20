using UnityEngine;

namespace DefaultNamespace
{
	public static class TextureMergering
	{
        public static Texture2D MergeTextures(Texture2D img, Texture2D overlay)
        {
            Debug.Log("Screenshot : " + img.width + " x " + img.height);
            Debug.Log("Frame : " + overlay.width + " x " + overlay.height);
            Texture2D tex = img;

            Color[] cols1 = img.GetPixels();
            Color[] cols2 = overlay.GetPixels();

            float rOut;
            float gOut;
            float bOut;
            float aOut;

            for (var i = 0; i < cols1.Length; ++i)
            {               
                rOut = (cols2[i].r * cols2[i].a) + (cols1[i].r * (1 - cols2[i].a));
                gOut = (cols2[i].g * cols2[i].a) + (cols1[i].g * (1 - cols2[i].a));
                bOut = (cols2[i].b * cols2[i].a) + (cols1[i].b * (1 - cols2[i].a));
                aOut = cols2[i].a + (cols1[i].a * (1 - cols2[i].a));

                cols1[i] = new Color(rOut, gOut, bOut, aOut);
            }
            tex.SetPixels(cols1);
            tex.Apply();

            return tex;
        }
    }
}
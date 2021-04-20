using UnityEngine;

namespace DefaultNamespace
{
	public static class SpriteExtension
	{
		public static Texture2D ToTexture(this Sprite sprite)
        {
            var texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                    (int)sprite.textureRect.y,
                                                    (int)sprite.textureRect.width,
                                                    (int)sprite.textureRect.height);
            texture.SetPixels(pixels);
            texture.Apply();
            texture.EncodeToPNG();

            return texture;
        }
	}
}
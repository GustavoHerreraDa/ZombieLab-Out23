using UnityEngine;
using Utils;

namespace SpriteViewer
{
	public abstract class RemoteSpriteViewer : MonoBehaviour
	{
		[Header("Config")]
		[SerializeField] private SpriteContainer spriteContainer;
		[SerializeField] private SpriteRenderer spriteRenderer;

		public void Show(int index)
        {
			print("Emoji " + index);
			if (index < 0)
				spriteRenderer.sprite = null;
			else
				spriteRenderer.sprite = spriteContainer.sprites[index];
		}
	}
}
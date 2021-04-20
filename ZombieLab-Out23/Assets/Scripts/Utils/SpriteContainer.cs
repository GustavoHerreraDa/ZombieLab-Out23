using UnityEngine;

namespace Utils
{
	[CreateAssetMenu(fileName = "SpriteContainer", menuName = "Utils/SpriteContainer")]
	public class SpriteContainer : ScriptableObject
	{
		[Header("Config")]
		public Sprite[] sprites;
	}
}
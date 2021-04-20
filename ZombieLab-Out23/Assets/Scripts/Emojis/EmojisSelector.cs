using UnityEngine;

namespace SpriteViewer.Emojis
{
	public class EmojisSelector : MonoBehaviour
	{
		[Header("Config")]
		[SerializeField] private EmojiSlots emojiSlots;

        private void Update()
        {
            for (int i = 0; i < 10; i++)
            {
                if(Input.GetKeyDown((KeyCode)48 + i))
                {
                    emojiSlots.SelectEmoji(i);
                    //gameObject.SetActive(false);
                }
            }
        }
    }
}

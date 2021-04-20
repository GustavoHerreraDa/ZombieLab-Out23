using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpriteViewer.Emojis
{
    public class EmojiSlot : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private GameObject selectionFrame;

        public void SetSlot(Sprite sprite, int index)
        {
            image.sprite = sprite;
            keyText.text = index.ToString();
        }

        public void Select()
        {
            selectionFrame.SetActive(true);
        }

        internal void Deselect()
        {
            selectionFrame.SetActive(false);
        }
    }
}

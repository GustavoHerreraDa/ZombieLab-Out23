using UnityEngine;
using UnityEngine.UI;
using Utils;
using System.Collections.Generic;

namespace SpriteViewer.Emojis
{
    public class EmojiSlots : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private LayoutGroup layoutGroup;
        [SerializeField] private GameObject emojiSlot;
        [SerializeField] private SpriteContainer spriteContainer;
        [SerializeField] private LocalSpriteViewer localSpriteViewer;

        [Header("config")]
        [SerializeField] private EmojiSlot _currentSlot;
        private readonly List<EmojiSlot> slots = new List<EmojiSlot>();

        private void Start()
        {
            SetSlots();
        }

        private void SetSlots()
        {

            for (int i = 0; i < spriteContainer.sprites.Length; i++)
            {
                EmojiSlot emoji =  Instantiate(emojiSlot, layoutGroup.transform).GetComponent<EmojiSlot>();
                
                emoji.SetSlot(spriteContainer.sprites[i], i);

                slots.Add(emoji);
            }

            for (int i = 0; i < spriteContainer.sprites.Length; i++)
            {
                layoutGroup.transform.GetChild(i).SetAsFirstSibling();
            }
        }

        public void SelectEmoji(int index)
        {
            _currentSlot?.Deselect();

            if(_currentSlot == slots[index])
            {
                _currentSlot = null;
                localSpriteViewer.Show(null, -1);
            }
            else
            {
                _currentSlot = slots[index];
                _currentSlot.Select();
                localSpriteViewer.Show(spriteContainer.sprites[index], index);
            } 
        }
    }
}
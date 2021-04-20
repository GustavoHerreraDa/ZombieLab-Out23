using UnityEngine;

namespace ScapeRoom.Common.UI.SlotsSystem
{
    public abstract class SlotUIData : ScriptableObject
    {
        [Header("Config")]
        public Sprite sprite;
        public bool isEmpty;
    }
}
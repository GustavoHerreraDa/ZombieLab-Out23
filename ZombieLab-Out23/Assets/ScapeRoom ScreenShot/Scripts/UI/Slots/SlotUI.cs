using UnityEngine;
using UnityEngine.UI;

namespace ScapeRoom.Common.UI.SlotsSystem
{
    public abstract class SlotUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] protected Image image;
        [SerializeField] protected float minSize;

        [Header("Debug")]
        public SlotUIData slotUIData;

        private Slots _slots;
        private Vector3 _startSize;
        private Vector3 _minScaleSize;

        private void Awake()
        {
            _startSize = transform.localScale;
            _minScaleSize = _startSize - (_startSize * minSize);
        }

        public virtual void Select()
        {
            _slots.SetCurrentSlotUI(this);
            transform.localScale = _minScaleSize;
        }

        public virtual void Deselect()
        {
            transform.localScale = _startSize;
        }

        public void SetSlot(Slots slots, SlotUIData slotUIData)
        {
            _slots = slots;
            this.slotUIData = slotUIData;

            SetUI();
        }

        protected abstract void SetUI();
    }
}
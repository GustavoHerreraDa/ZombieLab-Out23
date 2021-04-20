using ScapeRoom.Common.UI.SlotsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FrameSlots : Slots
    {
        [Header("Config")]
        [SerializeField] private FrameSlotsData slots;
        [SerializeField] private Image frameUI;
        [SerializeField] private Scrollbar scrollbar;

        private void Start()
        {
            DrawSlots(slots.slotUIDatas);
        }

        protected override void OnFinishSlotLoad()
        {
            scrollbar.value = 1;
        }

        protected override void OnSelectSlot()
        {
            if (selectedSlotUI.slotUIData.isEmpty)
            {
                SetColorAlpha(0);
            }
            else
            {
                frameUI.sprite = selectedSlotUI.slotUIData.sprite;
                SetColorAlpha(255);
            }
        }

        private void SetColorAlpha(float a)
        {
            Color color = frameUI.color;
            color.a = a;
            frameUI.color = color;
        }
    }
}
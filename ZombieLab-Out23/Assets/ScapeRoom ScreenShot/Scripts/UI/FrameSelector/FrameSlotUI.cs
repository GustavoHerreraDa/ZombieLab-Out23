using ScapeRoom.Common.UI.SlotsSystem;

namespace DefaultNamespace
{
    public class FrameSlotUI : SlotUI
    {
        protected override void SetUI()
        {
            image.sprite = slotUIData.sprite;
        }
    }
}
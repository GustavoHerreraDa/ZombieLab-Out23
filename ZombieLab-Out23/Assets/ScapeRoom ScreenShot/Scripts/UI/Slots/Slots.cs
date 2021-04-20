using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ScapeRoom.Common.UI.SlotsSystem
{
	public abstract class Slots : MonoBehaviour
	{
        [Header("Config")]       
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform slotContainer;

        [Header("Debug")]
        public SlotUI selectedSlotUI;
        [SerializeField] private SlotUIData[] slotUIDatas;
        [SerializeReference] private bool isDrawing;

        public void SetCurrentSlotUI(SlotUI slotUI)
        {
            if (selectedSlotUI != null) selectedSlotUI.Deselect();

            selectedSlotUI = slotUI;

            OnSelectSlot();
        }

        public void Deselect()
        {
            if (null == selectedSlotUI) return;

            selectedSlotUI.Deselect();
            selectedSlotUI = null;
        }

        protected abstract void OnSelectSlot();

        public void DrawSlots(SlotUIData[] slotUIDatas)
        {
            this.slotUIDatas = slotUIDatas;

            if(isDrawing)
            {
                Reset();
            }

            SetSlots();
        }

        private void Reset()
        {
            for (int i = 0; i < slotContainer.childCount; i++)
            {
                Destroy(slotContainer .GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Set the inventory with the charmsUIData
        /// </summary>
        private void SetSlots()
        {
            InstatiateSlotUI();
        }

        /// <summary>
        /// Instantiate all the charmUI in the grid
        /// </summary>
        private void InstatiateSlotUI()
        {
            int SlotUIDatalength = slotUIDatas.Length;

            for (int i = 0; i < SlotUIDatalength; i++)
            {
                GameObject slotUI = Instantiate(slotPrefab, slotContainer);

                slotUI.GetComponent<SlotUI>().SetSlot(this, slotUIDatas[i]);

                if(i == 0)
                    selectedSlotUI = slotUI.GetComponent<SlotUI>();
            }

            isDrawing = true;

            StartCoroutine(SetScrollvarValue());
        }

        private IEnumerator SetScrollvarValue()
        {
            yield return new WaitForEndOfFrame();
            OnFinishSlotLoad();
        }

        protected virtual void OnFinishSlotLoad() { }
    }
}
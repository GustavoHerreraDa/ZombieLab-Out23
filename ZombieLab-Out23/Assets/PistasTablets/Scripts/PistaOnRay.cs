using EnhancedScrollerDemos.ExpandingCells;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PistaOnRay : OnRay
    {
        [Header("UI Config")]
        public GameObject canvasTablet;
        private Controller listController;

        private void Start()
        {
            listController = GetComponent<Controller>();
        }
        public override void OnRayEnter()
        {
            ShowCursor(true);
            canvasTablet.SetActive(true);
            listController.InitList();
            StartCoroutine(WaitToExit());
        }

        public override void OnRayExit()
        {
            _raycastManager.isAlreadyEnter = false;

            ShowCursor(false);

            canvasTablet.SetActive(false);
        }

        private IEnumerator WaitToExit()
        {
            bool xIsPressed = false;
            while (!xIsPressed)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    xIsPressed = true;

                    OnRayExit();
                }

                yield return null;
            }
        }

        /// <summary>
        /// Set Cursor visible
        /// </summary>
        /// <param name="state"></param>
        public void ShowCursor(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
        }
    }
}
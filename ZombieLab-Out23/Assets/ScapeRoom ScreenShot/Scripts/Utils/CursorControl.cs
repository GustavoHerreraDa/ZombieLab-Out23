using UnityEngine;

namespace DefaultNamespace
{
	public static class CursorControl
	{
        private static bool forceCursor = false;

        /// <summary>
        /// Set Cursor visible
        /// </summary>
        /// <param name="state"></param>
        public static void ShowCursor(bool state)
        {
            //Force the cursor asi no se sale
            if (forceCursor && !state) return;

            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
        }

        public static void ForceCursor(bool state)
        {
            forceCursor = state;
        }

    }
}
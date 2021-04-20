using UnityEngine;

namespace DefaultNamespace
{
	public class FreezeCharacter : MonoBehaviour
	{
		[SerializeField] private playerFps playerFps;

        private void OnEnable()
        {
            playerFps.blockMove = true;
            playerFps.canMove = false;
        }

        private void OnDisable()
        {
            playerFps.blockMove = false;
            playerFps.canMove = true;
        }
    }
}
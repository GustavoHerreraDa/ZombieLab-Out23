using UnityEngine;

namespace DefaultNamespace
{
	public abstract class OnRay : MonoBehaviour
	{
		[Header("Config")]
		public float distToHit = 100;
		public string infoText = "Presiona click derecho para";

		protected RaycastManager _raycastManager;

		public void SetManager(RaycastManager raycastManager)
        {
			_raycastManager = raycastManager;
        }

		public abstract void OnRayEnter();

		public abstract void OnRayExit();
	}
}
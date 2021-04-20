using UnityEngine;

namespace DefaultNamespace
{
	public class ActiveEmojis : MonoBehaviour
	{
		[Header("Config")]
		[SerializeField] private GameObject go;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                go.SetActive(true);
            }
        }
    }
}
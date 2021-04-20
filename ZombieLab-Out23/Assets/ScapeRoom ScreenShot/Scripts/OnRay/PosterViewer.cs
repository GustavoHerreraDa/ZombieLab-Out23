using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{
    public class PosterViewer : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private GameObject viewer;
        [SerializeField] private RaycastManager raycastManager; 

        [SerializeField] private SpriteRenderer spriterenderer;
        private void Awake()
        {
            spriterenderer = GetComponent<SpriteRenderer>();
        }

        public void Show(Sprite poster)
        {
            spriterenderer.sprite = poster;
            viewer.SetActive(true);
        }
    }
}
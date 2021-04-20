using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Poster : MonoBehaviour
    {
        [Header("Config")]
        public Sprite sprite;
        public int index;
        [SerializeField] private PosterTable posterTable;

        public void SetPoster()
        {
            posterTable.SetPoster(this);
            gameObject.SetActive(false);
        }

        internal void ShowOff()
        {
            gameObject.SetActive(true);
        }
    }
}
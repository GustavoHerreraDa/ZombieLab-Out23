using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PosterOnRay : OnRay
    {
        [Header("Debug")]
        public int index;

        private PosterViewer _posterViewerUI;
        private Poster _poster;
     
        private void Awake()
        {
            _posterViewerUI = FindObjectOfType<PosterViewer>();
            _poster = GetComponent<Poster>();
        }

        public override void OnRayEnter()
        {
            _posterViewerUI.Show(_poster.sprite);
            _poster.SetPoster();
            _raycastManager.isAlreadyEnter = false;
        }

        public override void OnRayExit()
        {
            _raycastManager.isAlreadyEnter = false;
        }
    }
}
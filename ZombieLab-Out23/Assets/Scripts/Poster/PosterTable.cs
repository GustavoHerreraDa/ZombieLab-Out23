using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
	public class PosterTable : MonoBehaviour
	{
		private Poster _currentPoster;

        public void SetPoster(Poster poster)
        {
            _currentPoster?.ShowOff();

            _currentPoster = poster;

            SendPosterToServer(poster.index);

            StartCoroutine(GetInputForExit());
        }

        private IEnumerator GetInputForExit()
        {
            while (!Input.GetKeyDown(KeyCode.Mouse1))
            {
                SendPosterToServer(-1);

                _currentPoster.ShowOff();
                _currentPoster = null;

                yield return null;
            }
        }

        private void SendPosterToServer(int index)
        {
            ISFSObject sfso = new SFSObject();

            sfso.PutText("username", SmartFoxConnection.SFS.MySelf.Name);
            sfso.PutInt("poster", index);

            SmartFoxConnection.SFS.Send(new ExtensionRequest("trigger", sfso));
        }
    }
}
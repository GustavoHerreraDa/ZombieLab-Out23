using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

namespace SpriteViewer.Emojis
{
    public class LocalSpriteViewer : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Show(Sprite sprite, int index)
        {
            spriteRenderer.sprite = sprite;
            SendViewerToServer(index);
        }

        private void SendViewerToServer(int index)
        {
            print("Envio");
            ISFSObject sfso = new SFSObject();

            sfso.PutText("username", SmartFoxConnection.SFS.MySelf.Name);
            sfso.PutInt("emoji", index);

            SmartFoxConnection.SFS.Send( new ExtensionRequest("trigger", sfso));
        }
    }
}
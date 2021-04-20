using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

namespace DefaultNamespace
{
    public class ButtonScreenshotOnRay : OnRay
    {
        [Header("Config")]
        [SerializeField] private Animator buttonAnim;
        [SerializeField] private ScreenshotFecade screenshotFecade;

        [Header("Debug")]
        [SerializeField] private bool isPressed;

        public override void OnRayEnter()
        {
            if (isPressed) return;

            isPressed = false; //??? LALO LUCAS
            ISFSObject sfso = new SFSObject();
            sfso.PutNull("screenshot");
            SmartFoxConnection.SFS.Send(new ExtensionRequest("trigger", sfso));
            AnimationButton();
            screenshotFecade.TakeScreenshot();
            _raycastManager.isAlreadyEnter = false;
        }

        private void AnimationButton()
        {
            buttonAnim.SetTrigger("Pressed");
        }

        public override void OnRayExit()
        {
            _raycastManager.isAlreadyEnter = false;
        }

        public void RemoteScreenshot()
        {
            AnimationButton();
            screenshotFecade.TakeScreenshot();
        }
    }
}
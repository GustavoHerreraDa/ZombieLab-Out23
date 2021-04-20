using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RaycastManager : MonoBehaviour
    {
        [Header("Ray Config")]
        public Camera cam;
        public LayerMask layerMask;
        public float maxDistance = 1000;

        [Header("UI Config")]
        public GameObject infoText;  

        private Ray _ray;
        [SerializeField] public bool isAlreadyEnter;
        [SerializeField] private bool isPressed;



        private void Start()
        {
            if (null == cam)
            {
                cam = Camera.main;
            }
        }

        private void Update()
        {
            if(!isAlreadyEnter)
            {
                PressMouse();
                CalculateRay();
            }
        }

        public void PressMouse()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) isPressed = true;
            else isPressed = false;
        }

        private void CalculateRay()
        {
            _ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(_ray, out RaycastHit hit, maxDistance, layerMask))
            {
                OnRayEnter(hit.collider.gameObject);
            }
            else
            {
                infoText.SetActive(false);
            }
        }

        private float DistanceTo(Vector3 hittedPos)
        {
            return Vector3.Distance(transform.position, hittedPos);
        }

        private void OnRayEnter(GameObject hittedObject)
        {
            OnRay onRay = hittedObject.GetComponent<OnRay>();
            
            if (null == onRay) throw new System.Exception($"El gameObject {hittedObject.name} no tiene el script OnRay");

            onRay.SetManager(this);

            SetInfoText(onRay.infoText);
            infoText.SetActive(true);

            if (DistanceTo(hittedObject.transform.position) <= onRay.distToHit && isPressed)
            {
                isAlreadyEnter = true;
                infoText.SetActive(false);
                onRay.OnRayEnter();
            }
        }

        private void SetInfoText(string info)
        {
            Text text = infoText.GetComponent<Text>();
            if(null != text)
            {
                text.text = info;
            }
            else
            {
                infoText.GetComponent<TextMeshProUGUI>().text = info;
            }
        }
    }
}
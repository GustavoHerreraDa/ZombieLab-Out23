using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    [SerializeField]private Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }
    public void SetCaM(Transform cam)
    {
        print("ChangeCam");
        this.cam = cam;
    }

    void Update()
    {
        transform.LookAt(cam);
    }
}

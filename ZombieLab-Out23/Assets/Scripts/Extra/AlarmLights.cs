using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLights : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firstLight;
    public Transform to;
    public float timeCount;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, 5);
    }
}

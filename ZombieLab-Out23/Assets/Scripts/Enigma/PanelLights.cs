using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLights : MonoBehaviour
{
    public baul[] bauls;
    public Light[] lights;

    public GameObject door_Light;

    public int currentOnLights;

    [SerializeField]
    private Material light_off;

    [SerializeField]
    private Material light_One;

    [SerializeField]
    private Material light_Two;

    [SerializeField]
    private Material light_Thre;

    [SerializeField]
    private Material light_Four;



    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < lights.Length; x++)
        {
            lights[x].gameObject.SetActive(false);//esconde todos
        }
    }

    public void EnableLight()
    {
        currentOnLights++;
        switch (currentOnLights)
        {
            case 1:
                door_Light.GetComponent<Renderer>().sharedMaterial = light_One;
                break;
            case 2:
                door_Light.GetComponent<Renderer>().sharedMaterial = light_Two;
                break;
            case 3:
                door_Light.GetComponent<Renderer>().sharedMaterial = light_Thre;
                break;
            case 4:
                door_Light.GetComponent<Renderer>().sharedMaterial = light_Four;
                var animator = GetComponent<Animator>();
                animator.SetBool("IsOpen", true);
                break;
            default:
                door_Light.GetComponent<Renderer>().sharedMaterial = light_off;
                break;
        }
    }
}

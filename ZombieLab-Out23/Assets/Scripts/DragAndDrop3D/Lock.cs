using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public int idEnigm;
    public string LockName;
    public GameObject ObjectToOpen;
    public PanelLights LightControl;

    private Animator animator;
    public GameObject boleto;
    public GameObject key;

    public void CheckAnswerd(string key)
    {
        Debug.Log("Check Answerd");
        if (key == LockName)
        {
            EnigmaManager.Instance.CompleteEnigm(idEnigm);

            if (ObjectToOpen != null)
                animator = ObjectToOpen.GetComponent<Animator>();

            if (ObjectToOpen != null && animator != null)
                ObjectToOpen.GetComponent<Animator>().SetTrigger("Open");

            if (LightControl != null)
            {
                LightControl.EnableLight();
            }

            Debug.Log("Check Answerd Is Correct");

            Destroy(this.gameObject, 3f);
        }
    }

    public void AutoComplete()
    {
        if (ObjectToOpen != null)
            animator = ObjectToOpen.GetComponent<Animator>();

        if (ObjectToOpen != null && animator != null)
            ObjectToOpen.GetComponent<Animator>().SetTrigger("Open");

        if (LightControl != null)
        {
            LightControl.EnableLight();
        }

        Debug.Log("Check Answerd Is Correct");
        if (key != null)
            Destroy(key);
        if (boleto != null)
            Destroy(boleto);
        Destroy(this.gameObject, 3f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Exit_Zone());
        }
    }
    IEnumerator Exit_Zone()
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Certificado");
    }
}

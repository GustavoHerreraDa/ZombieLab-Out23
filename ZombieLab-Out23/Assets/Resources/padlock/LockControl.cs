using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    public int idEnigm;
    public enigmasCaurentna enigmaManag; //referencia al manager de engimas
    private int[] result, correctCombination;
    private bool isOpened;

    public int[] correctaCombinacion;
    private void Start()
    {
        result = new int[]{0,0,0,0};
        //correctCombination = new int[] {1,1,0,1};
        correctCombination = correctaCombinacion;
        isOpened = false;
        Rotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "WheelOne":
                result[0] = number;
                break;

            case "WheelTwo":
                result[1] = number;
                break;

            case "WheelThree":
                result[2] = number;
                break;

            case "WheelFour":
                result[3] = number;
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1]
            && result[2] == correctCombination[2] && result[3] == correctCombination[3] && !isOpened)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            isOpened = true;
            enigmaManag.checaResultado(true);//acepta el resultado y lo manda a enigma manager
            if (idEnigm == 7)
                EnigmaManager.Instance.CompleteEnigm(7);
        }
        /*else
        {
            enigmaManag.checaResultado(false);//acepta el resultado y lo manda a enigma manager
        }*/
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}

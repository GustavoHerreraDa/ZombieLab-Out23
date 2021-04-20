using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl_3dig : MonoBehaviour
{
    public int idEnigm;
    public enigmasCaurentna enigmaManag; //referencia al manager de engimas
    public int[] result, correctCombination;
    private bool isOpened;
    
    private void Start()
    {
        result = new int[]{0,0,0};
        correctCombination = new int[] {5,0,4};
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

        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1]
            && result[2] == correctCombination[2] && !isOpened)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            isOpened = true;
            enigmaManag.checaResultado(true);//acepta el resultado y lo manda a enigma manager
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}

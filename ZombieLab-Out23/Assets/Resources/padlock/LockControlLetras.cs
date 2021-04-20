using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControlLetras : MonoBehaviour
{
    public enigmasCaurentna enigmaManag; //referencia al manager de engimas
    private int[] result, correctCombination;
    private bool isOpened;
    private void Start()
    {
        result = new int[]{0,0,0,0,0};
        correctCombination = new int[] {3,8,5,9,2};
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
            case "WheelFive":
                result[4] = number;
                break;
        }
        
        //Debug.Log(result[3]);

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1]
            && result[2] == correctCombination[2] && result[3] == correctCombination[3] 
            && result[4] == correctCombination[4] && !isOpened)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            isOpened = true;
            //enigmaManag.checaResultadoEnigma2y4(true);//acepta el resultado y lo manda a enigma manager
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}

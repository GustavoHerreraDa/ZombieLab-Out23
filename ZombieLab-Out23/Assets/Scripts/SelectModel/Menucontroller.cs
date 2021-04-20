using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menucontroller : MonoBehaviour
{
	int whichCharacter;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	 if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (PlayerInfo.PI != null){
            	PlayerInfo.PI.mySelectedCharacter=whichCharacter;
            	PlayerPrefs.SetInt("Mycharacter",whichCharacter);
            }
        }
 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (PlayerInfo.PI != null){
            	PlayerInfo.PI.mySelectedCharacter=whichCharacter;
            	PlayerPrefs.SetInt("Mycharacter",whichCharacter);
            }
        }     	 
    }
    public void OnclickcharacterPick (int whichCharacter){
    	if (PlayerInfo.PI != null){
            	PlayerInfo.PI.mySelectedCharacter=whichCharacter;
            	PlayerPrefs.SetInt("Mycharacter",whichCharacter);
            }
    }
}

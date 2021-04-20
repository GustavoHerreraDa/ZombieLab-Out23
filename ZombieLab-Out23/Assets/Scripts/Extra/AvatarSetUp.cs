using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarSetUp : MonoBehaviour
{
    //public GameObject myCharacter;
    public int characterValue;
    private string minombre;
    public TMP_Text MyText;
    public GameObject mujer01;
    public GameObject mujer02;
    public GameObject hombre01;
    public GameObject hombre02;
    public GameObject MujerT;

    // Start is called before the first frame update
    void Start()
    {
        characterValue = PlayerPrefs.GetInt("Mycharacter");//PlayerInfo.PI.mySelectedCharacter;
        minombre = PlayerPrefs.GetString("Mynombre");
       	//myCharacter = Instantiate(PlayerInfo.PI.allCharacters[characterValue], transform.position, transform.rotation, transform);

       	MyText.text = minombre;

        //Animator animator = myCharacter.AddComponent<Animator>(); 
  		//animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Asets/Modelo/Mujer02/Mujer02.animator");

  		

  		if (characterValue==2){
  			mujer01.SetActive(true);
  		}else if (characterValue==3){
  			mujer02.SetActive(true);
  		}else if (characterValue==0){
  			hombre01.SetActive(true);
  		}else{
  			hombre02.SetActive(true);
  		}
    }
}
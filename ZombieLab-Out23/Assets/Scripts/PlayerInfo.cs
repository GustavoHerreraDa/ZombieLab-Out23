using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;

    public int mySelectedCharacter;
    public GameObject[] allCharacters;
    public GameObject ruleta;
    float rotgradosY = 0f ;

    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if (PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    	rotgradosY= ruleta.transform.rotation.eulerAngles.y;   

        if (rotgradosY==40.19996)
        {
            mySelectedCharacter = 0;
        }
        else if (rotgradosY==310.2)
        {
            mySelectedCharacter = 1;
        }
        else if (rotgradosY==220.2)
        {
            mySelectedCharacter = 2;
        }
        else if (rotgradosY==130.2)
        {
            mySelectedCharacter = 3;
        }
    }
}

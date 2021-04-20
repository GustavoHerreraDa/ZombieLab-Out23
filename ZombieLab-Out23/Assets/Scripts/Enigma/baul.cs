using UnityEngine;

public class baul : MonoBehaviour
{
    public enigmasCaurentna enigMa;
    public levelMan levelMan;
    public int roomNumber;
    //public bool isComplete;
    public int enigmaNumber;
    public PanelLights panelLights;
    //When the Primitive collides with the walls, it will reverse direction

    public void Awake()
    {
        levelMan = FindObjectOfType<levelMan>();
    }

    public void Update()
    {
        //var active = levelMan.roomNumber == roomNumber;
        //gameObject.SetActive(active);
    }

    private void OnTriggerEnter(Collider other) //Fer aca se activa el enigma
    {
        //        Debug.Log("colision con el baul!");
        if (other.tag == "Player")
        {
            var player = other.GetComponent<levelMan>();
            //      Debug.Log("el jugador choco con el baul");
            //enigMa.showMe();
            //que se mueca 3 unidades hacia atras
            //other.gameObject.transform.position += Vector3.back * 1.5f;
            enigMa.callMeEnigma(enigmaNumber);//FER

        }
    }
}
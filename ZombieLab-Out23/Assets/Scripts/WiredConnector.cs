using UnityEngine;
using System;

public class WiredConnector : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Camera cam;
    [SerializeField] private WireGamemanager wireGamemanager;

    private Vector2 mousePosition;
    private Vector3 wireConnectorPosition;
    private bool dragging;
    private bool onConnector;
    private bool isWiredConnected;
    private Vector3 connectorPosition;

    private float deltaX;
    private float deltaY;


    public bool inCorrectPosition;
    private Connector connector;
    public static event Action<Connector,bool> OnConnection;

    private void Start()
    {
        dragging = false;
        inCorrectPosition = false;
        wireConnectorPosition = transform.localPosition;
    }
    private void Update()
    {
        if(dragging)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = new Vector3(mousePosition.x - deltaX, mousePosition.y - deltaY, transform.position.z);
            transform.position = pos;
        }
    }
    private void OnMouseOver()
    {
        if (!inCorrectPosition)
            CheckInput();                   
    }
    
    private void CheckInput()
    {
       

        if (Input.GetMouseButtonDown(0))
        {
            SetDelta();   
            dragging = true;
        }

        if (Input.GetMouseButtonDown(0) && isWiredConnected)
         {
            SetDelta();
            dragging = true;
            isWiredConnected = false;
            OnConnection?.Invoke(connector, false);
         }

         if (Input.GetMouseButtonDown(1) && onConnector && !isWiredConnected)
         {
            if (!connector.alreadyConnected)                
                    Connect(connector);              
         }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        onConnector = true;
        connector = other.GetComponent<Connector>();       
        connectorPosition = other.gameObject.transform.position;         
    }

    private void Connect(Connector connector)
    {
        isWiredConnected = true;
        OnConnection?.Invoke(connector, true);
        dragging = false;
        wireConnectorPosition = new Vector3(connectorPosition.x, connectorPosition.y, transform.position.z);
        transform.position = wireConnectorPosition;

        if (connector.value == value)
        {
            inCorrectPosition = true;
            wireGamemanager.CheckWiresPositions();
        }
        else
        {
            inCorrectPosition = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {        
            onConnector = false;       
    }

    private void SetDelta()
    {
        deltaX = cam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = cam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }
}

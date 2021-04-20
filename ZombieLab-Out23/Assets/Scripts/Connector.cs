using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public int value;
    public bool alreadyConnected;

    private void OnEnable()
    {
        WiredConnector.OnConnection += SetConnectionStatus;
    }
    private void OnDisable()
    {
        WiredConnector.OnConnection -= SetConnectionStatus;
    }

    private void Start()
    {
        alreadyConnected = false;
    }

    private void SetConnectionStatus(Connector connector, bool status)
    {
        if (connector == this)
            alreadyConnected = status;
    }
}

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Utils;
using TMPro;
using System;
using System.Collections.Generic;
using Sfs2X.Entities.Variables;
using Sfs2X.Entities.Data;

public class SFSManager : Singleton<SFSManager>
{
    #region Var

    #region Public
    public GameObject loginMenu;
    public SelectAvatar selectAvatar;
    public TMP_InputField userInput;
    public TMP_InputField codeInput;
    public GameObject screenCode;
    #endregion

    #region Private
    [Tooltip("SmartFoxServer Config")]
    private string Host = "192.168.0.1";
    private const int TcpPort = 9933;
    private const int WSPort = 8080;
    private const string Zone = "OUT23";

    private bool onLogin;

    private Room room;
    public Room Room { get => room; set => room = value; }
    #endregion

    #endregion

    #region UnityMethods
    void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(GetHost());
    }

    private void OnDestroy()
    {
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.CONNECTION, OnConnection);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.LOGIN, OnLogin);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.LOGOUT, OnLogout);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);

        SmartFoxConnection.SFS.Disconnect();
    }

    

    void Update()
    {
        if (SmartFoxConnection.SFS != null)
        {
            SmartFoxConnection.SFS.ProcessEvents();
        }
    }

    #endregion

    #region Connection

    private void InitConection()
    {
        ConfigData cfg = new ConfigData();
        cfg.Host = Host;
#if !UNITY_WEBGL
        cfg.Port = TcpPort;
#else
		cfg.Port = WSPort;
#endif
        cfg.Zone = Zone;

#if !UNITY_WEBGL
        SmartFoxConnection.SFS = new SmartFox();
#else
		SmartFox sfs = new SmartFox(UseWebSocket.WS_BIN);
#endif

        SmartFoxConnection.SFS.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.LOGIN, OnLogin);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.LOGOUT, OnLogout);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);

        //SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_VARIABLES_UPDATE, OnRoomVariablesUpdate);

        SmartFoxConnection.SFS.Connect(cfg);
        Debug.Log("Connecting to " + Host + "...");
    }

  
    private void OnConnection(BaseEvent evt)
    {
        print("OnConnection");

        if ((bool)evt.Params[CONST.CMD_SUCCESS])
        {
            print("ConnectionSuccess");
            loginMenu.SetActive(true);
        }
        else
        {
            SmartFoxConnection.SFS.Disconnect();
        }
    }

    private void OnConnectionLost(BaseEvent evt)
    {
        print("OnConnectionLost");

        string reason = (string)evt.Params["reason"];

        if (reason != ClientDisconnectionReason.MANUAL)
        {
            Debug.LogError("Connection was lost; reason is: " + reason);
        }
        else
        {
            Debug.LogWarning("User Disconnected");
        }

        SmartFoxConnection.SFS.RemoveAllEventListeners();
    }

    public void OnClickLogin()
    {
        Debug.Log("Init Login");
        SmartFoxConnection.SFS.Send(new LoginRequest(userInput.text));
    }

    private void OnLoginError(BaseEvent evt)
    {
        Debug.Log("Login failed: " + (string)evt.Params["errorMessage"]);
    }

    private void OnLogin(BaseEvent evt)
    {
        SmartFoxConnection.SFS.Send(new JoinRoomRequest(codeInput.text.ToUpper()));
    }

    private void OnLogout(BaseEvent evt)
    {
        Debug.Log("Login out");
    }

    private void OnJoinRoom(BaseEvent evt)
    {
        Debug.Log("Join Room");
        
        SmartFoxConnection.Room = (Room) evt.Params["room"];

        SmartFoxConnection.SFS.Send(new ExtensionRequest("userJoin",new SFSObject()));

        screenCode.SetActive(false) ;
    }

    private void OnJoinRoomError(BaseEvent evt)
    {
        SmartFoxConnection.SFS.Send(new LogoutRequest());
        Debug.Log("Login failed: " + (string)evt.Params["errorMessage"]);
    }

    private void OnUserEnterRoom(BaseEvent evt)
    {
        Debug.Log("OnUserEnterRoom");
    }

    public void InitGame()
    {
        List<UserVariable> userVariables = new List<UserVariable>();
        print(selectAvatar.mySelectedCharacter + " Selected");
        userVariables.Add(new SFSUserVariable("model", selectAvatar.mySelectedCharacter, 2));

        SmartFoxConnection.SFS.Send(new SetUserVariablesRequest(userVariables));
        selectAvatar.OnInicioClicked();
    }

    //private void OnRoomVariablesUpdate(BaseEvent evt)
    //{
    //    List<string> changedVars = (List<string>)evt.Params["changedVars"];

    //    foreach (string item in changedVars)
    //    {
    //        print("VAR: " + item + " VALUE: " + room.GetVariable(item).Value);
    //    }
    //}
    #endregion

    #region Utils
    private IEnumerator GetHost()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get("https://drive.google.com/uc?export=download&id=1JeURBLe1Z8Oo7A_nZVSDnY-wUqbvrqri");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            Host = webRequest.downloadHandler.text.Split('\"')[3];
            InitConection();
        }
    }
    #endregion
}

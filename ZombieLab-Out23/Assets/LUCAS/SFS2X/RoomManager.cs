using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class RoomManager : Singleton<RoomManager>
{
    #region VAR
    public GameObject RoomExtPrefab;
    #endregion

    private GameObject roomExtension;
    private Room roomMatch;

    public Room RoomMatch { get => roomMatch;}

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_CREATION_ERROR, OnRoomCreationError);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_ADD, OnRoomCreated);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_REMOVE, OnRoomRemove);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
    }

    private void OnRoomCreationError(BaseEvent evt)
    {
        Debug.LogError("OnRoomCreationError: " + (string)evt.Params["errorMessage"]);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy RoomManager");
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.ROOM_CREATION_ERROR, OnRoomCreationError);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.ROOM_ADD, OnRoomCreated);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.ROOM_REMOVE, OnRoomRemove);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);

    }

    private void OnRoomCreated(BaseEvent evt)
    {
        Debug.Log("OnRoomCreated");
    }

    private void OnJoinRoom(BaseEvent evt)
    {
        Debug.Log("OnJoinRoom");
        //Room room = (Room)evt.Params[CONST.CMD_ROOM];
    }

    private void OnUserEnterRoom(BaseEvent evt)
    {
        Debug.Log("OnUserEnterRoom");
        //Room room = (Room)evt.Params[CONST.CMD_ROOM];
        //if (room.Name == SmartFoxConnection.SFS.MySelf.Name)
        //{
        //    if (room.Capacity == room.UserCount)
        //    {
        //        AvailableListManager.Instance.SetPlayButtons();
        //    }
        //}

        //if (room.Name == CONST.ROOM_LOBBY)
        //{
        //    AvailableListManager.Instance.RefreshList();
        //}
    }

    private void OnRoomRemove(BaseEvent evt)
    {
        Debug.Log("OnRoomRemove");
        if (roomExtension != null && ((Room)evt.Params[CONST.CMD_ROOM]).GroupId == CONST.CMD_MATCHS_GROUP)
            Destroy(roomExtension);
    }

    private void OnUserExitRoom(BaseEvent evt)
    {
        if (((Room)evt.Params[CONST.CMD_ROOM]).GroupId != CONST.CMD_MATCHS_GROUP)
            return;

        SmartFoxConnection.SFS.Send(new JoinRoomRequest(CONST.ROOM_LOBBY));
        SceneManager.LoadScene(CONST.SCENE_MENU);
    }

    public void OnConnectionLost(BaseEvent evt)
    {
        SmartFoxConnection.SFS.RemoveAllEventListeners();
        Debug.LogError("Connection Lost");
    }


    public void CreateRoomMatch(string opponentName)
    {
        Debug.Log("CreateRoom");
        RoomSettings settings = new RoomSettings(SmartFoxConnection.SFS.MySelf.Name + "_VS_" + opponentName);
        settings.MaxUsers = 2;
        settings.GroupId = CONST.CMD_MATCHS_GROUP;
        settings.IsGame = true;
        settings.Name = SmartFoxConnection.SFS.MySelf.Name +"_VS_" + opponentName;
        settings.Extension = new Sfs2X.Requests.RoomExtension("ext", "com.tglgames.roomext.RoomExt");
        settings.AllowOwnerOnlyInvitation = true;

        SmartFoxConnection.SFS.Send(new CreateRoomRequest(settings));
    }

    public void CancelMatch()
    {
        Debug.Log("Cancel Match");
        SmartFoxConnection.SFS.Send(new LeaveRoomRequest());
        SmartFoxConnection.SFS.Send(new JoinRoomRequest(CONST.ROOM_LOBBY));
    }
    public void JoinMatch()
    {
        Debug.Log("Join Match");
    }

}
using Sfs2X.Core;
using Sfs2X.Entities;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Entities.Variables;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Utils;
using System;
using TMPro;
using SpriteViewer;
using DefaultNamespace;

public class Spawner : Singleton<Spawner>
{
    public GameObject remotePlayer;
    public SecuenciaEnigma secuencia;
    public Transform parentEnigmaVacuna;
    public ButtonScreenshotOnRay screenshotBtn;
    public List<Transform> vacunas;
    public List<Transform> contenedores;

    Dictionary<string, SimpleRemoteInterpolation> remoteInterpolations = new Dictionary<string, SimpleRemoteInterpolation>();

    //private GameObject _instanciedRemote;

    void Start()
    {
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserUpdateVariables);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
    }
    private void OnExtensionResponse(BaseEvent e)
    {
        print((string)e.Params[CONST.CMD]);

        ISFSObject sfso = (SFSObject)e.Params[CONST.CMD_PARAMS];

        if ((string)e.Params[CONST.CMD] == "trigger")
        {
            if (sfso.ContainsKey("idTransformer"))
            {
                Vector3 pos = new Vector3(sfso.GetFloat("x"), sfso.GetFloat("y"), sfso.GetFloat("z"));
                Quaternion rot = new Quaternion(sfso.GetFloat("rx"), sfso.GetFloat("ry"), sfso.GetFloat("rz"), sfso.GetFloat("rw"));

                SetPosVacuna(sfso.GetInt("idTransformer"), sfso.GetInt("idCont"), pos, rot);
            }
            else if (sfso.ContainsKey("emoji"))
            {
                remoteInterpolations[sfso.GetText("username")].GetComponentInChildren<RemoteSpriteViewerEmoji>().Show(sfso.GetInt("emoji"));
            }
            else if (sfso.ContainsKey("poster"))
            {
                remoteInterpolations[sfso.GetText("username")].GetComponentInChildren<RemoteSpriteViewerPoster>().Show(sfso.GetInt("poster") - 1);
            }
            else if (sfso.ContainsKey("tele"))
            {
                secuencia.ParcialComplete();
            }
            else if (sfso.ContainsKey("screenshot"))
            {
                screenshotBtn.RemoteScreenshot();
            }
        }

    }

    void SpawnRemotePlayer(User user)
    {
        if (!remoteInterpolations.ContainsKey(user.Name))
        {
            remoteInterpolations.Add(user.Name, Instantiate(remotePlayer, new Vector3(-1.56f, 1.77f, 10.47f), new Quaternion()).AddComponent<SimpleRemoteInterpolation>());
            remoteInterpolations[user.Name].modelType = user.GetVariable("model").GetIntValue();
            remoteInterpolations[user.Name].GetComponentInChildren<TextMeshPro>().text = user.Name;
        }

        if (remoteInterpolations[user.Name].modelType == 0)
        {
            Destroy(remoteInterpolations[user.Name].transform.GetChild(1).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(2).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(3).gameObject);
        }
        if (remoteInterpolations[user.Name].modelType == 1)
        {
            Destroy(remoteInterpolations[user.Name].transform.GetChild(0).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(2).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(3).gameObject);
        }
        if (remoteInterpolations[user.Name].modelType == 2)
        {
            Destroy(remoteInterpolations[user.Name].transform.GetChild(1).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(0).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(3).gameObject);
        }
        if (remoteInterpolations[user.Name].modelType == 3)
        {
            Destroy(remoteInterpolations[user.Name].transform.GetChild(1).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(2).gameObject);
            Destroy(remoteInterpolations[user.Name].transform.GetChild(0).gameObject);
        }

        print("Player count > " + remoteInterpolations.Count);
    }

    private void OnDestroy()
    {
        SmartFoxConnection.SFS.RemoveEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserUpdateVariables);
    }

    Vector3 pos = new Vector3();
    private void OnUserUpdateVariables(BaseEvent evt)
    {
        
        List<string> changedVars = (List<string>)evt.Params["changedVars"];
        User eUser = (User)evt.Params["user"];

        if (!eUser.IsItMe && !eUser.IsSpectator)
        {
            if (!remoteInterpolations.ContainsKey(eUser.Name))
            {
                SpawnRemotePlayer(eUser);
                return;
            }

            if (changedVars.Contains("pos"))
            {
                pos = new Vector3(eUser.GetVariable("pos").GetSFSArrayValue().GetFloat(0), 1.77f, eUser.GetVariable("pos").GetSFSArrayValue().GetFloat(1));
                remoteInterpolations[eUser.Name].SetPosition(pos, true);
            }

            if (changedVars.Contains("rot"))
            {
                remoteInterpolations[eUser.Name].SetRotation((float)eUser.GetVariable("rot").GetDoubleValue());
            }

            if (changedVars.Contains("animJump"))
            {
                remoteInterpolations[eUser.Name].JumpAnim(eUser.GetVariable("animJump").GetStringValue());
            }

            if (changedVars.Contains("animWalk"))
            {
                remoteInterpolations[eUser.Name].WalkAnim(eUser.GetVariable("animWalk").GetSFSArrayValue().GetText(0),
                    eUser.GetVariable("animWalk").GetSFSArrayValue().GetFloat(1),
                    eUser.GetVariable("animWalk").GetSFSArrayValue().GetText(2),
                    eUser.GetVariable("animWalk").GetSFSArrayValue().GetFloat(3));
            }

        }
    }

    public void SendPos(Transform player)
    {
        SFSArray positionArray = new SFSArray();

        positionArray.AddFloat(player.position.x);
        positionArray.AddFloat(player.position.z);

        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("pos", positionArray, 6));

        SmartFoxConnection.SFS.Send(new SetUserVariablesRequest(userVariables));
    }
    public void SendRot(float rotY)
    {
        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("rot", (double)rotY, 3));

        SmartFoxConnection.SFS.Send(new SetUserVariablesRequest(userVariables));
    }

    public void SendAnimWalk(string xName, float xFloat, string yName, float yFloat)
    {
        SFSArray AnimArray = new SFSArray();

        AnimArray.AddText(xName);
        AnimArray.AddFloat(xFloat);
        AnimArray.AddText(yName);
        AnimArray.AddFloat(yFloat);

        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("animWalk", AnimArray, 6));

        SmartFoxConnection.SFS.Send(new SetUserVariablesRequest(userVariables));
    }

    public void SendAnimJump(string jumpName)
    {
        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("animJump", jumpName, 4));

        SmartFoxConnection.SFS.Send(new SetUserVariablesRequest(userVariables));
    }
    private void OnUserEnterRoom(BaseEvent evt)
    {
        
    }

    private void OnUserExitRoom(BaseEvent evt)
    {
        User user = (User)evt.Params["user"];
        GameObject temp = remoteInterpolations[user.Name].gameObject;
        remoteInterpolations.Remove(user.Name);
        Destroy(temp);

    }

    public void SendPositionObject(Transform obj, Transform objCont)
    {
        ISFSObject sfso = new SFSObject();
        sfso.PutInt("idTransformer", vacunas.FindInstanceID(obj));
        sfso.PutInt("idCont", contenedores.FindInstanceID(objCont));
        sfso.PutFloat("x", obj.position.x);
        sfso.PutFloat("y", obj.position.y);
        sfso.PutFloat("z", obj.position.z);
        sfso.PutFloat("rx", obj.rotation.x);
        sfso.PutFloat("ry", obj.rotation.y);
        sfso.PutFloat("rz", obj.rotation.z);
        sfso.PutFloat("rw", obj.rotation.w);
        SmartFoxConnection.SFS.Send(new ExtensionRequest("trigger",sfso));
    }

    void SetPosVacuna(int id, int idCont, Vector3 pos, Quaternion rot)
    {
        print(id);
        vacunas[id].position = pos;
        vacunas[id].rotation = rot;
        vacunas[id].SetParent(contenedores[idCont]);
    }
}

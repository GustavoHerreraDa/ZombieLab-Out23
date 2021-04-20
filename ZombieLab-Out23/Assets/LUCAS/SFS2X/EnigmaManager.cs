using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sfs2X.Requests;
using Sfs2X.Core;
using System;
using Sfs2X.Entities.Variables;
using Sfs2X.Entities.Data;
using Utils;

public class EnigmaManager : Singleton<EnigmaManager>
{
    //public List<bool> EnigmaState = new List<bool>();
    
    public bool[] EnigmBools = new bool[11];

    public UnityEvent[] unityEvent;

    List<RoomVariable> roomVariables;

    private void Start()
    {
        SmartFoxConnection.SFS.AddEventListener(SFSEvent.ROOM_VARIABLES_UPDATE, ROOM_VARIABLES_UPDATE);

        roomVariables = SmartFoxConnection.Room.GetVariables();

        //foreach (RoomVariable item in roomVariables)
        //{
        //    print("VAR: " + item.Name + " VALUE: " + item.Value);
        //}

        SetEnimgState();
    }

    private void ROOM_VARIABLES_UPDATE(BaseEvent evt)
    {
        List<string> changedVars = (List<string>)evt.Params["changedVars"];
        roomVariables = SmartFoxConnection.Room.GetVariables();

        foreach (string item in changedVars)
        {
            print("_______VAR: " + item + " VALUE: " + SmartFoxConnection.Room.GetVariable(item).Value);
            
            int id = int.Parse(item.Replace("enigm", ""));
            if (item.Contains("enigm"))
            {
                if (!EnigmBools[id])
                    unityEvent[id].Invoke();
            }
        }

        EnigmBools[0] = roomVariables[0].GetBoolValue();
        EnigmBools[1] = roomVariables[1].GetBoolValue();
        EnigmBools[2] = roomVariables[2].GetBoolValue();
        EnigmBools[3] = roomVariables[3].GetBoolValue();
        EnigmBools[4] = roomVariables[4].GetBoolValue();
        EnigmBools[5] = roomVariables[5].GetBoolValue();
        EnigmBools[10] = roomVariables[6].GetBoolValue();
        EnigmBools[6] = roomVariables[7].GetBoolValue();
        EnigmBools[7] = roomVariables[8].GetBoolValue();
        EnigmBools[8] = roomVariables[9].GetBoolValue();
        EnigmBools[9] = roomVariables[10].GetBoolValue();
    }

    void SetEnimgState() 
    {
        EnigmBools[0] = roomVariables[0].GetBoolValue();
        EnigmBools[1] = roomVariables[1].GetBoolValue();
        EnigmBools[2] = roomVariables[2].GetBoolValue();
        EnigmBools[3] = roomVariables[3].GetBoolValue();
        EnigmBools[4] = roomVariables[4].GetBoolValue();
        EnigmBools[5] = roomVariables[5].GetBoolValue();
        EnigmBools[10] = roomVariables[6].GetBoolValue();
        EnigmBools[6] = roomVariables[7].GetBoolValue();
        EnigmBools[7] = roomVariables[8].GetBoolValue();
        EnigmBools[8] = roomVariables[9].GetBoolValue();
        EnigmBools[9] = roomVariables[10].GetBoolValue();

        //EnigmaState.Clear();

        for (int i = 0; i < 11; i++)
        {
            if(EnigmBools[i])
                unityEvent[i].Invoke();
            //if (i != 6)
            //{
            //    EnigmaState.Add(roomVariables[i].GetBoolValue());
            //    if (roomVariables[i].GetBoolValue())
            //        unityEvent[i].Invoke();
            //}
        }
    }

    public void CompleteEnigm(int idEnigm)
    {
        if (EnigmBools[idEnigm])
            return;

        EnigmBools[idEnigm] = true;

        SFSObject sfso = new SFSObject();
        sfso.PutText("name","enigm" + idEnigm);
        sfso.PutInt("type", 1);
        sfso.PutBool("value", true);

        SmartFoxConnection.SFS.Send(new ExtensionRequest("setRoomVariable", sfso));

        print("CompleteEnigm >>> " + idEnigm);

    }
}

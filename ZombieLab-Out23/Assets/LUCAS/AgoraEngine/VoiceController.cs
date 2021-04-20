using UnityEngine;
using Utils;
using UnityEngine.UI;
#if (UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif
using agora_gaming_rtc;

public class VoiceController : Singleton<VoiceController>
{
    private IRtcEngine mRtcEngine = null;
    private string TOKEN;
    private string AppID = "40e7cbdd05364342830887a0d440e658";

    bool isJoined = false;

    void Awake()
    {
        Application.targetFrameRate = 30;
        DontDestroyOnLoad(gameObject);
        //userID = SmartFoxConnection.SFS.MySelf.Name;
        //CheckAppId();
        GotoStart();
        //JoinChannel(SmartFoxConnection.Room.Name, (uint)SmartFoxConnection.SFS.MySelf.Id);
    }


    void GotoStart()
    {
#if (UNITY_2018_3_OR_NEWER)
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {

        }
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif


        mRtcEngine = IRtcEngine.GetEngine(AppID);

        mRtcEngine.OnJoinChannelSuccess += (string channelName, uint uid, int elapsed) =>
        {
            string joinSuccessMessage = string.Format("joinChannel callback uid: {0}, channel: {1}, version: {2}", uid, channelName, getSdkVersion());
            Debug.Log(joinSuccessMessage);
        };

        mRtcEngine.OnLeaveChannel += (RtcStats stats) =>
        {
            string leaveChannelMessage = string.Format("onLeaveChannel callback duration {0}, tx: {1}, rx: {2}, tx kbps: {3}, rx kbps: {4}", stats.duration, stats.txBytes, stats.rxBytes, stats.txKBitRate, stats.rxKBitRate);
            Debug.Log(leaveChannelMessage);
            
            // reset the mute button state
            //if (isMuted)
            //{
            //    MuteButtonTapped();
            //}
        };

        mRtcEngine.OnUserJoined += (uint uid, int elapsed) =>
        {
            string userJoinedMessage = string.Format("onUserJoined callback uid {0} {1}", uid, elapsed);
            Debug.Log(userJoinedMessage);
        };

        mRtcEngine.OnUserOffline += (uint uid, USER_OFFLINE_REASON reason) =>
        {
            string userOfflineMessage = string.Format("onUserOffline callback uid {0} {1}", uid, reason);
            Debug.Log(userOfflineMessage);
        };

        mRtcEngine.OnVolumeIndication += (AudioVolumeInfo[] speakers, int speakerNumber, int totalVolume) =>
        {
            //if (speakerNumber == 0 || speakers == null)
            //{
            //    Debug.Log(string.Format("onVolumeIndication only local {0}", totalVolume));
            //}

            for (int idx = 0; idx < speakerNumber; idx++)
            {
                string volumeIndicationMessage = string.Format("{0} onVolumeIndication {1} {2}", speakerNumber, speakers[idx].uid, speakers[idx].volume);
                Debug.Log(volumeIndicationMessage);
            }
        };

        mRtcEngine.OnUserMutedAudio += (uint uid, bool muted) =>
        {
            string userMutedMessage = string.Format("onUserMuted callback uid {0} {1}", uid, muted);
            Debug.Log(userMutedMessage);
        };

        mRtcEngine.OnWarning += (int warn, string msg) =>
        {
            string description = IRtcEngine.GetErrorDescription(warn);
            string warningMessage = string.Format("onWarning callback {0} {1} {2}", warn, msg, description);
            Debug.Log(warningMessage);
        };

        mRtcEngine.OnError += (int error, string msg) =>
        {
            string description = IRtcEngine.GetErrorDescription(error);
            string errorMessage = string.Format("onError callback {0} {1} {2}", error, msg, description);
            Debug.Log(errorMessage);
        };

        mRtcEngine.OnRtcStats += (RtcStats stats) =>
        {
            string rtcStatsMessage = string.Format("onRtcStats callback duration {0}, tx: {1}, rx: {2}, tx kbps: {3}, rx kbps: {4}, tx(a) kbps: {5}, rx(a) kbps: {6} users {7}",
                stats.duration, stats.txBytes, stats.rxBytes, stats.txKBitRate, stats.rxKBitRate, stats.txAudioKBitRate, stats.rxAudioKBitRate, stats.userCount);

            int lengthOfMixingFile = mRtcEngine.GetAudioMixingDuration();
            int currentTs = mRtcEngine.GetAudioMixingCurrentPosition();

            string mixingMessage = string.Format("Mixing File Meta {0}, {1}", lengthOfMixingFile, currentTs);
        };

        mRtcEngine.OnAudioRouteChanged += (AUDIO_ROUTE route) =>
        {
            string routeMessage = string.Format("onAudioRouteChanged {0}", route);
            Debug.Log(routeMessage);
        };

        mRtcEngine.OnRequestToken += () =>
        {
            string requestKeyMessage = string.Format("OnRequestToken");
            Debug.Log(requestKeyMessage);
        };

        mRtcEngine.OnConnectionInterrupted += () =>
        {
            string interruptedMessage = string.Format("OnConnectionInterrupted");
            Debug.Log(interruptedMessage);
        };

        mRtcEngine.OnConnectionLost += () =>
        {
            string lostMessage = string.Format("OnConnectionLost");
            Debug.Log(lostMessage);
        };

        mRtcEngine.SetLogFilter(LOG_FILTER.INFO);

        mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_COMMUNICATION);
    }

    //private void CheckAppId()
    //{
    //    Debug.Assert(AppID.Length > 10, "Please fill in your AppId first on Game Controller object.");
    //    GameObject go = GameObject.Find("AppIDText");
    //    if (go != null)
    //    {
    //        if (appIDText != null)
    //        {
    //            if (string.IsNullOrEmpty(AppID))
    //            {
    //                appIDText.text = "AppID: " + "UNDEFINED!";
    //                appIDText.color = Color.red;
    //            }
    //            else
    //            {
    //                appIDText.text = "AppID: " + AppID.Substring(0, 4) + "********" + AppID.Substring(AppID.Length - 4, 4);
    //            }
    //        }
    //    }
    //}

    public void ToggleConnection(Image btn)
    {
        if(!isJoined){

            Joint();
            btn.color = new Color(btn.color.r, btn.color.g, btn.color.b, 1f);
            isJoined = true;
        }
        else
        {
            LeaveChannel();
            btn.color = new Color(btn.color.r, btn.color.g, btn.color.b, .6f);
            isJoined = false;
        }
    }
    private void Joint()
    {
        JoinChannel(SmartFoxConnection.Room.Name, (uint)SmartFoxConnection.SFS.MySelf.Id);
    }
    private void JoinChannel(string channelName, uint uid)
    {
        Debug.Log(string.Format("Tap joinChannel with channel name {0}", channelName));

        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }
        if (mRtcEngine != null)
        {
            mRtcEngine.JoinChannel(channelName);
            mRtcEngine.EnableAudioVolumeIndication(500, 3, true);
        }
        else
        {
            Invoke(channelName, 1);
        }
        
    }
    private void LeaveChannel()
    {
        mRtcEngine.LeaveChannel();
        Debug.Log(string.Format("left channel name "));
    }

    //void OnApplicationQuit()
    //{
    //    if (mRtcEngine != null)
    //    {
    //        IRtcEngine.Destroy();
    //    }
    //}


    public string getSdkVersion()
    {
        string ver = IRtcEngine.GetSdkVersion();
        return ver;
    }


    bool isMuted = false;
    void MuteButtonTapped()
    {
        string labeltext = isMuted ? "Mute" : "Unmute";
        //if (label != null)
        //{
        //    label.text = labeltext;
        //}
        isMuted = !isMuted;
        mRtcEngine.EnableLocalAudio(!isMuted);
    }

    void OnApplicationQuit()
    {
        if (mRtcEngine != null)
        {
            mRtcEngine.LeaveChannel();
            IRtcEngine.Destroy();
        }
    }

    private void OnDestroy()
    {
        if (mRtcEngine != null)
            mRtcEngine.LeaveChannel();
    }

    private void OnDisable()
    {
        if (mRtcEngine != null)
            mRtcEngine.LeaveChannel();
    }

}

using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class audioWww : MonoBehaviour
{
    private float progress;
    private AudioClip audioClip1;
    public AudioSource audioSrc;
    public string url;
    
    void Start()
    {
    //    StartCoroutine(WWWload());
        StartCoroutine(Start1());
    }
    
    private IEnumerator Start1()
    {
        //string url = "piratas";
        string qJson = "sound/" + url+".ogg";
        string WavPath =  Path.Combine(Application.streamingAssetsPath, qJson);
        Debug.Log(WavPath);
        
        using (var webRequest = UnityWebRequestMultimedia.GetAudioClip(WavPath, AudioType.OGGVORBIS))
        //using (var webRequest = UnityWebRequestMultimedia.GetAudioClip(WavPath, AudioType.MPEG));
        {
            //((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
            ((DownloadHandlerAudioClip) webRequest.downloadHandler).streamAudio = false;
 
            webRequest.SendWebRequest();
            while (!webRequest.isNetworkError && webRequest.downloadedBytes < 1024)
                yield return null;
 
            if (webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.error);
                yield break;
            }
       
            var clip = ((DownloadHandlerAudioClip)webRequest.downloadHandler).audioClip;
            audioSrc.clip = clip;
            audioSrc.Play();
        }
    }

    private IEnumerator WWWload()
    {
        string url = "piratas";
        //string qJson = "sound/" + url+".ogg";
        //UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("File://"+Path.Combine(Application.streamingAssetsPath, qJson), AudioType.OGGVORBIS);
        string qJson = "sound/" + url+".mp3";
        UnityWebRequest www =
            UnityWebRequestMultimedia.GetAudioClip("File://" + Path.Combine(Application.streamingAssetsPath, qJson),
                AudioType.UNKNOWN);
        var asyncOperarion = www.SendWebRequest();

        while (!www.isDone)
        {
            progress = asyncOperarion.progress;
            yield return null;
        }

        progress = 1f;
        if (!string.IsNullOrEmpty(www.error))
        {
            //algun error
        }

        audioClip1 = DownloadHandlerAudioClip.GetContent(www);
        audioSrc.clip = audioClip1;
        audioSrc.Play();


    }


}

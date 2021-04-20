using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class CodeRequest : MonoBehaviour
{
    // Start is called before the first frame update
    private string qJson, fileName;
    private ListCode listCode;
    void Start()
    {
        qJson = "codes_unique";
        fileName = Path.Combine(Application.streamingAssetsPath, qJson);
        StartCoroutine(GetRequest(fileName));
    }

    IEnumerator GetRequest(string uri)
    {
        yield return new WaitForSeconds(.4f); //ques e espere .2 sgs.
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            string json = webRequest.downloadHandler.text;
            ListCode q = JsonUtility.FromJson<ListCode>(json);
            if (q != null) listCode = q;
        }
    }

    IEnumerator SaveCodes(string code)
    {
        yield return new WaitForSeconds(.4f); //ques e espere .2 sgs.

        var listCodeActual = listCode.CodesAvailable.ToList();
        listCodeActual.Remove(listCode.CodesAvailable.Where(x => x.value == code).FirstOrDefault());
        listCode.CodesAvailable = listCodeActual.ToArray();

        string jsonData = JsonUtility.ToJson(listCode, true);
        File.WriteAllText(fileName, jsonData);

    }

    public bool CheckCode(string code)
    {
        var q = listCode.CodesAvailable.Where(x => x.value == code).FirstOrDefault();

        if (q != null)
        {
            StartCoroutine(SaveCodes(code));

            return true;
        }

        return false;
    }
}

[Serializable]
public class ListCode
{
    public CodeValues[] CodesAvailable;
}

[Serializable]
public class CodeValues
{
    public string value;
}

public class Response
{
    public int code;
    public string response;
}
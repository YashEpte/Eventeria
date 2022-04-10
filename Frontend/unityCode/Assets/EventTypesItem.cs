using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EventTypesItem : MonoBehaviour
{
    public Text event_name,event_decs,event_price;
    public string id;
    public Button view,registerButton;
    public void RegisterEvent()
    {
        StartCoroutine(registerUser());
    }
    IEnumerator registerUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("ticketCount", "1");
        form.AddField("eventId", id);
        UnityWebRequest unityWebRequest = UnityWebRequest.Post(NetworkManager.Instance.url + "/registration/register", form);
        unityWebRequest.SetRequestHeader("authorization", "Bearer "+PlayerPrefs.GetString("token"));
        yield return unityWebRequest.SendWebRequest();
        while (!unityWebRequest.isDone)
        {
            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.DataProcessingError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                break;
            }
            else
            {
                Debug.Log(unityWebRequest.downloadProgress);
            }
        }
        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(unityWebRequest.downloadHandler.data);
            Debug.Log("\n");
            Debug.Log(unityWebRequest.downloadHandler.text);
            AllClasesCallback.eventRegistrationCallback = JsonUtility.FromJson<EventRegistrationCallback>(unityWebRequest.downloadHandler.text);
            Debug.Log(AllClasesCallback.eventRegistrationCallback.status);
            Debug.Log(AllClasesCallback.eventRegistrationCallback.body.qrCode);

            AllClasesCallback.userData.body.events.Add(new UserDataEventList()
            {
                eventId = id,
                qrCode = AllClasesCallback.eventRegistrationCallback.body.qrCode
            });
            
            FindObjectOfType<MainEventHandler>().enableQrCode(AllClasesCallback.eventRegistrationCallback.body.qrCode);
        }
        else
        {
            Debug.Log("Failed");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    public string url;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator RegisterUser(string email,string name,string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("name", name);
        form.AddField("password", password);
        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url+"/user/register", form);
        yield return unityWebRequest.SendWebRequest();
        while(!unityWebRequest.isDone)
        {
            if(unityWebRequest.result==UnityWebRequest.Result.ConnectionError|| unityWebRequest.result == UnityWebRequest.Result.DataProcessingError|| unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
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
            AllClasesCallback.registrationCallback = JsonUtility.FromJson<RegistrationCallback>(unityWebRequest.downloadHandler.text);
            Debug.Log(AllClasesCallback.registrationCallback.status);
            Debug.Log(AllClasesCallback.registrationCallback.body.token);
            PlayerPrefs.SetString("token", AllClasesCallback.registrationCallback.body.token);
            AllClasesCallback.userData = JsonUtility.FromJson<UserData>(unityWebRequest.downloadHandler.text);
            
            GetAllEvent();
        }
        else
        {
            Debug.Log("Failed");
        }
    }
    IEnumerator LoginUser(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url + "/user/login", form);
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
            AllClasesCallback.loginCallback = JsonUtility.FromJson<LoginCallback>(unityWebRequest.downloadHandler.text);
            Debug.Log(AllClasesCallback.loginCallback.status);
            Debug.Log(AllClasesCallback.loginCallback.body.token);
            PlayerPrefs.SetString("token", AllClasesCallback.loginCallback.body.token);
            AllClasesCallback.userData = JsonUtility.FromJson<UserData>(unityWebRequest.downloadHandler.text);
            GetAllEvent();


        }
        else
        {
            Debug.Log("Failed");
        }
    }
    IEnumerator GetAllEvents()
    {
        
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url + "/event/");
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
            AllClasesCallback.allEventCallback = JsonUtility.FromJson<AllEventCallback>(unityWebRequest.downloadHandler.text);
            Debug.Log(AllClasesCallback.allEventCallback.status);
            Debug.Log(AllClasesCallback.allEventCallback.body.events.Count);
            Invoke("LoadScene", 1f);
        }
        else
        {
            Debug.Log("Failed");
        }
    }
    IEnumerator GetRefreshEvent(System.Action action,System.Action action1)
    {

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url + "/event/");
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
            AllClasesCallback.allEventCallback = JsonUtility.FromJson<AllEventCallback>(unityWebRequest.downloadHandler.text);
            Debug.Log(AllClasesCallback.allEventCallback.status);
            Debug.Log(AllClasesCallback.allEventCallback.body.events.Count);
            action();
            action1();
        }
        else
        {
            Debug.Log("Failed");
        }
    }
    public void RegisterUserCall(string email,string name,string password)
    {
        StartCoroutine(RegisterUser(email,name,password));
    }
    public void LoginUserCall(string email, string password)
    {
        StartCoroutine(LoginUser(email,password));
    }
    public void GetAllEvent()
    {
        StartCoroutine(GetAllEvents());
    }
    public void GetRefreshEvents(System.Action action, System.Action action1)
    {
        StartCoroutine(GetRefreshEvent(action,action1));
    }
    public void GetRegisterdAllEvent()
    {
        StartCoroutine(GetRegisterdAllEvents());
    }
    IEnumerator GetRegisterdAllEvents()
    {

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url + "/registration/getRegisteredEvents");
        unityWebRequest.SetRequestHeader("authorization", "Bearer " + PlayerPrefs.GetString("token"));
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
            AllClasesCallback.userData = JsonUtility.FromJson<UserData>(unityWebRequest.downloadHandler.text);
            Debug.Log(AllClasesCallback.userData.status);
            Debug.Log(AllClasesCallback.userData.body.events.Count);
            Invoke("LoadScene", 1f);
        }
        else
        {
            Debug.Log("Failed");
        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator RegisterNewEvent(string name, Texture2D banner, string description, string subEvent)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("description", description);
        form.AddField("subEvents", subEvent);
        byte[] data = banner.EncodeToPNG();
        form.AddBinaryData("banner", data, name, "images/png");
        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url + "/event/", form);
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
            if(unityWebRequest.downloadHandler.text.Contains("success"))
            {
                Debug.Log("Added Success");
            }
            else
            {
                Debug.Log("Failed");
            }

            GetAllEvent();
        }
        else
        {
            Debug.Log("Failed");
        }
    }

    public void AddEvent(string name, Texture2D banner, string description, string subEvent)
    {
        StartCoroutine(RegisterNewEvent(name, banner, description, subEvent));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

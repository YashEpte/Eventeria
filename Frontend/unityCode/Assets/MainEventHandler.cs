using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainEventHandler : MonoBehaviour
{
    public GameObject eventObject, eventObjectFeatur;
    public Transform eventObjectParent;
    public GameObject main, viewEvent, eventDetails,qrIDObject,allList;
    public GameObject eventMainViewObject;
    public Transform eventMainViewObjectParent;
    public GameObject eventViewObject;
    public Transform eventViewObjectParent;
    public GameObject enableSetting,mainObject;
    public Animator settingsAnimator;

    public Image qrIcon;

    public  RegistrationCallback registrationCallback = new RegistrationCallback();
    public  LoginCallback loginCallback = new LoginCallback();
    public  AllEventCallback allEventCallback = new AllEventCallback();
    public  UserData userData = new UserData();

    public List<GameObject> eventObjectList = new List<GameObject>();
    public List<GameObject> eventMainViewObjectList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        registrationCallback = AllClasesCallback.registrationCallback;
        loginCallback = AllClasesCallback.loginCallback;
        allEventCallback = AllClasesCallback.allEventCallback;
        userData = AllClasesCallback.userData;
        EnableOrDisableObject(main, true);
        EnableOrDisableObject(viewEvent, false);
        EnableOrDisableObject(qrIDObject, false);
        EnableOrDisableObject(eventDetails, false);
        EnableOrDisableObject(allList, false);
        FeaturedList();
        AllList();
    }
    public void RefreshList(string str)
    {
        if (str == "") {
            for (int i = 0; i < eventObjectList.Count; i++)
            {
                eventObjectList[i].SetActive(true);
        }
        }
        else
        {
            for (int i = 0; i < eventObjectList.Count; i++)
            {
                if (eventObjectList[i].GetComponent<MainEventObjectAssigner>().event_name.text.ToLower().Contains(str.ToLower()))
                {
                    eventObjectList[i].SetActive(true);
                }
                else
                {
                    eventObjectList[i].SetActive(false);    
                }
            }
        }
    }
    public void RefreshCategoryList(string str)
    {
        if (str == "")
        {
            for (int i = 0; i < eventObjectList.Count; i++)
            {
                eventObjectList[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < eventObjectList.Count; i++)
            {
                if (eventObjectList[i].GetComponent<MainEventObjectAssigner>().event_name.text.ToLower().Contains(str.ToLower()))
                {
                    eventObjectList[i].SetActive(true);
                }
                else
                {
                    eventObjectList[i].SetActive(false);
                }
            }
        }
    }
    public void Logout()
    {
        PlayerPrefs.SetString("token", "");
        SceneManager.LoadScene(0);
    }
    public void Home()
    {

        EnableOrDisableObject(main, true);
        EnableOrDisableObject(viewEvent, false);
        EnableOrDisableObject(qrIDObject, false);
        EnableOrDisableObject(eventDetails, false);
        EnableOrDisableObject(allList, false);
        NetworkManager.Instance.GetRefreshEvents(FeaturedList, AllList);
        //FeaturedList();
        //AllList();
        //for (int i = 0; i < eventObjectParent.transform.childCount; i++)
        //{
        //    Destroy(eventObjectParent.transform.GetChild(0).gameObject);
        //}
        //eventObjectList = new List<GameObject>();
        //for (int i = 0; i < AllClasesCallback.allEventCallback.body.events.Count; i++)
        //{
        //    GameObject g = Instantiate(eventObject, eventObjectParent);
        //    g.GetComponent<MainEventObjectAssigner>().event_name.text = AllClasesCallback.allEventCallback.body.events[i].name;
        //    g.GetComponent<MainEventObjectAssigner>().GetTextureCall(AllClasesCallback.allEventCallback.body.events[i].banner);
        //    eventObjectList.Add(g);
        //}
    }
    
    public void OpenAllEvent()
    {
        EnableOrDisableObject(main, false);
        EnableOrDisableObject(viewEvent, false);
        EnableOrDisableObject(qrIDObject, false);
        EnableOrDisableObject(eventDetails, false);
        EnableOrDisableObject(allList, true);
        for (int i = 0; i < eventObjectParent.transform.childCount; i++)
        {
            Destroy(eventObjectParent.transform.GetChild(i).gameObject);
        }
        eventObjectList = new List<GameObject>();
        for (int i = 0; i < AllClasesCallback.allEventCallback.body.events.Count; i++)
        {
            GameObject g = Instantiate(eventObject, eventObjectParent);
            g.GetComponent<MainEventObjectAssigner>().event_name.text = AllClasesCallback.allEventCallback.body.events[i].name;
            g.GetComponent<MainEventObjectAssigner>().GetTextureCall(AllClasesCallback.allEventCallback.body.events[i].banner);
            g.GetComponent<MainEventObjectAssigner>().index = i;
            eventObjectList.Add(g);
        }
    }
    public Transform fParent, cParent, aParent;
    public void FeaturedList()
    {
        for (int i = 0; i < fParent.transform.childCount; i++)
        {
            Destroy(fParent.transform.GetChild(i).gameObject);
        }
        eventObjectList = new List<GameObject>();
        for (int i = 0; i < AllClasesCallback.allEventCallback.body.events.Count; i++)
        {
            if (AllClasesCallback.allEventCallback.body.events[i].isFeatured.Equals("true"))
            {
                GameObject g = Instantiate(eventObjectFeatur, fParent);
                g.GetComponent<MainEventObjectAssigner>().event_name.text = AllClasesCallback.allEventCallback.body.events[i].name;
                g.GetComponent<MainEventObjectAssigner>().GetTextureCall(AllClasesCallback.allEventCallback.body.events[i].banner);
                g.GetComponent<MainEventObjectAssigner>().index = i;
                eventObjectList.Add(g);
            }
        }
    }
    public void CategoryList()
    {
        for (int i = 0; i < eventObjectParent.transform.childCount; i++)
        {
            Destroy(eventObjectParent.transform.GetChild(0).gameObject);
        }
        eventObjectList = new List<GameObject>();
        for (int i = 0; i < AllClasesCallback.allEventCallback.body.events.Count; i++)
        {
            GameObject g = Instantiate(eventObject, eventObjectParent);
            g.GetComponent<MainEventObjectAssigner>().event_name.text = AllClasesCallback.allEventCallback.body.events[i].name;
            g.GetComponent<MainEventObjectAssigner>().GetTextureCall(AllClasesCallback.allEventCallback.body.events[i].banner);
            eventObjectList.Add(g);
        }
    }
    public void AllList()
    {
        for (int i = 0; i < aParent.transform.childCount; i++)
        {
            Destroy(aParent.transform.GetChild(i).gameObject);
        }
        eventObjectList = new List<GameObject>();
        for (int i = 0; i < AllClasesCallback.allEventCallback.body.events.Count; i++)
        {
            GameObject g = Instantiate(eventObjectFeatur, aParent);
            g.GetComponent<MainEventObjectAssigner>().event_name.text = AllClasesCallback.allEventCallback.body.events[i].name;
            g.GetComponent<MainEventObjectAssigner>().GetTextureCall(AllClasesCallback.allEventCallback.body.events[i].banner);
            g.GetComponent<MainEventObjectAssigner>().index = i;
            eventObjectList.Add(g);
        }
    }
    public void ViewDetails(int index)
    {
        EnableOrDisableObject(main, false);
        EnableOrDisableObject(viewEvent, true);
        EnableOrDisableObject(eventDetails, false);
        EnableOrDisableObject(allList, false);
        EnableOrDisableObject(qrIDObject, false);
        for (int i=0;i<eventMainViewObjectParent.transform.childCount;i++)
        {
            Destroy(eventMainViewObjectParent.transform.GetChild(i).gameObject);
        }
        eventMainViewObjectList = new List<GameObject>();
        GameObject gO= Instantiate(eventMainViewObject, eventMainViewObjectParent);
        gO.GetComponent<MainEventBannerDetailsItem>().event_name.text = AllClasesCallback.allEventCallback.body.events[index].name;
        gO.GetComponent<MainEventBannerDetailsItem>().GetTextureCall(AllClasesCallback.allEventCallback.body.events[index].banner);
        gO.GetComponent<MainEventBannerDetailsItem>().event_decs.text = AllClasesCallback.allEventCallback.body.events[index].description;
        eventMainViewObjectList.Add(gO);

        for (int i=0;i< AllClasesCallback.allEventCallback.body.events[index].subEvents.Count;i++)
        {

            GameObject g = Instantiate(eventViewObject, eventViewObjectParent);
            g.GetComponent<EventTypesItem>().event_name.text = AllClasesCallback.allEventCallback.body.events[index].subEvents[i].name;
            g.GetComponent<EventTypesItem>().event_decs.text = AllClasesCallback.allEventCallback.body.events[index].subEvents[i].description;

            g.GetComponent<EventTypesItem>().id = AllClasesCallback.allEventCallback.body.events[index].subEvents[i]._id;
            bool isthere = false;
            string qrId = "";
            for(int j=0;j< AllClasesCallback.userData.body.events.Count;j++)
            {
                if(AllClasesCallback.userData.body.events[j].eventId.Equals( AllClasesCallback.allEventCallback.body.events[index].subEvents[i]._id))
                {
                    isthere = true;
                    qrId = AllClasesCallback.userData.body.events[j].qrCode;
                    break;
                }
            }
            if (isthere)
            {
                g.GetComponent<EventTypesItem>().view.gameObject.SetActive(true);
                g.GetComponent<EventTypesItem>().registerButton.gameObject.SetActive(false);
                g.GetComponent<EventTypesItem>().view.onClick.AddListener(() => enableQrCode(qrId));
            }
            else
            {
                g.GetComponent<EventTypesItem>().view.gameObject.SetActive(false);
                g.GetComponent<EventTypesItem>().registerButton.gameObject.SetActive(true);
            }

            eventMainViewObjectList.Add(g);
        }

    }

    public void enableQrCode(string qrID)
    {
        string[] str = qrID.Split(',');

        //Load Scanner
        byte[] imageBytes = Convert.FromBase64String(str[1]);//"iVBORw0KGgoAAAANSUhEUgAAAHQAAAB0CAYAAABUmhYnAAAAAklEQVR4AewaftIAAALbSURBVO3BQa7jSAwFwXyE7n/lnL/kqgBBsqdNMCL+YY1RrFGKNUqxRinWKMUapVijFGuUYo1SrFGKNUqxRinWKMUapVijFGuUi4eS8E0qJ0noVE6S0Kl0SfgmlSeKNUqxRinWKBcvU3lTEk6S0Kl0SehUOpU7VN6UhDcVa5RijVKsUS4+LAl3qNyhcqJykoRO5Y4k3KHyScUapVijFGuUi2GS0Kl0SehUJinWKMUapVijXPy4JHQqXRI6lS4JncovK9YoxRqlWKNcfJjKJ6l0SfgmlX9JsUYp1ijFGuXiZUn4piR0Kl0S3pSEf1mxRinWKMUaJf5hkCQ8ofLLijVKsUYp1igXDyWhU+mS0Kl0SehUuiR0KicqJ0noVJ5IQqdykoRO5U3FGqVYoxRrlIuHVLokvEnlJAmdSpeETuWOJHQqnUqXhE7lm4o1SrFGKdYoFw8l4USlS0KncpKETuVNSehUOpWTJHQqXRI6lS4JncoTxRqlWKMUa5T4h/9REk5UuiR0KidJ6FTuSEKn8kQSOpU3FWuUYo1SrFEuXpaENyWhU3lTEjqVO5LQqXRJ6FS6JHQqTxRrlGKNUqxR4h9+WBJOVLokdCpdEjqVLgmdyh1J6FTeVKxRijVKsUa5eCgJ36TSqXxSEp5IQqfyScUapVijFGuUi5epvCkJn5SEE5UuCV0STlS+qVijFGuUYo1y8WFJuEPljiR0Kl0STlS6JHRJOFE5SUKn0iWhU3miWKMUa5RijXLx41S6JJyonKh0SehUnkhCp/KmYo1SrFGKNcrFj0vCicpJEjqVTuUkCZ1Kp3KShE7liWKNUqxRijXKxYepfJJKl4Q7VE6S0Kl0KidJ6FQ6lTcVa5RijVKsUS5eloRvSkKnckcSOpU7ktCp3JGETuWJYo1SrFGKNUr8wxqjWKMUa5RijVKsUYo1SrFGKdYoxRqlWKMUa5RijVKsUYo1SrFGKdYo/wEzLDTlnNEx6gAAAABJRU5ErkJggg==");
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        qrIcon.sprite = sprite;
        EnableOrDisableObject(main, false);
        EnableOrDisableObject(viewEvent, false);
        EnableOrDisableObject(qrIDObject, true);
        EnableOrDisableObject(eventDetails, false);
        EnableOrDisableObject(allList, false);
    }
    //IEnumerator LoadQr(string Id)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("eventQrID", Id);
    //    UnityWebRequest unityWebRequest = UnityWebRequest.Post(NetworkManager.Instance.url + "/registration/register", form);
    //    unityWebRequest.SetRequestHeader("authorization", "Bearer " + PlayerPrefs.GetString("token"));
    //    yield return unityWebRequest.SendWebRequest();
    //    while (!unityWebRequest.isDone)
    //    {
    //        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.DataProcessingError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            break;
    //        }
    //        else
    //        {
    //            Debug.Log(unityWebRequest.downloadProgress);
    //        }
    //    }
    //    if (unityWebRequest.result == UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log(unityWebRequest.downloadHandler.data);
    //        Debug.Log("\n");
    //        Debug.Log(unityWebRequest.downloadHandler.text);
    //        AllClasesCallback.eventRegistrationCallback = JsonUtility.FromJson<EventRegistrationCallback>(unityWebRequest.downloadHandler.text);
    //        Debug.Log(AllClasesCallback.eventRegistrationCallback.status);
    //        Debug.Log(AllClasesCallback.eventRegistrationCallback.body.token);

    //        string[] str = AllClasesCallback.eventRegistrationCallback.body.token.Split(',');

    //        //Load Scanner
    //        byte[] imageBytes = Convert.FromBase64String(str[1]);//"iVBORw0KGgoAAAANSUhEUgAAAHQAAAB0CAYAAABUmhYnAAAAAklEQVR4AewaftIAAALbSURBVO3BQa7jSAwFwXyE7n/lnL/kqgBBsqdNMCL+YY1RrFGKNUqxRinWKMUapVijFGuUYo1SrFGKNUqxRinWKMUapVijFGuUi4eS8E0qJ0noVE6S0Kl0SfgmlSeKNUqxRinWKBcvU3lTEk6S0Kl0SehUOpU7VN6UhDcVa5RijVKsUS4+LAl3qNyhcqJykoRO5Y4k3KHyScUapVijFGuUi2GS0Kl0SehUJinWKMUapVijXPy4JHQqXRI6lS4JncovK9YoxRqlWKNcfJjKJ6l0SfgmlX9JsUYp1ijFGuXiZUn4piR0Kl0S3pSEf1mxRinWKMUaJf5hkCQ8ofLLijVKsUYp1igXDyWhU+mS0Kl0SehUuiR0KicqJ0noVJ5IQqdykoRO5U3FGqVYoxRrlIuHVLokvEnlJAmdSpeETuWOJHQqnUqXhE7lm4o1SrFGKdYoFw8l4USlS0KncpKETuVNSehUOpWTJHQqXRI6lS4JncoTxRqlWKMUa5T4h/9REk5UuiR0KidJ6FTuSEKn8kQSOpU3FWuUYo1SrFEuXpaENyWhU3lTEjqVO5LQqXRJ6FS6JHQqTxRrlGKNUqxR4h9+WBJOVLokdCpdEjqVLgmdyh1J6FTeVKxRijVKsUa5eCgJ36TSqXxSEp5IQqfyScUapVijFGuUi5epvCkJn5SEE5UuCV0STlS+qVijFGuUYo1y8WFJuEPljiR0Kl0STlS6JHRJOFE5SUKn0iWhU3miWKMUa5RijXLx41S6JJyonKh0SehUnkhCp/KmYo1SrFGKNcrFj0vCicpJEjqVTuUkCZ1Kp3KShE7liWKNUqxRijXKxYepfJJKl4Q7VE6S0Kl0KidJ6FQ6lTcVa5RijVKsUS5eloRvSkKnckcSOpU7ktCp3JGETuWJYo1SrFGKNUr8wxqjWKMUa5RijVKsUYo1SrFGKdYoxRqlWKMUa5RijVKsUYo1SrFGKdYo/wEzLDTlnNEx6gAAAABJRU5ErkJggg==");
    //        Texture2D tex = new Texture2D(2, 2);
    //        tex.LoadImage(imageBytes);
    //        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    //        qrIcon.sprite = sprite;

    //    }
    //    else
    //    {
    //        Debug.Log("Failed");
    //    }
        
    //}


    void EnableOrDisableObject(GameObject g, bool b)
    {
        g.SetActive(b);
    }
    public void EnableSetting()
    {
        if(enableSetting.activeSelf)
        {
            settingsAnimator.SetTrigger("Out");
            Invoke("DisableSetting", 1f);
        }
        else
        {
            settingsAnimator.SetTrigger("In");
            EnableOrDisableObject(mainObject, false);
        }
    }
    void DisableSetting()
    {
        EnableOrDisableObject(mainObject, true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Home();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenManager : MonoBehaviour
{
    public GameObject initUI, registrationUI, loginUI, forgetPasswordUI, errorPopup;

    public Text errorMsg;

    public InputField email_r;
    public InputField name_r;
    public InputField password_r;

    public InputField email_l;
    public InputField password_l;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("token"))
        {
            string key = PlayerPrefs.GetString("token");
            if (key != "")
            {
                print("Key +" + key);
                NetworkManager.Instance.GetAllEvent();
                NetworkManager.Instance.GetRegisterdAllEvent();
            }
        }
    }
    void EnableOrDisableObject(GameObject g,bool b)
    {
        g.SetActive(b);
    }
    public void Register()
    {
        if (email_r.text == "" || name_r.text == "" || password_r.text == "")
        {
            errorMsg.text = "Some Field is Empty";
            EnableOrDisableObject(errorPopup, true);
            Invoke("DiableErrorPopup",1.5f);
        }
        else
        {
            NetworkManager.Instance.RegisterUserCall(email_r.text, name_r.text, password_r.text);
        }
    }
    public void Login()
    {
        if (email_l.text == "" || password_l.text == "")
        {
            errorMsg.text = "Some Field is Empty";
            EnableOrDisableObject(errorPopup, true);
            Invoke("DiableErrorPopup", 1.5f);
        }
        else
        {
            NetworkManager.Instance.LoginUserCall(email_l.text, password_l.text);
        }
    }
    void DiableErrorPopup()
    {
        EnableOrDisableObject(errorPopup, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

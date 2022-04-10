using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllClasesCallback : MonoBehaviour
{
    public static RegistrationCallback registrationCallback = new RegistrationCallback();
    public static LoginCallback loginCallback = new LoginCallback();
    public static AllEventCallback allEventCallback = new AllEventCallback();
    public static EventRegistrationCallback eventRegistrationCallback = new EventRegistrationCallback();
    public static UserData userData = new UserData();

}

[System.Serializable]
public class RegistrationCallback
{
    public string status;
    public RegistrationBodyCallback body = new RegistrationBodyCallback();
}
[System.Serializable]
public class RegistrationBodyCallback
{
    public string token;
}

[System.Serializable]
public class LoginCallback
{
    public string status;
    public LoginBodyCallback body = new LoginBodyCallback();
}
[System.Serializable]
public class LoginBodyCallback
{
    public string token;
}


[System.Serializable]
public class AllEventCallback
{
    public string status;
    public AllEventBodyCallback body = new AllEventBodyCallback();
}
[System.Serializable]
public class AllEventBodyCallback
{
    public List<AllEventsCallback> events=new List<AllEventsCallback>();
}
[System.Serializable]
public class AllEventsCallback
{
    public string id;
    public string name;
    public string description;
    public string banner;
    public string isFeatured;
    public List<AllSubEventsCallback> subEvents=new List<AllSubEventsCallback>();
    public string __v;
}
[System.Serializable]
public class AllSubEventsCallback
{
    public string name;
    public string description;
    public string venue;
    public string date;
    public string totalSeats;
    public string bookedSeates;
    public string _id;
    public string price;
}


[System.Serializable]
public class EventRegistrationCallback
{
    public string status;
    public EventRegistrationBodyCallback body = new EventRegistrationBodyCallback();
}
[System.Serializable]
public class EventRegistrationBodyCallback
{
    public string qrCode;
}


[System.Serializable]
public class UserData
{
    public string status;
    public UserDataBody body = new UserDataBody();
}
[System.Serializable]
public class UserDataBody
{
    public string token;
    public List<UserDataEventList> events = new List<UserDataEventList>();
}
[System.Serializable]
public class UserDataEventList
{
    public string eventId;
    public string qrCode;
}


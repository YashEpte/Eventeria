using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class AddNewEvent : MonoBehaviour
{
    public Image Raw;
    public InputField eName, sDetails, locDetails, sDate, eDate;
    public List<GameObject> addSections = new List<GameObject>();
    public List<AddSection> sections = new List<AddSection>();
    public GameObject newSection, secParent;
    Texture2D texture2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void addNewSection()
    {
        //addSections[addSections.Count - 1].transform.GetChild(addSections[addSections.Count - 1].transform.childCount - 1).gameObject.SetActive(false);
        GameObject g = Instantiate(newSection, secParent.transform);
        addSections.Add(g);
        sections.Add(g.GetComponent<SubEventAdder>().newEvent);
    }
    public void Save()
    {
        string desc = sDetails.text + "\nLocation : " + locDetails.text + "\nStart Date: " + sDate.text + "\nEnd Date" + eDate.text;
        Debug.Log(desc);
        Debug.Log((Texture2D)Raw.mainTexture);
        Debug.Log(eName.text);
        Debug.Log(JsonUtility.ToJson(sections));
        NetworkManager.Instance.AddEvent(eName.text,(Texture2D)Raw.mainTexture, desc, JsonUtility.ToJson(sections));
    }

    public void Button()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            FindObjectOfType<FIleManager>().selectImg(Raw);
        }
        //else
        //{
        //    FindObjectOfType<FIleManager>().openFileExplorer(Raw);
        //}
        //PickImage(512);
    }
    //public void PickImage(int maxSize)
    //{
    //    NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
    //    {
    //        Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
    //        if (texture == null)
    //        {
    //            Debug.Log("Couldn't load texture from " + path);
    //            return;
    //        }
    //        Raw = GetComponent<RawImage>();
    //        Raw.texture = texture;
    //        texture2D = (Texture2D)Raw.texture;
    //    }, "Select a PNG image", "image/png");
    //    Debug.Log("Permission result: " + permission);
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class AddSection
{
    public InputField ename, details, fees;
    public string name, description, price,totalSeates="100",bookedSeates="0",venue="Mumbai";
}
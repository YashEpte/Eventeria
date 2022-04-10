using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;
using static NativeGallery;
public class FIleManager : MonoBehaviour
{
    public Image img;
    public Sprite noPhoto;
    public Texture2D myTexture;
    public GameObject msgBox;
    #region Editor
#if UNITY_EDITOR
    public void openFileExplorer(Image image)
    {
        img = image;
        string path = EditorUtility.OpenFilePanel("Show all images(.png)", "", "png");
        Debug.Log(path);
        StartCoroutine(getTexture(path));
    }


#endif

    #endregion

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
//        msgBoxPos = msgBox.transform.position;
    }
    IEnumerator getTexture(string path)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
             myTexture= ((DownloadHandlerTexture)www.downloadHandler).texture;
            img.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
        }
    }
    public void selectImg(Image image)
    {
        img = image;
        MediaPickCallback callback = new MediaPickCallback(trial);
        GetImageFromGallery(callback, "Select Identity Proof", "image/*");
    }
    private void trial(string str)
    {
        Debug.Log("TRIAL: " + str);
        StartCoroutine(getTexture(str));
    }
    
    public void submitPhoto()
    {
       // showMsg("KYC submitted successfully!");
        //Debug.Log("SUBMIT ");
        //if (myTexture != null)
        //StartCoroutine(uploadPhoto(myTexture)); remove comment afterwards
    }
    IEnumerator uploadPhoto(Texture2D imgTexture)
    {
        Debug.Log("upload started");
        WWWForm form = new WWWForm();
        byte[] imgData = null;
        imgData = imgTexture.EncodeToPNG();
        form.AddBinaryData("uIData", imgData, "BhavinDivecha.png", "image/png");
        UnityWebRequest webRequest = UnityWebRequest.Post("", form);
        yield return webRequest.SendWebRequest();
        if (webRequest.downloadHandler.text != "cFailed" && webRequest.downloadHandler.text != "UnSucess")
        {
            img.enabled = false;
        }
        else
        {
            img.sprite = noPhoto;
        }

    }
}

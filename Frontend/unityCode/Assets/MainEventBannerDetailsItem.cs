using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;

public class MainEventBannerDetailsItem : MonoBehaviour
{
    public Image icon;
    public Text event_name,event_decs;

    public void GetTextureCall(string url)
    {
        StartCoroutine(GetTexture(url));
    }

    

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(NetworkManager.Instance.url + "/images/" + url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            icon.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0, 0));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using LitJson;

public class getPhoto : MonoBehaviour
{
    // 直接拖过来button和按钮上的image
    public Button btn;
    public Image ImageView;
    public Image ImageTEST;
    AndroidJavaObject jo;
    public byte[] bye;

    
    private void Awake()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        btn.onClick.AddListener(CallAndroid);
        bye = null;
    }
    
    private void Start()
    {
        eventCenter.AddListener<string>(staticVariable.upload_image, Upload_image);
        eventCenter.AddListener(staticVariable.submit_success, refresh_image);
    }
    
    public void refresh_image()
    {
        bye = null;
        ImageView.sprite = null;
    }

    /// <summary>
    /// CALL>>>ANDROID>>>打开相册
    /// </summary>
    void CallAndroid()
    {
        jo.Call("startPhoto");
    }
    /// <summary>
    /// 给Android调用的
    /// </summary>
    /// <param name="str"></param>
    public void CallUnity(string str)
    {
        ShowImage(str);
        jo.Call("CallAndroid", string.Format("图片Address>>>>" + str));

        //string path = "file://"  + str;
        //StartCoroutine(LoadTexturePreview(path));
    }
   
    //使用文件流读取图片
    private void ShowImage(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        bye = new byte[fileStream.Length];
        fileStream.Read(bye, 0, (int)bye.Length);
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;
        Texture2D texture = new Texture2D(100, 50);
        texture.LoadImage(bye);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        ImageView.sprite = sprite;
    }
    

    public void Upload_image(string title)
    {
        if(bye == null)
        {
            return;
        }
        StartCoroutine(IUpload_image(title));
    }

    IEnumerator IUpload_image(string title)
    {
        Texture2D texture = duplicateTexture(ImageView.sprite.texture);
        bye = texture.EncodeToJPG();
        eventCenter.PostEvent(staticVariable.callPaiMeng);
        sendPhoto r = new sendPhoto();
        r.name = title;    
        r.file = Convert.ToBase64String(bye);
        Debug.Log(r.file);
        string data = JsonMapper.ToJson(r);
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(data);
        using (UnityWebRequest request = new UnityWebRequest("http://8.134.67.143:7878/upload/v2", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + staticVariable.token);
            yield return request.SendWebRequest();
            eventCenter.PostEvent(staticVariable.closePaiMeng);
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("上传失败:" + request.error);
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "上传图片失败");
            }
            else
            {
                Debug.Log("服务器返回值" + request.downloadHandler.text);
                /*
                urlRoot url = JsonMapper.ToObject<urlRoot>(@request.downloadHandler.text);
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "上传图片成功");
                eventCenter.PostEvent(staticVariable.submit2, url.data.url);
                */
            }
        }      
    }
    public class sendPhoto
    {
        public string name { get; set; }
        public string file {  get;set; }
    }

    public class Data
    {
        public string url { get; set; }
    }

    public class urlRoot
    {
        public int code { get; set; }
        public string success { get; set; }
        public Data data { get; set; }
    }

    public Texture2D DeCompress(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }


    private Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }


}

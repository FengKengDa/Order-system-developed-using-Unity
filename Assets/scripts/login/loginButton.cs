using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class loginButton : MonoBehaviour
{
    public InputField uid;
    public InputField password;

    private void Awake()
    {
        uid.text = "";
        password.text = "";
    }
    private void OnEnable()
    {
        uid.text = "";
        password.text = "";
    }

    public void login()
    {
        if (uid.text == "")
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "用户ID不能为空");
            return;
        }
        if(password.text == "")
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "密码不能为空");
            return;
        }
        eventCenter.PostEvent<string, string>(staticVariable.login, uid.text, password.text);
    }


}

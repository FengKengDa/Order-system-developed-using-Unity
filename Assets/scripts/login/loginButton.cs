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
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "�û�ID����Ϊ��");
            return;
        }
        if(password.text == "")
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "���벻��Ϊ��");
            return;
        }
        eventCenter.PostEvent<string, string>(staticVariable.login, uid.text, password.text);
    }


}

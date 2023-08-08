using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class submitAssPage : MonoBehaviour
{
    [HideInInspector]
    public int tid;

    public GameObject departmentContent;

    public void submit()
    {
        if(departmentContent.transform.childCount==0)
        {
            eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "�����벿����Ϣ");
            return;
        }

        List<List<string>> depa = new List<List<string>>();

        for(int i=0;i<departmentContent.transform.childCount;i++)
        {
            GameObject departmentPre = departmentContent.transform.GetChild(i).gameObject;
            string dep, num;
            dep = departmentPre.transform.GetChild(0).GetChild(0).gameObject.GetComponent<InputField>().text;
            num = departmentPre.transform.GetChild(1).GetChild(0).gameObject.GetComponent<InputField>().text;
            if(dep==""||num=="")
            {
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "�����������Ĳ�����Ϣ");
                return;
            }
            depa.Add(new List<string>(){dep,num });
        }

        //TODO: �����ύЭ������֪ͨ
        eventCenter.PostEvent<List<List<string>>, int>(staticVariable.submit_ass, depa,tid);


        for (int i = 0; i < departmentContent.transform.childCount; i++)
        {
            Destroy(departmentContent.transform.GetChild(i));
            
        }
        gameObject.SetActive(false);

    }

    public void closeAssPage()
    {
        tid = -1;
        eventCenter.PostEvent<int>(staticVariable.banAllCloseChild, 6);
    }



}

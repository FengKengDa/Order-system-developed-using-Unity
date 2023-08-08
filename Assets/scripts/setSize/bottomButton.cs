using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bottomButton : MonoBehaviour
{
    public int numberOfChoices;
    public int buttonPosition;

    public List<GameObject> pages;
    void Start()
    {
        staticVariable.screen_width = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        staticVariable.screen_heigh = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        //֪ͨ�����ű�����UI�ߴ�
        eventCenter.PostEvent(staticVariable.screenSizeChanged);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(staticVariable.screen_width/numberOfChoices, staticVariable.screen_heigh/10);
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(staticVariable.screen_width/numberOfChoices*buttonPosition, 0, 0);
    }

    public void openPage()
    {
        //����ҳֻ����ά����Ա��
        if(buttonPosition == 1)
        {
            //�ݶ�1��ά����Ա���ٸ�
            if(staticVariable.user_type != 1)
            {
                eventCenter.PostEvent<string>(staticVariable.setErrorInformation, "��ҳ��ֻ��ά����Ա����");
                return;
            }
        }

        for(int i=0;i<pages.Count;i++)
        {
            if(buttonPosition==i)
            {
                pages[i].SetActive(true);
                if(i == 1)
                {
                    eventCenter.PostEvent(staticVariable.loadTicket);
                }
                if(i==2)
                {
                    eventCenter.PostEvent(staticVariable.get_current_ticket);
                    eventCenter.PostEvent(staticVariable.get_history_ticket);
                }
            }
            else
            {
                pages[i].SetActive(false);
            }
        }



    }


    
}

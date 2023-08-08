using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refreshSubmit : MonoBehaviour
{
    // Start is called before the first frame update
    public void onclick()
    {
        eventCenter.PostEvent(staticVariable.submit_success);
        eventCenter.PostEvent("department", 0);
        eventCenter.PostEvent("fundlist", 0);
    }
}

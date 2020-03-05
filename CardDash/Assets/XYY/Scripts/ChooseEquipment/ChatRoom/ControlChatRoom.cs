using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlChatRoom : MonoBehaviour
{
 public InputField chatInput;
    public Text chatText;
    public ScrollRect scrollRect;
    string username = "DHX";
    // Use this for initialization
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()//pivot y  应该设置为1.0
        {
 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (chatInput.text != "")
            {
                string addText = "\n" + "<color=red>" + username + "</color>:<color=black>" + chatInput.text+"</color>";
                chatText.text += addText;
                chatInput.text = "";
                chatInput.ActivateInputField();
                StartCoroutine(waitUpdate());
               
            }
        }
        IEnumerator waitUpdate(){
           Canvas.ForceUpdateCanvases();       //关键代码
            yield return null;
             scrollRect.verticalNormalizedPosition = 0f;  //关键代码
             Canvas.ForceUpdateCanvases();   //关键代码
        }
 
    }
}

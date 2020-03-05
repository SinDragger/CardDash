using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CharacShow_ActiveAble : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    UI_CharacShow_Text target;
    bool isIn;
    float count;
    //-数据——————
    int runtimePlayerId;
    int characId;
    bool isInGame;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isIn = true;
    }

    public void Init(int _r,int _c,bool _i) {
        runtimePlayerId = _r;
        characId = _c;
        isInGame = _i;
        target = UIController.instance.characShowText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isIn = false;
        count = 0f;
        if (target)
        {
            target.Close();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
        //向UIController拿这个
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn&&target)
        {
            count += Time.deltaTime;
            if (count > UI_CharacShow_Text.timeCountDown)
            {
                Debug.Log("Open");
                target.Show(transform.position);
            }
        }
    }
}

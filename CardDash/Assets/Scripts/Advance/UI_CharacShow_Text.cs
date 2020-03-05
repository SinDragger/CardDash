using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacShow_Text : MonoBehaviour
{
    public Text characName;
    public Text characEffect;
    public Text characDescribe;

    public static float timeCountDown = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenewData()
    {
        //Character拿数据
    }

    public void Show(Vector3 position)
    {
        gameObject.SetActive(true);
        RenewData();
    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
}

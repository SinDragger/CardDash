using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;
public class UI_DeadEffect : MonoBehaviour, DataReceiver
{

    public void RenewData()
    {
        transform.SetAsLastSibling();
        //当下属的全部置Active
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void RenewData(List<string> temp)
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIController.instance.attachRenewList(this, DataChangeType.Dead);
    }
}

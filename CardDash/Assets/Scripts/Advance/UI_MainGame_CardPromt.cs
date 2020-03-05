using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;
public class UI_MainGame_CardPromt : MonoBehaviour
{
    public UI_CardSlot_2 weapon;
    public UI_CardSlot_2 equip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseCard()
    {
        weapon.gameObject.SetActive(false);
        equip.gameObject.SetActive(false);
    }
    public void showCard(Vector3 position,CardContainerType type,int id)
    {
        //寻找卡
        if (position == Vector3.zero)
        {

        }
        else
        {
            transform.position = position;
        }
        
        if (type == CardContainerType.Weapon)
        {
            weapon.gameObject.SetActive(true);
            weapon.RenewData(RaceController.instance.GetRealPlayerWeapon(id));
            equip.gameObject.SetActive(false);
        }
        else
        {
            weapon.gameObject.SetActive(false);
            equip.gameObject.SetActive(true);
            equip.RenewData(RaceController.instance.GetRealPlayerEquip(id));
        }
    }
}

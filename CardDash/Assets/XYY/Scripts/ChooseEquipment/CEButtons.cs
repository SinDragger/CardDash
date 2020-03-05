using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace xyy
{
    public class CEButtons : MonoBehaviour
    {
        private int nowCharacterId;
        public static CEButtons singleton = null;
        private bool enlargeStatus;
        private Character nowCharacter;
        private wyc.ThugSource player;
        public Weapon[] weapons;
        private List<wyc.WeaponSource> weaponSources;
        public Refitting[] refittings;
        private List<wyc.EquipSource> equipSources;
        private List<wyc.WeaponSource> buyedWeapons;
       
        private List<wyc.EquipSource> buyedEquips;
        public GameObject ShelterPanel;
        public CE_PlayerData ce;
        void Awake()
        {
            if (singleton == null)
            {//单例模式
                singleton = this;
            }
            else if (singleton != this)
            {
                Destroy(gameObject);
            };
            enlargeStatus = false;

        //}
        //void Start()
        //{
            buyedWeapons = new List<wyc.WeaponSource>();
            buyedEquips = new List<wyc.EquipSource>();
            //nowCharacterId=GameManager.instance.mainCharacterId;
            nowCharacterId = wyc.GameController.instance.playerThugId;
            player = wyc.DataController.instance.GetThugSourceById(wyc.GameController.instance.playerThugId);
            weaponSources = wyc.DataController.instance.BuyAbleWeapons();
            equipSources = wyc.DataController.instance.BuyAbleRandomEquips();
            if (ce != null) { ce.DataUpdate(player);}
            nowCharacter = GameManager.instance.nowCharcter;
            //weapons = GameManager.instance.weapons;
            weapons = ToolTransForm.InitWeaponTransForm(weaponSources);
            //refittings = GameManager.instance.refittings;
            refittings = ToolTransForm.InitEquipTransForm(equipSources);
        }
        #region enlarging
        public bool getEnlargeStatus()
        {
            return enlargeStatus;
        }
        public void setEnlargeStatus(bool b)
        {
            enlargeStatus = b;
        }
        #endregion
        #region Extra
        public void Extra()
        {
            Debug.Log("This is Extra Menu");
        }
        #endregion
        #region NextScene
        public void Confirm()
        {
            Debug.Log("this is next Scene");
            GameManager.instance.nowCharcter = nowCharacter;
            GameManager.instance.nowCharcter.weapon = HaveBuyedCards.buyedWeapon;
            Debug.Log("Test");
            foreach (var d in GameManager.instance.nowCharcter.weapon) {
                Debug.Log(d.Id);
            }
            GameManager.instance.nowCharcter.refitting = HaveBuyedCards.buyedRefitting;
            // SceneManager.LoadScene("ChooseEquipment"); load main scene
            //修改Source内容数据
            Debug.Log(HaveBuyedCards.buyedRefitting.Count);
            Debug.Log(HaveBuyedCards.buyedWeapon.Count);
            wyc.GameController.instance.RenewPlayerEquipAndWeapons(buyedEquips,buyedWeapons);
            SceneManager.LoadScene("MainGame");
        }
        #endregion
        #region  sort
        public void moenyRank()
        {

            WeaponInit.weaponCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetWeapon().price.CompareTo(y.GetComponent<EquipmentCards>().GetWeapon().price); });
            RefittingInit.refittingCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetRefitting().money.CompareTo(y.GetComponent<EquipmentCards>().GetRefitting().money); });
            WeaponInit.singleton.renewCards();
            RefittingInit.singleton.renewCards();
        }
        public void typeRank()
        {
            WeaponInit.weaponCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetWeapon().type.CompareTo(y.GetComponent<EquipmentCards>().GetWeapon().type); });
            RefittingInit.refittingCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetRefitting().type.CompareTo(y.GetComponent<EquipmentCards>().GetRefitting().type); });
            WeaponInit.singleton.renewCards();
            RefittingInit.singleton.renewCards();
        }
        public void damageRank()
        {
            WeaponInit.weaponCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetWeapon().damage.CompareTo(y.GetComponent<EquipmentCards>().GetWeapon().damage); });
            WeaponInit.singleton.renewCards();
        }
        public void threatRank()
        {
            WeaponInit.weaponCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetWeapon().threatening.CompareTo(y.GetComponent<EquipmentCards>().GetWeapon().threatening); });
            WeaponInit.singleton.renewCards();
        }
        public void effectllevelRank()
        {
            RefittingInit.refittingCards.Sort(delegate (GameObject x, GameObject y) { return x.GetComponent<EquipmentCards>().GetRefitting().effectllevel.CompareTo(y.GetComponent<EquipmentCards>().GetRefitting().effectllevel); });
            RefittingInit.singleton.renewCards();
        }

        #endregion
        #region 

        #endregion
    }
}


public class ToolTransForm
{
    public static xyy.Weapon[] InitWeaponTransForm(List<wyc.WeaponSource> weapons)
    {
        xyy.Weapon[] result= new xyy.Weapon[weapons.Count];
        for(int i = 0; i < weapons.Count; i++)
        {
            result[i] = WYCWeaponSourceToXYYWeapon(weapons[i]);
            //Debug.Log(result)
        }
        return result;
    }
    public static xyy.Refitting[] InitEquipTransForm(List<wyc.EquipSource> equips)
    {
        xyy.Refitting[] result= new xyy.Refitting[equips.Count];
        for(int i = 0; i < equips.Count; i++)
        {
            result[i] = WYCEquipSourceToXYYWeapon(equips[i]);
        }
        return result;
    }

    public static wyc.WeaponSource XYYWeaponToWYCWeaponSource(xyy.Weapon xyyWeapon)
    {
        wyc.WeaponSource target;
        return null;
    }
    public static xyy.Weapon WYCWeaponSourceToXYYWeapon(wyc.WeaponSource wycWeapon)
    {
        xyy.Weapon target=null;
        string temp = "";
        //从特性列表中加载内容
        switch (wycWeapon.weaponType)
        {
            case (int)wyc.WEAPON_TYPE.LONG_RANGE:
                target = new xyy.CloseCombt(wycWeapon.price,wycWeapon.damage, wycWeapon.wanted, temp);
                break;
            case (int)wyc.WEAPON_TYPE.MELEE:
                target = new xyy.RangedAttack(wycWeapon.price, wycWeapon.damage, wycWeapon.wanted, temp);
                break;

        }
        target.setID(wycWeapon.orderNumber);
        return target;
    }
    public static wyc.EquipSource XYYEquipToWYCEquipSource(xyy.Refitting xyyEquip)
    {
        wyc.EquipSource target;
        return null;
    }
    public static xyy.Refitting WYCEquipSourceToXYYWeapon(wyc.EquipSource wycEquip)
    {
        xyy.Refitting target=null;
        //public xyy.Refitting[] refittings ={new xyy.Tire(10000,1,"null")  ,
        //                            new xyy.Braking(10000,1,"null") ,
        //                            new xyy.Engine(20000,1,"null") ,
        //                            new xyy.FireSysytem(10000,1,"null") ,
        //                            new xyy.Suspension(10000,1,"null") ,
        //                            new xyy.OilTank(10000,1,"null"),
        //                            new xyy.SpeedUp(5000,1,"null")  };
        string temp="";
        //从特性列表中加载内容
        switch (wycEquip.equipType)
        {
            case (int)wyc.EQUIP_TYPE.BRAKE:
                target = new xyy.Braking(wycEquip.price, wycEquip.effectLevel, temp);
                break;
            case (int)wyc.EQUIP_TYPE.CHASSIS:
                target = new xyy.Suspension(wycEquip.price, wycEquip.effectLevel, temp);
                break;
            case (int)wyc.EQUIP_TYPE.CONSUME:
                target = new xyy.SpeedUp(wycEquip.price, wycEquip.effectLevel, temp);
                break;
            case (int)wyc.EQUIP_TYPE.ENGINE:
                target = new xyy.Engine(wycEquip.price, wycEquip.effectLevel, temp);
                break;
            case (int)wyc.EQUIP_TYPE.IGNITION:
                target = new xyy.FireSysytem(wycEquip.price, wycEquip.effectLevel, temp);
                break;
            case (int)wyc.EQUIP_TYPE.TIRE:
                target = new xyy.Tire(wycEquip.price, wycEquip.effectLevel, temp);
                break;
        }
        target.setID(wycEquip.orderNumber);
        return target;
    }
}

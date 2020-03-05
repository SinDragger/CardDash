using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;

namespace xyy
{
    public class WeaponInit : MonoBehaviour
    {
        public Weapon[] weapons;//全部列表
        public static WeaponInit singleton = null;
        public GameObject weaponCardsPrefab;
        //  public  GameObject ShelterPanel;
        public static List<GameObject> weaponCards;//Prefabs?
        public static int actualCapacity;
        [SerializeField]
        Vector3 startPosition;
        [SerializeField]
        GameObject father;
        [SerializeField]
        int cardsDistance;//间隔 x direction
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

            //InitTools.renewInformation(weaponCards,startPosition,cardsDistance);
            //InitTools.RenewSliderSingleMove(actualCapacity ,this,cardsDistance);
        }
        void Start()
        {
            weaponCards = new List<GameObject>();
            Debug.Log(CEButtons.singleton.weapons.Length);
            weapons = CEButtons.singleton.weapons;
            //weapons = GameManager.instance.weapons;
            Debug.Log(CEButtons.singleton.weapons.Length);
            Debug.Log(weapons.Length);
            actualCapacity = weapons.Length;
            for (int i = 0; i < weapons.Length; i++)
            {
                var clone = Instantiate(weaponCardsPrefab, Vector3.zero, weaponCardsPrefab.transform.rotation, father.transform);


                //clone.GetComponent<RectTransform>().anchoredPosition3D=startPosition+new Vector3(cardsDistance,0,0)*i;

                clone.GetComponent<EquipmentCards>().SetE_CardType(cardsType.Weaponcards);
                clone.GetComponent<EquipmentCards>().SetWeapon(weapons[i]);


                string s = "";
                foreach (var w in DataController.instance.weaponSources)
                {
                    if (w.orderNumber == weapons[i].Id)
                    {
                        s = w.name;
                        break;
                    }
                }
                Sprite temp = (Sprite)Resources.Load("Pictures/Weapon/" + s, typeof(Sprite));
                clone.GetComponent<UnityEngine.UI.Image>().sprite = temp;


                //clone.GetComponent<UnityEngine.UI.Image>().color = new Color(i / weapons.Length * 1, (weapons.Length - i) / weapons.Length * 1, Random.Range(0.0f, 1.0f));
                //*Need Fix
                if (CEButtons.singleton.ShelterPanel != null)
                {
                    //Debug.Log(CEButtons.singleton .ShelterPanel);
                    clone.GetComponent<Drag>().shelterPanel = CEButtons.singleton.ShelterPanel;
                }
                weaponCards.Add(clone);

            }
            // InitTools.renewIndexValue(weaponCards);
            renewCards();
        }
        public void renewCards()
        {//留给外部用来更新卡片
            InitTools.renewInformation(weaponCards, startPosition, cardsDistance);
            InitTools.RenewSliderSingleMove(actualCapacity, this, cardsDistance);
        }

    }
}

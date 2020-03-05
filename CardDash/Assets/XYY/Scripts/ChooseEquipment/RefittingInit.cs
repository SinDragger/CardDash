using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;
namespace xyy{

public class RefittingInit : MonoBehaviour
{
   // public  GameObject ShelterPanel;
  public  Refitting[] refittings;
     public static RefittingInit singleton=null;
  public GameObject refittingCardsPrefab;
   public static List<GameObject> refittingCards;//Prefabs?
   public static int actualCapacity;
   [SerializeField]
   Vector3 startPosition;
   [SerializeField]
   GameObject father;
   [SerializeField]
  int  cardsDistance;//间隔 x direction
    void Awake(){
       if(singleton==null){//单例模式
           singleton=this;}
        else  if(singleton!=this){
            Destroy(gameObject);
        };
    
  
       // InitTools.renewInformation(refittingCards ,startPosition,cardsDistance);
        //InitTools.RenewSliderSingleMove(actualCapacity ,this,cardsDistance);
         // InitTools.renewIndexValue(refittingCards);
  }
  void Start()
        {
            refittingCards = new List<GameObject>();
            //refittings=GameManager.instance.refittings;
            refittings = CEButtons.singleton.refittings;
            actualCapacity = refittings.Length;
            Debug.Log(actualCapacity);
            for (int i = 0; i < refittings.Length; i++)
            {
                var clone = Instantiate(refittingCardsPrefab, Vector3.zero, refittingCardsPrefab.transform.rotation, father.transform);
                //clone.GetComponent<RectTransform>().anchoredPosition3D=startPosition+new Vector3(cardsDistance,0,0)*i;

                clone.GetComponent<EquipmentCards>().SetE_CardType(cardsType.Refittingcards);
                clone.GetComponent<EquipmentCards>().SetRefitting(refittings[i]);
                string s="";
                foreach (var e in DataController.instance.equipSources)
                {
                    if (e.orderNumber == refittings[i].Id) {
                        s = e.name;
                        break;
                    }
                }
                Sprite temp = (Sprite)Resources.Load("Pictures/Equip/" + s, typeof(Sprite));
                //clone.GetComponent<UnityEngine.UI.Image>().color = new Color(i / refittings.Length * 1, (refittings.Length - i) / refittings.Length * 1, Random.Range(0.0f, 1.0f));
                clone.GetComponent<UnityEngine.UI.Image>().sprite = temp;

                clone.GetComponent<Drag>().shelterPanel = CEButtons.singleton.ShelterPanel;
                refittingCards.Add(clone);

            }
            renewCards();
  }
   public  void renewCards(){//留给外部用来更新卡片
    InitTools.renewInformation(refittingCards,startPosition,cardsDistance);
        InitTools.RenewSliderSingleMove(actualCapacity ,this,cardsDistance);
  }
 
}
}


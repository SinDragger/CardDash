using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace xyy
{
    public class CCButtons : MonoBehaviour
    {
        public GameObject chatting;//聊天记录
        public GameObject timeCount;//倒计时
        private Vector3 hideDistance;
        private bool hideornot;
        public Button[] chooseButtons;//上面小的选择按钮
        public GameObject characterImage;//界面上的 主人物图片
        public wyc.PlayerCard thugImage;//界面上的 主人物图片
        public Text[] characterValue;//人物属性展示
        public Text[] characterValueRank;//人物属性排名
        private int nowCharacterId;//现在角色ID 传给GM的
        private const int characterNumber = 3;//手动指定总人物数量? 
        private Character[] characters = new Character[characterNumber];//GM传过来的如今角色 数量手动确定？
        private List<wyc.ThugSource> thugs;//GM传过来的如今角色 数量手动确定？
        private Sprite[] characterPictures = new Sprite[characterNumber];//人物图像 也应该GM确定 数量手动确定？ 
        void Awake()
        {
            hideDistance = new Vector3(300, 0, 0);
            hideornot = false;
            TimeCounting();


            thugs = new List<wyc.ThugSource>();
            characterPictures = GameManager.instance.characterPictures;//是直接用呢 还是拷贝一份呢？
            characters = GameManager.instance.characters;
            thugs = wyc.GameController.instance.ChooseAblePlayers(characterNumber);
            //nowCharacterId=GameManager.instance.mainCharacterId;
            nowCharacterId = 0;

            RenewThugCharacterImage();
        }
        #region hidebutton
        public void HideButton()
        {
            if (hideornot)
            {
                Hide();
            }
            else
            {
                DeleteHide();
            }
        }
        private void Hide()
        {
            chatting.GetComponent<RectTransform>().anchoredPosition3D += hideDistance;
            hideornot = false;
        }
        private void DeleteHide()
        {
            chatting.GetComponent<RectTransform>().anchoredPosition3D -= hideDistance;
            hideornot = true;
        }
        #endregion
        #region exit
        public void Exit()
        {
            Debug.Log("This is Exit Menu");
        }
        #endregion
        #region timecounting

        public void TimeCounting()
        {
            InvokeRepeating("TimeDecrease", 0.1f, 1.0f);
        }

        private void TimeDecrease()
        {
            timeCount.GetComponent<Text>().text = (int.Parse(timeCount.GetComponent<Text>().text) - 1).ToString();

            if (timeCount.GetComponent<Text>().text == "0")
            {
                GameManager.instance.mainCharacterId = nowCharacterId;
                SceneManager.LoadScene("ChooseEquipment");

            }
        }
        #endregion
        #region renewImage

        public void changeThugCharacter(int i)
        {
            nowCharacterId += i;
            nowCharacterId = nowCharacterId % thugs.Count;
            if (nowCharacterId < 0)
            {
                nowCharacterId += thugs.Count;
            }
            RenewThugCharacterImage();
        }
        public void plusOneCharacter()
        {
            nowCharacterId++;
            RenewCharacterImage();
        }
        public void plusDoubleCharacter()
        {
            nowCharacterId += 2;
            RenewCharacterImage();
        }
        public void minusOneCharacter()
        {
            nowCharacterId--;
            RenewCharacterImage();
        }
        public void minusDoubleCharacter()
        {
            nowCharacterId -= 2;
            Debug.Log("???");
            RenewCharacterImage();
        }
        private delegate bool ReturnRank(int i, int j, Character[] mycharacters);

        ReturnRank healthRank = delegate (int i, int j, Character[] mycharacters) { return mycharacters[i].health > mycharacters[j].health; };
        ReturnRank MoneyRank = delegate (int i, int j, Character[] mycharacters) { return mycharacters[i].money > mycharacters[j].money; };
        ReturnRank PowerRank = delegate (int i, int j, Character[] mycharacters) { return mycharacters[i].damage > mycharacters[j].damage; };
        ReturnRank DriveRank = delegate (int i, int j, Character[] mycharacters) { return mycharacters[i].driveAbility > mycharacters[j].driveAbility; };
        int CharacterRank(int id, ReturnRank tempRank)
        {
            int rank = 1;
            for (int i = 0; i < characterNumber; i++)
            {
                if (tempRank(i, id, characters))
                {
                    rank++;
                }
                else
                {
                    break;
                }
            }
            return rank;
        }
        private delegate bool ReturnThugRank(int i, int j, List<wyc.ThugSource> t);

        ReturnThugRank thugHealthRank = delegate (int i, int j, List<wyc.ThugSource> t) { return t[i].health > t[j].health; };
        ReturnThugRank thugWealthRank = delegate (int i, int j, List<wyc.ThugSource> t) { return t[i].wealth > t[j].wealth; };
        ReturnThugRank thugStrenthRank = delegate (int i, int j, List<wyc.ThugSource> t) { return t[i].strength > t[j].strength; };
        ReturnThugRank thugDriveRank = delegate (int i, int j, List<wyc.ThugSource> t) { return t[i].drive > t[j].drive; };
        private int ThugCharacterRank(int id, ReturnThugRank tempRank)
        {
            int rank = 1;
            for (int i = 0; i < characterNumber; i++)
            {
                if (i == id) continue;
                if (tempRank(i, id, thugs))
                {
                    rank++;
                }
            }
            return rank;
        }
        private void RenewCharacterText(int j)
        {
            characterValue[0].text = characters[j].characterName.ToString();
            characterValue[1].text = characters[j].health.ToString();
            characterValue[2].text = characters[j].money.ToString();
            characterValue[3].text = characters[j].damage.ToString();
            characterValue[4].text = characters[j].driveAbility.ToString();
            characterValue[5].text = characters[j].specialAbility.ToString();
            characterValueRank[0].text = CharacterRank(j, healthRank).ToString();
            characterValueRank[1].text = CharacterRank(j, MoneyRank).ToString();
            characterValueRank[2].text = CharacterRank(j, PowerRank).ToString();
            characterValueRank[3].text = CharacterRank(j, DriveRank).ToString();
        }

        private void RenewThugCharacterText(int j)
        {
            characterValue[0].text = thugs[j].name;
            characterValue[1].text = thugs[j].health.ToString();
            characterValue[2].text = thugs[j].wealth.ToString()+",000";
            characterValue[3].text = thugs[j].strength.ToString();
            characterValue[4].text = thugs[j].drive.ToString();
            string temp="";
            if (thugs[j].characteristics.Count > 0)
            {
                temp += wyc.DataController.instance.GetCharacter(thugs[j].characteristics[0]).name;
                for(int i=1;i< thugs[j].characteristics.Count; i++)
                {
                    temp += "、";
                    temp += wyc.DataController.instance.GetCharacter(thugs[j].characteristics[i]).name;
                }
            }
            //characterValue[5].text = thugs[j].specialAbility.ToString();载入特性内容
            characterValue[5].text = temp;


            characterValueRank[0].text = ThugCharacterRank(j, thugHealthRank).ToString();
            characterValueRank[1].text = ThugCharacterRank(j, thugWealthRank).ToString();
            characterValueRank[2].text = ThugCharacterRank(j, thugStrenthRank).ToString();
            characterValueRank[3].text = ThugCharacterRank(j, thugDriveRank).ToString();
        }


        private void RenewCharacterImage()
        {
            if (nowCharacterId < 0)
            {
                nowCharacterId += characterNumber;
            }
            else if (nowCharacterId > characterNumber - 1)
            {
                nowCharacterId -= characterNumber;
            }
            characterImage.GetComponent<Image>().sprite = characterPictures[nowCharacterId];
            for (int i = 0; i != 5; i++)
            {
                int j = nowCharacterId - 2 + i;//最左测的按钮  6 7 0 1 2   7 0 1 2 3  4 5 6 7 0  5 6 7 0 1
                if (j < 0)
                {
                    j += characterNumber;
                }
                else if (j > 7)
                {
                    j -= characterNumber;
                }
                else
                {
                    j = nowCharacterId - 2 + i;
                }
                chooseButtons[i].GetComponent<Image>().sprite = characterPictures[j];
                RenewCharacterText(j);
                //RenewThugCharacterImage();
            }

        }

        private void RenewThugCharacterImage()
        {
            if (thugImage != null)
            {
                thugImage.UpdateData(thugs[nowCharacterId]);
            }
            for(int i = 0; i < 3; i++)
            {
                int j = i-1+ nowCharacterId;

                if (j < 0) j += 3;
                if (j > 2) j -= 3;

                Sprite temp = (Sprite)Resources.Load("Pictures/Thug/" + thugs[j].name, typeof(Sprite));

                chooseButtons[i].GetComponent<Image>().sprite = temp;
            }
            RenewThugCharacterText(nowCharacterId);

            //for (int i = 0; i != 5; i++)
            //{
            //    int j = nowCharacterId - 2 + i;//最左测的按钮  6 7 0 1 2   7 0 1 2 3  4 5 6 7 0  5 6 7 0 1
            //    if (j < 0)
            //    {
            //        j += characterNumber;
            //    }
            //    else if (j > 7)
            //    {
            //        j -= characterNumber;
            //    }
            //    else
            //    {
            //        j = nowCharacterId - 2 + i;
            //    }
            //    chooseButtons[i].GetComponent<Image>().sprite = characterPictures[j];
            //    RenewCharacterText(j);
            //}

        }
        #endregion
        #region NextScene
        public void Confirm()
        {
            Debug.Log("this is next Scene");
            GameManager.instance.mainCharacterId = nowCharacterId;
            //SceneManager.LoadScene("ChooseEquipment");
            wyc.GameController.instance.EnemyPoolRemovePlayer(thugs[nowCharacterId].orderNumber);
            wyc.GameController.instance.playerThugId= thugs[nowCharacterId].orderNumber;
            //等待其他玩家
            wyc.GameController.instance.GameScenceChange(2);
        }
        #endregion
    }
}

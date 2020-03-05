using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 namespace xyy
{
    public class StartButtons : MonoBehaviour
    {
        public void StartGame()
        {
            Debug.Log("this is StartGame");
            GameManager.instance.GameState = 1;//1 为ChooseCharacter;
            //wyc.GameController.instance.ChooseAblePlayers(3);
            wyc.GameController.instance.GameScenceChange(1);
        }
        public void ContinueGame()
        {
            Debug.Log("this is Continue");
        }
        public void SaveGame()
        {
            Debug.Log("this is SaveGame");
        }
        public void Practise()
        {
            Debug.Log("Practise");
        }
        public void LoadGame()
        {
            Debug.Log("this is LoadGame");
        }
        public void Settings()
        {
            Debug.Log("this is Settings");
        }
        public void DevelopmentList()
        {
            Debug.Log("this is DevelopmentList");
        }
    }
}
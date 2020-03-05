using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    Ray ray;
    LayerMask cardSortLayer=1<<9;
    RaycastHit hit;

    void Update(){
     Ray cameraRay=Camera.main.ScreenPointToRay(Input .mousePosition);
     
    
      //if(Input.GetMouseButtonDown(0)){
  if(Physics.Raycast(cameraRay,out hit,float.MaxValue,cardSortLayer) ){
        Debug.Log(hit.transform .name);
    }
    else{
        Debug.Log("??");
        
    }
   //   }
      




    }
    
}

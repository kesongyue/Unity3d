using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    SSDirector director;
   
    void Start()
    {
        director = SSDirector.GetInstance() ;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Devil" || hit.transform.tag == "Priest")
                {
                   
                    director.genGameObjects.RoleMove(hit.collider.gameObject);
                }
                else if (hit.transform.tag == "Boat")
                {
                    director.genGameObjects.moveBoat();
                    
                }
            }
        }
    }

    private void OnGUI()
    {
        if (director.genGameObjects.GetGameState() == GenGameObjects.GameState.WIN)
        {
            GUI.Label(new Rect(Screen.width / 3, Screen.height / 3, 200, 200), "WIN");
        }
        else if(director.genGameObjects.GetGameState() == GenGameObjects.GameState.FAILED)
        {
            GUI.Label(new Rect(Screen.width / 3, Screen.height / 3, 200, 200), "YOU LOSE");
        }
    }

}


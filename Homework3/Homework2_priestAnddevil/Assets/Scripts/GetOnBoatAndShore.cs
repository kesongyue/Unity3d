using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoatAndShore : SSAction
{

    public GenGameObjects gameObjects = SSDirector.GetInstance().genGameObjects;
    GameObject role;
    public static GetOnBoatAndShore GetSSAction(GameObject role)
    {
        GetOnBoatAndShore action = ScriptableObject.CreateInstance<GetOnBoatAndShore>();
        action.role = role;
        return action;
    }
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        if (role!=null && role.transform.position.z * gameObjects.boatSign > 0)
        {
            return;
        }
        if (role != null && role.transform.parent != null)
        {
            getOnShore(role);
        }
        else if (gameObjects.boatCapcity() > 0)
        {
            getOnBoat(role);
        }
    }

    void getOnBoat(GameObject person)
    {

        if (gameObjects.boatCapcity() >= 0)
        {
            gameObjects.boat_capacity--;
            person.transform.parent = gameObjects.boat.transform;
            if (gameObjects.ObjectOnBoat[0] == null)
            {
                gameObjects.ObjectOnBoat[0] = person;
                gameObjects.positionOfObjectOnBoat[0] = person.transform.position;
                person.transform.localPosition = new Vector3(0, 0.8f, -0.3f);
            }
            else
            {
                gameObjects.ObjectOnBoat[1] = person;
                gameObjects.positionOfObjectOnBoat[1] = person.transform.position;
                person.transform.localPosition = new Vector3(0, 0.8f, 0.3f);
            }

            if (gameObjects.boatSign == 1)
            {
                if (person.tag == "Priest")
                {
                    gameObjects.PriestsOnStartNumbers--;
                    gameObjects.PriestsOnBoatNumbers++;
                }
                else
                {
                    gameObjects.DevilsOnStartNumbers--;
                    gameObjects.DevilsOnBoatNumbers++;
                }
            }
            else if (gameObjects.boatSign == -1)
            {
                if (person.tag == "Priest")
                {
                    gameObjects.PriestsOnEndNumbers--;
                    gameObjects.PriestsOnBoatNumbers++;
                }
                else
                {
                    gameObjects.DevilsOnEndNumbers--;
                    gameObjects.DevilsOnBoatNumbers++;
                }
            }
        }
        else
        {
            Debug.Log("full");
        }

    }

    public void getOnShore(GameObject person)
    {
        gameObjects.boat_capacity++;
        for (int side = 0; side < 2; side++)
        {
            if (gameObjects.ObjectOnBoat[side] == person)
            {
                gameObjects.ObjectOnBoat[side].transform.parent = null;
                float z = Math.Abs(gameObjects.positionOfObjectOnBoat[side].z);
                // if(boatSign > 0)
                gameObjects.positionOfObjectOnBoat[side].z = z * -gameObjects.boatSign;

                gameObjects.ObjectOnBoat[side].transform.position = gameObjects.positionOfObjectOnBoat[side];
                if (gameObjects.boatSign == 1)
                {
                    if (gameObjects.ObjectOnBoat[side].tag == "Priest")
                    {
                        gameObjects.PriestsOnStartNumbers++;
                        gameObjects.PriestsOnBoatNumbers--;
                    }
                    else
                    {
                        gameObjects.DevilsOnStartNumbers++;
                        gameObjects.DevilsOnBoatNumbers--;
                    }
                }
                else if (gameObjects.boatSign == -1)
                {
                    if (gameObjects.ObjectOnBoat[side].tag == "Priest")
                    {
                        gameObjects.PriestsOnEndNumbers++;
                        gameObjects.PriestsOnBoatNumbers--;
                    }
                    else
                    {
                        gameObjects.DevilsOnEndNumbers++;
                        gameObjects.DevilsOnBoatNumbers--;
                    }
                }
                gameObjects.ObjectOnBoat[side] = null;
                break;
            }
        }
        gameObjects.check();
        Debug.Log("Priest:" + gameObjects.PriestsOnEndNumbers + "  Devils: " + gameObjects.DevilsOnEndNumbers);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenGameObjects : MonoBehaviour, SceneController
{
    public CCActionManager actionManager { get; set; }
    public enum GameState{STOP ,DOING,WIN,FAILED};
    public GameState gameState = GameState.STOP;
    public int PriestsOnStartNumbers = 3;
    public int DevilsOnStartNumbers = 3;
    public int PriestsOnEndNumbers = 0;
    public int DevilsOnEndNumbers = 0;
    public int PriestsOnBoatNumbers = 0;
    public int DevilsOnBoatNumbers = 0;

    public GameObject shoreLeft;
    public GameObject shoreRight;
    public int boatSign =1;  //1 -->Left -1 --> Right
    public int boat_capacity;
    public Vector3 shoreLeftPosition = new Vector3(-8, 1, -8f);
    public Vector3 shoreRightPosition = new Vector3(-8, 1, 8f);
    public Vector3 boatPositionLeft = new Vector3(-8, 0.5f, -4);
    public Vector3 boatPositionRight = new Vector3(-8, 0.5f, 4);
    public Vector3[] positionOfObjectOnBoat=new Vector3[2] { new Vector3(0, 0.8f, -0.3f), new Vector3(0, 0.8f, 0.3f) };

    public Vector3[] RolePositions = new Vector3[] {new Vector3(-8,1.8f,-5.5f), new Vector3(-8,1.8f,-6f), new Vector3(-8,1.8f,-6.5f),
                new Vector3(-8,1.8f,-7f), new Vector3(-8,1.8f,-8f), new Vector3(-8,1.8f,-9f)};
    public GameObject boat;
    public GameObject[] devils = new GameObject[3];
    public GameObject[] priests = new GameObject[3];

    public GameObject[] ObjectOnBoat = new GameObject[2];
    float speed = 2f;
    public void LoadResources()
    {
        shoreLeft=Instantiate(Resources.Load("Prefabs/Coast"), shoreLeftPosition, Quaternion.identity) as GameObject;
        shoreRight=Instantiate(Resources.Load("Prefabs/Coast"), shoreRightPosition, Quaternion.identity) as GameObject;

        boat = Instantiate(Resources.Load("Prefabs/Boat"), boatPositionLeft, Quaternion.identity) as GameObject;

        for (int i = 0; i < 3; i++)
        {
            priests[i] = Instantiate(Resources.Load("Prefabs/Priest"), RolePositions[i], Quaternion.identity) as GameObject;
        }
        for (int i = 3; i < 6; i++)
        {
            devils[i - 3] = Instantiate(Resources.Load("Prefabs/Devil"), RolePositions[i], Quaternion.identity) as GameObject;
        }
    }
    public void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.genGameObjects = this;
        LoadResources();
        boat_capacity = 2;
    }

    public int boatCapcity()
    {
        return boat_capacity;
    }
    public Vector3 getShoreLeft()
    {
        return shoreLeftPosition;
    }
    public Vector3 getShoreRight()
    {
        return shoreRightPosition;
    }
    void getOnBoat(GameObject person)
    {
      
        if (boatCapcity() >= 0)
        {
            boat_capacity--;
            person.transform.parent = boat.transform;
            if (ObjectOnBoat[0] == null)
            {
                ObjectOnBoat[0] = person;
                positionOfObjectOnBoat[0] = person.transform.position;
                person.transform.localPosition = new Vector3(0, 0.8f, -0.3f);
            }
            else
            {
                ObjectOnBoat[1] = person;
                positionOfObjectOnBoat[1] = person.transform.position;
                person.transform.localPosition = new Vector3(0, 0.8f, 0.3f);
            }
           
            if (boatSign==1)
            {
                if (person.tag == "Priest")
                {
                    PriestsOnStartNumbers--;
                    PriestsOnBoatNumbers++;
                }
                else
                {
                    DevilsOnStartNumbers--;
                    DevilsOnBoatNumbers++;
                }
            }
            else if(boatSign==-1)
            {
                if (person.tag == "Priest")
                {
                    PriestsOnEndNumbers--;
                    PriestsOnBoatNumbers++;
                }
                else
                {
                    DevilsOnEndNumbers--;
                    DevilsOnBoatNumbers++;
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
        boat_capacity++;
        for(int side = 0; side < 2; side++)
        {
            if (ObjectOnBoat[side] == person)
            {
                ObjectOnBoat[side].transform.parent = null;
                float z = Math.Abs(positionOfObjectOnBoat[side].z);
               // if(boatSign > 0)
                    positionOfObjectOnBoat[side].z = z * -boatSign;

                ObjectOnBoat[side].transform.position = positionOfObjectOnBoat[side];
                if (boatSign == 1)
                {
                    if (ObjectOnBoat[side].tag == "Priest")
                    {
                        PriestsOnStartNumbers++;
                        PriestsOnBoatNumbers-- ;
                    }
                    else
                    {
                        DevilsOnStartNumbers++;
                        DevilsOnBoatNumbers--;
                    }
                }
                else if (boatSign == -1)
                {
                    if (ObjectOnBoat[side].tag == "Priest")
                    {
                        PriestsOnEndNumbers++;
                        PriestsOnBoatNumbers--;
                    }
                    else
                    {
                        DevilsOnEndNumbers++;
                        DevilsOnBoatNumbers--;
                    }
                }
                ObjectOnBoat[side] = null;
                break;
            }
        }
        check();
        Debug.Log("Priest:" + PriestsOnEndNumbers + "  Devils: " + DevilsOnEndNumbers);
    }
    public GameState GetGameState()
    {
        return gameState;
    }

    public void moveBoat()
    {
       /* if(boatCapcity() <2)
        {
            if(boatSign == 1)
            {
                boat.transform.position = boatPositionRight;
            }
            else
            {
                boat.transform.position = boatPositionLeft;
            }
            boatSign = -boatSign;
        }
        check();*/
    }
    public void RoleMove(GameObject role)
    {
       /* if(role.transform.position.z * boatSign > 0)
        {
            return;
        }
        if(role.transform.parent != null)
        {
            getOnShore(role);
        }
        else if(boatCapcity()>0)
        {
            getOnBoat(role);
        }*/
    }
    public void check()
    {
        int Priests_total_left=0,Priests_total_right=0,Devils_total_left=0,Devils_total_right=0;
        if(boatSign == 1) // if boat is on the left;
        {
            Priests_total_left = PriestsOnStartNumbers + PriestsOnBoatNumbers;
            Priests_total_right = PriestsOnEndNumbers;
            Devils_total_left = DevilsOnBoatNumbers + DevilsOnStartNumbers;
            Devils_total_right = DevilsOnEndNumbers;
        }
        else if(boatSign == -1)  // if boat is on the right;
        {
            Priests_total_left = PriestsOnStartNumbers;
            Priests_total_right = PriestsOnEndNumbers + PriestsOnBoatNumbers;
            Devils_total_left = DevilsOnStartNumbers;
            Devils_total_right = DevilsOnEndNumbers+ DevilsOnBoatNumbers;
        }

        if(PriestsOnEndNumbers ==3 && DevilsOnEndNumbers == 3)
        {
            gameState = GameState.WIN;
            Debug.Log("youwin");
        }
        else if(Priests_total_left != 0 && Priests_total_right != 0 &&(Priests_total_left < Devils_total_left || Priests_total_right < Devils_total_right))
        {
            gameState = GameState.FAILED;
            Debug.Log("youlose");
        }
    }

}




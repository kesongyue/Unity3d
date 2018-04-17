using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory{

    private static DiskFactory _instance;
    private static List<GameObject> unusedDiskList = new List<GameObject>();
    private static List<GameObject> usedDiskList = new List<GameObject>();
    public GameObject diskTemplate;
    public static DiskFactory getInstance()
    {
        if(_instance == null)
        {
            _instance = new DiskFactory();
        }
        return _instance;
    }
    private DiskFactory() { }
    public GameObject getDiskObject()
    {
        GameObject disk1;
        if (unusedDiskList.Count == 0)
        {
            disk1 = GameObject.Instantiate(diskTemplate) as GameObject;
            usedDiskList.Add(disk1);
            
        }
        else
        {
            disk1 = unusedDiskList[0];
            unusedDiskList.RemoveAt(0);
            usedDiskList.Add(disk1);
        }
        disk1.GetComponent<GameModel>().setState(true);
        return disk1;
        
    }

    public void removeDiskObject(GameObject obj)
    {
        if (usedDiskList.Count > 0)
        {
            GameObject disk1 = obj;
            disk1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            disk1.GetComponent<GameModel>().setState(false);
            usedDiskList.Remove(obj);   
            unusedDiskList.Add(disk1);
        }
    }
	
    //返回出界的disk的数目
    public int updateList()
    {
        int count = 0;
        for(int i = 0; i < usedDiskList.Count; i++)
        {
            if(usedDiskList[i].GetComponent<GameModel>().is_outOfEdge())
            {
                removeDiskObject(usedDiskList[i]);
                count++;
            }
        }
        return count;
    }

    public void clear()
    {
        usedDiskList.Clear();
        unusedDiskList.Clear();
    }
}

public class DiskFactoryBC : MonoBehaviour
{
    public GameObject disk;
    private void Awake()
    {
        DiskFactory.getInstance().diskTemplate = disk;
    }
}
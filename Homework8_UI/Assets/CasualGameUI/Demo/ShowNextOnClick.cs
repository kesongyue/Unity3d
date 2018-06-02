using UnityEngine;
using System.Collections;

public class ShowNextOnClick : MonoBehaviour
{
    public GameObject toHide;
    public GameObject next;

    public void OnClick()
    {
        toHide.SetActive(false);
        next.SetActive(true);
    }
}
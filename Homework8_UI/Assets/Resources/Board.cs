using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    private Button btn;
    public Text text;
    private float text_height;
    //协程循环的次数
    private int frame = 20;

    private int timer = 0;

    // Use this for initialization  
    void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        text_height = text.rectTransform.sizeDelta.y;
    }

    void OnClick()
    {
        if (text.gameObject.activeSelf)
        {
            StartCoroutine(rotateIn());
        }
        else
        {
            StartCoroutine(rotateOut());
        }

    }

    IEnumerator rotateIn()
    {
        float rotatex = 0;
       
        float height = text_height;
        for (int i = 0; i < frame; i++)
        {
            rotatex -= 90f / frame;
            height -= text_height / frame;
            // 以下两行是为了让Text的收回有一个渐变过程
            text.transform.rotation = Quaternion.Euler(rotatex, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, height);
            // 保存现存变量的值，等到下一帧继续执行
            yield return null;
        }
        text.gameObject.SetActive(false);
    }

    IEnumerator rotateOut()
    {
        float rotatex = -90;
        float xy = 0;
        text.gameObject.SetActive(true);
        for (int i = 0; i < frame; i++)
        {
            rotatex += 90f / frame;
            xy += text_height / frame;
            text.transform.rotation = Quaternion.Euler(rotatex, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);
            // 保存现存变量的值，等到下一帧继续执行
            yield return null;
        }
        
    }

}
using UnityEngine;
using UnityEngine.UI;

public class DemoToggle : MonoBehaviour
{
    private Toggle _toggle;
    public GameObject on;
    public GameObject off;

    public void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(state =>
        {
            if (state)
            {
                on.SetActive(true);
                off.SetActive(false);
            }
            else
            {
                on.SetActive(false);
                off.SetActive(true);
            }
        });
    }
}
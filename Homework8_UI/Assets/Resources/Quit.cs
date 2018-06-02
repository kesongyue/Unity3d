using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Quit : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(QuitManager);
    }
    public void QuitManager()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
		Application.Quit();
        #endif
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainChatcontainer : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainChatcontainer instance { get; private set; } = null;
    public GameObject chatPanel;
    public GameObject[] AvatarIcons;

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        chatPanel.SetActive(false);
    }

    // Update is called once per frame
    /*public void OnPointerClick(PointerEventData eventData)
    {
        chatPanel.SetActive(true);
        foreach (GameObject obj in AvatarIcons)
        {
            obj.SetActive(false);
        }

    }*/
}

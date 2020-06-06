using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectIcons : MonoBehaviour, IPointerClickHandler
{
    public GameObject ChatPanel;
    // Start is called before the first frame update



    public void OnPointerClick(PointerEventData eventData)
    {
        ChatPanel.SetActive(true);
        foreach (GameObject obj in MainChatcontainer.instance.AvatarIcons)
        {
            obj.SetActive(false);
        }

    }

}

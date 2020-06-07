using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ChatButtonPress.instance.isChatconfActive)
            ChatConfPanel.instance.animator.SetTrigger("enterButtExit");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ChatButtonPress.instance.isChatconfActive)
            ChatConfPanel.instance.animator.SetTrigger("exitButtExit");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ChatConfPanel.instance.animator.ResetTrigger("enterButtExit");
        ChatConfPanel.instance.animator.ResetTrigger("exitButtExit");
        ChatConfPanel.instance.animator.SetTrigger("clickExitButton");
        ChatButtonPress.instance.isChatconfActive = false;
    }

}

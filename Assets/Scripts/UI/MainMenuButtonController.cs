using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonController : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        //MainMenuController.instance.animator.SetTrigger("ButtonMainMenuEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //MainMenuController.instance.animator.SetTrigger("ButtonMainMenuExit");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        MainMenuController.instance.animator.SetTrigger("ButtonMainMenuClick");
        //MainMenuController.instance.animator.SetTrigger("chatConnectorShow");

    }

}

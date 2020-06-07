using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonController1 : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        MainMenuController.instance.animator.SetTrigger("UpButtonMainMenuEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MainMenuController.instance.animator.SetTrigger("UpButtonMainMenuExit");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        MainMenuController.instance.animator.SetTrigger("UpButtonMainMenuClick");
    }

}

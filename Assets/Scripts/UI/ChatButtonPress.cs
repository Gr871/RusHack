using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChatButtonPress : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    // Start is called before the first frame update
    public GameObject Buttoncontainer;
    private bool isChatconfActive = false;

    public Animator animator;

    
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("click");
        if (!isChatconfActive)
        {
            ChatConfPanel.instance.SpawnConfPanel();
            isChatconfActive = true;
        }

        else
        {
            ChatConfPanel.instance.HideConfPanel();
            isChatconfActive = false;
        }
    }

    

}

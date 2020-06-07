using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PublicChatController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public Animator animator;
    
    
    
    
    
    void Start()
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
    }

    // Update is called once per frame

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChatButtonPress : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    // Start is called before the first frame update
    public GameObject Buttoncontainer;
    public static ChatButtonPress instance { get; private set; } = null;
    public bool isChatconfActive = false;

    public Animator animator;

    
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isChatconfActive)
            animator.SetTrigger("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetExtit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (!isChatconfActive)
        {
            animator.SetTrigger("click");
            ChatConfPanel.instance.SpawnConfPanel();
            isChatconfActive = true;
        }
    }
    

    public void SetExtit()
    {
        if (!isChatconfActive)
            animator.SetTrigger("exit");
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatConfPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chatConfPanel;
    public static ChatConfPanel instance { get; private set; } = null;

    public Animator animator;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public void SpawnConfPanel()
    {
        animator.SetTrigger("ConfChatPanelShow");
    }
    public void HideConfPanel()
    {
        animator.SetTrigger("ConfChatPanelHide");
    }


}

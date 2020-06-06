using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatField : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chatField;
    public Animator animator;


    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Spawn");
    }
    public void ClickSendMessage()
    {
        animator.SetTrigger("ChatClick");
    }
}

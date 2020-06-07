using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public static MainMenuController instance { get; private set; }

    public void Awake()
    {
        instance = this;
    }
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void MainMenuSpawn()
    {
        animator.SetTrigger("SpawnMainPanel");
    }
    // Update is called once per frame
    public void ChatConnectorShow()
    {
        animator.SetTrigger("chatConnectorShow");
    }


    public void ChatConnectorUpShow()
    {
        animator.SetTrigger("ChatConnectorShow2");
    }
}

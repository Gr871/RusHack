using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HelpPanel;
    public Animator animator;
    private bool isShowed = false;
    //[SerializeField] private Sprite background;

    public void Start()
    {
        animator = HelpPanel.GetComponent<Animator>();
    }

    public void ChangePriority(int priority)
    {
        HelpPanel.GetComponent<Canvas>().sortingOrder = priority;
    }


    public void ShowClick()
    {
        if (!isShowed)
        {
            animator.SetTrigger("Show");
            animator.ResetTrigger("Hide");
            isShowed = true;
        }
        else
        {
            animator.SetTrigger("Hide");
            animator.ResetTrigger("Show");
            isShowed = false;
        }
        
        //background = 
        //HelpPanel.GetComponent<Image>().sprite = 
    }
    


}

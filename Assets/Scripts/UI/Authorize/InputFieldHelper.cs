using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas HelpPanel;
    public Animator animator;
    
    private bool isShowed = false;
    
    public void ChangePriority(int priority)
    {
        HelpPanel.overrideSorting = (priority != 0);
        HelpPanel.sortingOrder = priority;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrivateChatController : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    // Start is called before the first frame update

    
    private int direction = 1;
    private int angle = 0;
    private float speed = 0.025f;
    public Animator animator;
    

    Vector2 cos_sin = Vector2.zero;

    
    [SerializeField] private RectTransform[] nums;
    
    void Start()
    {
        cos_sin = new Vector2(Mathf.Cos(speed * direction), Mathf.Sin(speed * direction));
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("click");
    }

    // Update is called once per frame


    public void circleRotation()
    {
        cos_sin = new Vector2(Mathf.Cos(speed * direction), Mathf.Sin(speed * direction));

        foreach (var num in nums)
        {
            num.anchoredPosition = new Vector2(num.anchoredPosition.x * cos_sin.x - num.anchoredPosition.y * cos_sin.y,
                    num.anchoredPosition.y * cos_sin.x + num.anchoredPosition.x * cos_sin.y);
        }

        
        

        /*        num.transform.localPosition = new Vector3(num1.transform.localPosition.x * Mathf.Cos(1f * direction) - num1.transform.localPosition.y * Mathf.Sin(1f * direction),
                    num1.transform.localPosition.y * Mathf.Cos(1f * direction) + num1.transform.localPosition.x * Mathf.Sin(1f * direction), 0f);
        */
        angle += 1;
        if (angle > 20)
        {
            angle = 0;
            CancelInvoke("circleRotation");
        }
        
    }


    public void ActivateRotation()
    {
        InvokeRepeating("circleRotation", 0, 0.02f);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        direction = -1;
        ActivateRotation();
        DesolvedPanel.instance.SetStartColour();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        direction = 1;
        ActivateRotation();
        DesolvedPanel.instance.SetUiFigure(0);
    }
}

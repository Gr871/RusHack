using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]

public class Stulmove : MonoBehaviour
{
    public GameObject Cheir;

        
        public void OnMouseDown()
        {
            Cheir.transform.Translate(0, 0, -1);
        }
    
        public void OnMouseUp()
        {
            Cheir.transform.Translate(0, 0, 1);
        }

}
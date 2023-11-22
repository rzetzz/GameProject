using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisable : MonoBehaviour
{
    Transform defaultParent;
    [SerializeField] private Transform attackLocation;
    private void Start() {
        defaultParent = transform.parent;
    }
   void SetDisabled(){
    //    transform.position = attackLocation.position;
    //    transform.SetParent(defaultParent);
       this.gameObject.SetActive(false);
   }
   void Lost()
   {
      
   }
   void Back()
   {
       
   }
}

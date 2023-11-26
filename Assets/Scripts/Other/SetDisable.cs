using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisable : MonoBehaviour
{
    Transform defaultParent;
    [SerializeField] private Transform attackLocation;
    Collider2D coll;
    private void Start() {
        defaultParent = transform.parent;
        coll = GetComponent<Collider2D>();
    }
   void SetDisabled(){
    //    transform.position = attackLocation.position;
    //    transform.SetParent(defaultParent);
       this.gameObject.SetActive(false);
   }
   void Lost()
   {
      coll.enabled = false;
      
   }
   void Back()
   {
       coll.enabled = true;
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] Transform shotLoc;
    BowController bow;
    Rigidbody2D rb;
    void Start()
    {
        bow = transform.parent.GetComponent<BowController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        transform.rotation = Quaternion.identity;
    }
    
    private void Update() {
        if (bow.launchArrow)
        {
            transform.SetParent(null);        
            transform.localScale = new Vector3(1,1,1);
            transform.rotation = ArrowRot();
            StartCoroutine(back()); 
            
        }
    }

    Quaternion ArrowRot()
    {
        Vector2 dir = rb.velocity;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        
        return Quaternion.AngleAxis(angle ,Vector3.forward);
    }

    IEnumerator back()
    {
        yield return new WaitForSeconds(1);
        transform.SetParent(bow.transform);
        transform.rotation = Quaternion.identity;
        transform.position = bow.transform.position;
        this.gameObject.SetActive(false);
        
    }

    
}

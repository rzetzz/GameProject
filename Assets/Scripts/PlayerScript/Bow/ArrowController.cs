using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] Transform shotLoc;
    BowController bow;
    Rigidbody2D rb;
    bool isMissing;
    bool isStuck;
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

    void Miss()
    {
        transform.SetParent(bow.transform);
        transform.rotation = Quaternion.identity;
        transform.position = bow.transform.position;
        rb.simulated = true;
        this.gameObject.SetActive(false);
    }
    IEnumerator back()
    {
        yield return new WaitForSeconds(1);
        Miss();
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Ground" || other.tag =="Enemy")
        {
            isMissing = true;
            bow.launchArrow = false;
            rb.velocity = Vector2.zero;
            rb.simulated = false;
            // Miss();
        }
    }

    
}

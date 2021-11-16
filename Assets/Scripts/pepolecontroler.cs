using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pepolecontroler : MonoBehaviour
{
    
    public int PepoleSpeed; //מהירות הבנאדם
    Vector2 direction;
    Animator anim;//יצירת אנימצה עבור אותו אובייקט
    Rigidbody2D rb2d;
    Vector3 localscale;
    // Start is called before the first frame update
    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        direction = Vector2.right;//מתחיל עם כיוון ימינה
    }

    // Update is called once per frame
    void Update()
    {
        if(rb2d.velocity.x != 0)
            anim.SetBool("isWalking",true);//מאפשר לו תמיד להיות בתנועה
        else
            anim.SetBool("isWalking",false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("pointendpepole"))//X-נקודה שתהפוך את האובייקט בציר ה
        {
            Flip();//היפוך
        }
    
 
    }

     void OnCollisionEnter2D(Collision2D other)//מפגש בין האויב לבנאדם ומשמידה את הבנאדם
     {
        if(other.gameObject.CompareTag("enemyknight"))
        {
            Destroy(this.gameObject);
        }
     }


    void Flip()
    {
        direction = -direction;
        Vector2 newScale = transform.localScale;
        newScale.x = newScale.x * -1;
        transform.localScale = newScale;
    }

    void FixedUpdate()//פונקציה לעדכונים הפיסקלים של המשחק
    {
         
        rb2d.velocity = new Vector2(direction.x * PepoleSpeed , rb2d.velocity.y);//x-הוספת כח לשחקן בציר ה 


    }
}

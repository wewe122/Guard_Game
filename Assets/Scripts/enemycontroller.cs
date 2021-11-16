using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{

    public int EnemySpeed; 
    public float blinkscount;//כמה היבהובים
    public float timetowaitforblink;//זמן המתנה לכל היבהוב
    public Transform Enemyposition;//מיקום חדש לאויב
    bool candie =true;//משתנה לבדיקה אם האויב יכול לההרס
    Vector2 direction;
    Animator anim;
    Rigidbody2D rb2d;
    Vector3 localscale;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        direction = Vector2.left;
    }

    void Update()
    {
        if(rb2d.velocity.x != 0)
            anim.SetBool("isWalking",true);
        else
            anim.SetBool("isWalking",false);
    }   


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("pointend"))//נקודה להיפוך האויב
        {
            
            Flip();
        }
        if(other.CompareTag("gunshot") && candie)//בדיקה אם האויב רשאי לההרס וגם נורה
        {
            
            killenemy();// הריגת האויב
            
        }
    }

   

    void Flip()
    {
        direction = -direction;
        Vector2 newScale = transform.localScale;//וקטור חדש על מנת לשנות את כיוון האויב
        newScale.x = newScale.x * -1;//שינוי הכיוון
        transform.localScale = newScale;//משנה את הכיוון של האויב
    }

    void killenemy()
    {
        StartCoroutine(bringbacktheEnemy());//
    }

    IEnumerator bringbacktheEnemy()//החזרת האוייב לחיים מנקודה שהוגדרה לו
    {
        this.transform.position = Enemyposition.position;
        
        candie = false;
        for(int i = 0; i < blinkscount; i++)//יוצרים היבהובים שמדמה כאילו השחקן נפסל
        {
            this.GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(timetowaitforblink);// ממתינים 0.2 שניות

            this.GetComponent<SpriteRenderer>().enabled = true;

            yield return new WaitForSeconds(timetowaitforblink);
            
        }
        yield return new WaitForSeconds(timetowaitforblink);
        candie = true;
    }


    void FixedUpdate()//פונקציה לעדכונים הפיסקלים של המשחק
    {
         
        rb2d.velocity = new Vector2(direction.x * EnemySpeed , rb2d.velocity.y);//x-הוספת כח לשחקן בציר ה 


    }
}

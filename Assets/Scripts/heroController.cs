using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isGround;//אם השחקן נמצא על מהפלטפורמה
    public float RadiusGround;//רדיוס לבדיקה מהפלטפורמה
    public int PlayerSpeed;//מהירות השחקן   
    public float JumpForce;//משתנה לעוצמת הקפיצה
    public float shootdelay;
    public GameObject shotPrefab;//אובייקט הירייה
    public Transform shotPrefabPoint;//מיקום של התחלת הירייה מהשחקן
    public LayerMask WhatIsGround;//ground שכבת 
    public Transform[] groundPoints;//מערך של שלושה נקודות מתחת לרגליים של השחקן
    Animator anim;//אובייקט ליצירת אנימציה
    float inputX;//x-משתנה לתזוזות השחקן בציר ה
    bool canShoot;//בדיקה אם רשאי לירות
    bool faceright = true;//מתחיל עם הפנים ימינה
    
    Rigidbody2D rb2d;
    Vector2 direction;//וקטור לכיוון ירי הבועה
    Vector3 localscale;
    
    void Start()
    {
        canShoot=true; 
        direction = Vector2.right;
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        localscale = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {   
         isGround = CheckIsGrounded();

        inputX = Input.GetAxis("Horizontal") * PlayerSpeed;
        if(isGround && Input.GetKeyDown("up"))//אם על הפלטפורמה וגם נלחץ למעלה רשאי לקפוץ
        {
            
            rb2d.AddForce(Vector2.up * JumpForce);//מוסיףף כח קפיצה לשחקן
        }

        if(Mathf.Abs(inputX) > 0 && rb2d.velocity.y == 0)//x-בדיקה לאנימציה אם קיימת תנונה כלשהי בציר ה
            anim.SetBool("isRuning",true);//מאתחל אנימצית ריצה
        else   
            anim.SetBool("isRuning",false);
               
        
        if(rb2d.velocity.y == 0)
        {
            anim.SetBool("isFlying",false);//השחקן לא באוויר מבטל אנימציית תעופה
            
        }
            
        
        if(rb2d.velocity.y > 0)
            anim.SetBool("isFlying",true);//השחקן באוויר מאתחל אנימציית תעופה


        if(Input.GetKeyDown(KeyCode.LeftControl) && canShoot)//בדיקה של אם השחקן יכול לירות  
        {
            StartCoroutine(Shooting());
        }
    }

    bool CheckIsGrounded()
    {
        for(int i=0; i < groundPoints.Length; i++ )//מערך של שלושה נקודות שנמצאות ברגליים של השחקן
        {
            if(Physics2D.OverlapCircle(groundPoints[i].position,RadiusGround,WhatIsGround))//בדיקה עבור כל נקודה אם לפחות אחת נמצאת על פלטפורמה כלשהי 
            {                                                                              //(ground)שנמצאת תחת שכבת אדמה
                return true;
            }
            
        }
        return false;
    }

    IEnumerator Shooting()// shootdelay פונקציה שמשהה את פעולת הירי של השחקן עם המשתנה 
    {
        GameObject shoot = Instantiate(shotPrefab,shotPrefabPoint.position,Quaternion.identity);//יצירת אובייקט הירייה
        shoot.GetComponent<gunshot>().direction = direction;//הירייה מקבלת את הכיוון שלה לפי כיוון השחקן

        canShoot=false;
        yield return new WaitForSeconds(shootdelay);//השהייה כל 0.4 שניות
        canShoot=true;

    }

    void OnCollisionEnter2D(Collision2D other)//בדיקה עם האויב התנגש בשחקן
     {
        if(other.gameObject.CompareTag("enemyknight"))
        {
            Destroy(this.gameObject);//הריגת השחקן
        }
     }

    void FixedUpdate()//פונקציה לעדכונים הפיסקלים של המשחק
    {
         
        rb2d.velocity = new Vector2(inputX , rb2d.velocity.y);//x-הוספת כח לשחקן בציר ה 

        if(inputX < 0)
        {
            faceright =false;
            direction = Vector2.left;
           
        }
        
        else if(inputX > 0)
        {
            faceright = true;
            direction = Vector2.right;
          
        }

        if((!faceright && localscale.x > 0) || (faceright && localscale.x < 0))//היפוך השחקן בהתאמה
            localscale.x *= -1;

        transform.localScale = localscale;

    }

}

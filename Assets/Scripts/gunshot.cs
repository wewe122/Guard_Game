using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class gunshot : MonoBehaviour
{
    public float ShootSpeed;
    Rigidbody2D rb2d;
    public Vector2 direction;

    private Vector2 ScreenBounds;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //וקטור בשביל לדעת את אורך המפה של המשחק
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > ScreenBounds.x || transform.position.x < -ScreenBounds.x)//במידה והירייה יצאה מגבולות האורך שלה
        {
          
            Destroyshot();//משמידים אותה
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("enemyknight"))//השמדת הירייה לאחר פגיעה באוייב
        {
           
            Destroyshot();
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = direction * ShootSpeed;
    }

    void Destroyshot()
    {
        Destroy(this.gameObject);
        
        
    }
}

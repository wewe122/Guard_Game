using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignore : MonoBehaviour
{
    public GameObject pepole;//
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(pepole.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());//התעלם בין שתי אובייקטים
    }                                                                                                   //boxColider עם 
}

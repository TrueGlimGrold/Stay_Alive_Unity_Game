using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {   
            Destroy(gameObject);

            // ! Test code 
            // gameObject.SetActive(false);
        }
    }

}

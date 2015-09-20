using UnityEngine;
using System.Collections;

public class bulletCollision : MonoBehaviour {

	
     void OnTriggerEnter(Collider col)
     {
     //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
     //if(col.gameObject.tag == "Enemy")
     //{
       Destroy(col.gameObject);
 //add an explosion or something
 //destroy the projectile that just caused the trigger collision
 Destroy(gameObject);
     }
}

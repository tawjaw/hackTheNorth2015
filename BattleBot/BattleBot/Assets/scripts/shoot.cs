using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class shoot : MonoBehaviour {

 
    public Rigidbody projectile;
 
    public float speed = 20;

    private GameObject objwithText = null;

    bool isFound;
    Text text;
    
    // Update is called once per frame
    void FixedUpdate()
    {

        isFound = true;
       
    }
   
    void OnGUI()
    {

        if (isFound)
        {
            if (!zombieMovement.isGameOver)
            {
               
                if (GUI.Button(new Rect(0, Screen.height * 0.1f, Screen.width*0.3f, Screen.height * 0.4f), "Shoot"))
                {

                    Rigidbody clone;

                    GetComponent<AudioSource>().Play();

                    clone = Instantiate(projectile, transform.position, Quaternion.identity) as Rigidbody;
                    clone.velocity = transform.TransformDirection(Vector3.forward * speed);

                    Destroy(clone.gameObject, 5);

                }

                
            }
            else
            {
               
             
                if (GUI.Button(new Rect(0, Screen.height * 0.1f, Screen.width*0.3f, Screen.height * 0.4f), "RESTART"))
                {
                    zombieMovement.isGameOver = false;
                    Application.LoadLevel(0);
                }
            }
        }
    }



    
}

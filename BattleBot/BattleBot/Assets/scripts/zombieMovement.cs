using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class zombieMovement : MonoBehaviour {

    private Rigidbody rb;
    private int count = 0;

    private int direction = 1;

    private Vector3 targetAngles;
    int maxCount = 1000;
    // private int direction = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxCount = Random.Range(700, 1000);
    }


    void FixedUpdate()
    {
        if (RobotControl.status == RobotControl.Status.PLAYING || RobotControl.status == RobotControl.Status.START)
        {


            Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f * direction);
            count++;
            if (count > maxCount)
            {
                direction = direction * -1;
                rb.velocity = Vector3.zero;
                rb.transform.RotateAround(transform.position, transform.up, 180f);
                count = 0;
            }
            rb.AddForce(rb.transform.forward);
        }

    }



    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Player")
        {

            RobotControl.status = RobotControl.Status.GAMEOVER;

        }
    }
    
}

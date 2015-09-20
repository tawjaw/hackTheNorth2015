using UnityEngine;
using System.Collections;

public class bulletCollision : MonoBehaviour {


    void OnTriggerEnter(Collider col)
    {

        Destroy(col.gameObject);

        Destroy(gameObject);
        RobotControl.NoOfKills++;
        if (RobotControl.NoOfKills == 7)
        {
            RobotControl.status = RobotControl.Status.WON;
            RobotControl.NoOfKills = 0;

        }

    }
}

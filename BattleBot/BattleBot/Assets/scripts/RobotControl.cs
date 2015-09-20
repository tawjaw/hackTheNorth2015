using UnityEngine;
using System.Collections;
using CnControls;

public class RobotControl : MonoBehaviour {

    string bt_module_name = "HC-06";
    public static int NoOfKills = 0;
    public enum Status { GAMEOVER, PLAYING, WON, START };
    public static Status status;
    // Use this for initialization
    void Start()
    {
        //use one of the following two methods to change the default bluetooth module.
        //BtConnector.moduleMAC("00:13:12:09:55:17");
        BtConnector.moduleName("HC-06");

    }

    // Update is called once per frame
    void Update()
    {



        if (status == Status.PLAYING || status == Status.START)
        {
            bool move_h = false;
            var movementVector = new Vector3(CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
            if (movementVector.sqrMagnitude < 0.00001f) return;


            double joystick_h = CnInputManager.GetAxis("Horizontal");

            double joystick_v = CnInputManager.GetAxis("Vertical");

            if (System.Math.Abs(joystick_h) > System.Math.Abs(joystick_v))
            {
                move_h = true;
            }

            if (move_h)
            {
                if (joystick_h < 0)
                {
                    move(3);
                }
                else
                {
                    move(4);
                }
            }
            else
            {
                if (joystick_v > 0)
                {
                    move(1);
                }
                else
                {
                    move(2);
                }
            }
        }



    }

    void OnGUI()
    {

        if (!BtConnector.isConnected())
        {
            if (GUI.Button(new Rect(0, Screen.height * 0.4f, Screen.width, Screen.height * 0.1f), "Connect"))
            {

                if (!BtConnector.isBluetoothEnabled())
                {
                    BtConnector.askEnableBluetooth();
                }
                else
                {
                    BtConnector.moduleName(bt_module_name);
                    BtConnector.connect();
                    status = Status.PLAYING;
                }
            }
        }
        else if (status == Status.GAMEOVER)
        {
            if (GUI.Button(new Rect(Screen.width*0.4f, Screen.height * 0.1f, Screen.width , Screen.height * 0.4f), "YOU LOST! RESTART?"))
            {
                status = Status.START;
                NoOfKills = 0;
                Application.LoadLevel(0);
            }
        }
        else if (status == Status.WON)
        {
            if (GUI.Button(new Rect(Screen.width*0.4f, Screen.height * 0.1f, Screen.width , Screen.height * 0.4f), "YOU WON! RESTART?"))
            {
                NoOfKills = 0;
                status = Status.START;
                Application.LoadLevel(0);
            }
        }

    }

    // forward = 1; backward = 2, turn left = 3, turn right = 4
    void move(int type)
    {
        char msg = 'x';
        switch (type)
        {
            case 1:
                msg = 'w';
                break;
            case 2:
                msg = 's';
                break;
            case 3:
                msg = 'a';
                break;
            case 4:
                msg = 'd';
                break;
            default:
                break;

        }
        BtConnector.sendChar(msg);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Raycaster : MonoBehaviour
{
    public float _speed = 0.1f;
    public float _rotationSpeed = 5f;
    public GameObject _mazeBot;
    public Text distTextForward;
    public Text distTextLeft;
    public Text distTextRight;

    private float _distForward;
    private float _distLeft;
    private float _distRight;

    /*
    private enum LeftOrRight {Left, Right}
    private enum LeftOrForward { Left, Forward }
    private enum RightOrForward { Right, Forward }
    //private Vector3 _targetDir;
    private float[] _leftOrRight = new float[] {0, 1};
    private float[] _leftOrForward = new float[] { 0, 1 };
    private float[] _rightOrForward = new float[] { 0, 1 };
    */

    private enum CrossRoadsList {Left, Right, Forward}

    void Update()
    {
        MovingForward(); 

        RaycastHit hitForward; 
        RaycastHit hitLeft;
        RaycastHit hitRight;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100f, Color.blue); 
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 100f, Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 100f, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitForward, 100f)) // Detta är de tre aktiva raycasts som jobbar
        {
            //Debug.Log(hitForward.collider.name);
            _distForward = hitForward.distance;
            //distTextForward.text = _distForward.ToString("f2") + "cm";
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hitLeft, 100f))
        {
            //Debug.Log(hitLeft.collider.name);
            _distLeft = hitLeft.distance; //Vector3.Distance(_mazeBot.transform.position, hitLeft.point); // Annat sätt för att skriva hitLeft.distance;
           //distTextLeft.text = _distLeft.ToString("f2") + "cm";
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitRight, 100f))
        {
            //Debug.Log(hitRight.collider.name);
            _distRight = hitRight.distance;
            //distTextRight.text = _distRight.ToString("f2") + "cm";
        }

        //if (_distRight > 12 && _distForward < 20) // Rotations satser
        if(_distLeft < 20 && _distForward < 8 && _distRight > _distLeft)
        {
            TurnRight();
            Debug.Log("Action: TurnRight()");
        }

        //if (_distLeft > 12 && _distForward < 8)
        if (_distRight < 20 && _distForward < 8 && _distLeft > _distRight)
        {
            TurnLeft();
            Debug.Log("Action: TurnLeft()");
        }

        if (_distRight < 15 && _distForward < 8 && _distLeft < 15)
        {
            TurnAround();
        }


        if (_distLeft > 25 && _distRight > 25) // vägskäls randomisering
        {
            TurnLeftOrRight();
        }
        else if (_distLeft > 25 && _distForward > 25)
        {
            TurnLeftOrForward();
        }
        else if (_distRight > 25 && _distForward > 25)
        {
            TurnRightOrForward();
        }


        if (_distLeft < 10) // centrerings satser. Detta för att _mazeBot ska centrera sig i korridoren hela tiden. 
        {
            transform.position = transform.position + _mazeBot.transform.right * _speed * Time.deltaTime;       
            _mazeBot.transform.position = transform.position + _mazeBot.transform.forward * _speed * Time.deltaTime;
        }
        if (_distRight < 10)
        {
            transform.position = transform.position + -_mazeBot.transform.right * _speed * Time.deltaTime;       
            _mazeBot.transform.position = transform.position + _mazeBot.transform.forward * _speed * Time.deltaTime;
        }

        if (_distLeft < 10 && _distForward < _distRight)
        {
            TurnRight();
        }
        if (_distRight < 10 && _distForward < _distLeft)
        {
            TurnLeft();
        }
    }

    private void TurnLeft()
    {
        transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);                                                             
    }

    private void TurnRight()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);                                                                       
    }

    private void TurnAround()
    {
        transform.Rotate(Vector3.up * 180);
    }

    private void TurnLeftOrRight() // Vägskäls-satser & randomizeringar
    {
        var leftOrRight = (CrossRoadsList)Random.Range(0, 1);
        if(leftOrRight == CrossRoadsList.Left)
        {
            TurnLeft();
        }
        else
        {
            TurnRight();
        }
        Debug.Log("ACTION: Random Left or Right");
    }

    private void TurnLeftOrForward()
    {
        var leftOrForward = (CrossRoadsList)Random.Range(0, 2);
        if (leftOrForward == CrossRoadsList.Left)
        {
            TurnLeft();
        }
        else
        {
            MovingForward();
        }
        Debug.Log("ACTION: Random Left or Forward");
    }

    private void TurnRightOrForward()
    {
        var rightOrForward = (CrossRoadsList)Random.Range(1, 2);
        if (rightOrForward == CrossRoadsList.Right)
        {
            TurnRight();
        }
        else
        {
            MovingForward();
        }
        Debug.Log("ACTION: Random Right or Forward");
    }

    private void AvoidCollision() // För att undvika att den går igenom väggar / för nära väggar
    {
        if (_distForward < 15 && _distForward < _distLeft && _distForward < _distRight)
        {
            TurnLeftOrRight();
        }
    }

    private void MovingForward()
    {
        transform.position = transform.position + _mazeBot.transform.forward * _speed * Time.deltaTime;
    }

    /*
    private void OnCollisionEnter(Collider collisioninfo) // För att se om och när och vem som trots scripten ändå lyckas nudda / åka igenom en vägg.
    {
        if (gameObject.name == "Player_Blue")
        {
            Debug.Log("WARNING: Player Blue hit a wall");
        }
        if (collisioninfo.name == "Player_Red")
        {
            Debug.Log("WARNING: Player Red hit a wall");
        }
    }
    */

    /*
    private void CrossRoads() // Eller borde jag ha en metod för för varje vägskäls variation? som med lists enums där uppe?
    {
        if (_distLeft > 12 && _distRight > 12)
        {
            // LeftOrRight Randomize with that array
        }
        else if (_distLeft > 12 && _distForward > 12)
        {
            // LeftOrForward Randomize with that array
        }
        else if (_distRight > 12 && _distForward > 12)
        {
            // RightOrForward Randomize with that array
        }
    }
    */
}

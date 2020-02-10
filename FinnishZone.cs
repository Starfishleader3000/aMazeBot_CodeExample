using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnishZone : MonoBehaviour
{
    public GameManager _gameManager;

    /*
    private void OnTriggerEnter(Collider other)
    {

        _gameManager.GameWon();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Finnish zone entered!");
        _gameManager.GameWon();
    }
    */

    /*
private void OnCollisionEnter(Collision collision)
{
    //Check for a match with the specified name on any GameObject that collides with your GameObject
    if (collision.gameObject.name == "_player")
    {
        //If the GameObject's name matches the one you suggest, output this message in the console
        Debug.Log("YOU WIN!");
        GameWon();
    }

    if (collision.gameObject.name == "_aMazeBot_Blue")
    {
        //If the GameObject's name matches the one you suggest, output this message in the console
        Debug.Log("YOU LOSE!");
        GameEnded();
    }

    if (collision.gameObject.name == "_aMazeBot_Red")
    {
        //If the GameObject's name matches the one you suggest, output this message in the console
        Debug.Log("YOU LOSE!");
    }
}
*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game Manager engaged at trigger zone");
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("YOU WIN!");
            _gameManager.GameWon();
        }

        if (other.gameObject.name == "Player_Blue")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("YOU LOSE!");
            _gameManager.GameEnded();
        }

        if (other.gameObject.name == "Player_Red")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("YOU LOSE!");
            _gameManager.GameEnded();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildThePlayer : MonoBehaviour
{
    public GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           other.transform.SetParent(this.transform);
           //other.GetComponent<PlayerLocomotionHandler>().playerIsGrounded = true;
           //other.GetComponent<CharacterController>().enabled = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        other.transform.parent = gameManager.transform;
        //other.GetComponent<CharacterController>().enabled = true;
    }
}

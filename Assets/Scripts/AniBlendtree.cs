using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AniBlendtree : MonoBehaviour
{
    public PlayerLocomotionHandler playerLocomotionHandler;
    public GameObject player;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        player.SetActive(true);
    }
    void Update()
    {
        anim.SetFloat("Speed",playerLocomotionHandler.playerVelocity);
    }
}

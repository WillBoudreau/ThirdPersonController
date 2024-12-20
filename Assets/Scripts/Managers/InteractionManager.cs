using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public Camera playerCam;
    public int MaxRayDist;
    public UIManager uiManager;
    public int CoinCount;
    [SerializeField]
    private GameObject target;
    private Interactable targetInteractable;
    public bool InteractionPossible;

    private void Awake()
    {
        playerCam = cameraManager.playerCamera;
        uiManager = FindObjectOfType<UIManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            InteractionPossible = true;
        }
        else
        {
            InteractionPossible = false;
        }
    }
    private void FixedUpdate()
    {
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, MaxRayDist))
        {
            if(hit.transform.gameObject.CompareTag("Interactable"))
            {
                Debug.Log(hit.transform.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
            }
            else
            {
                target = null;
                targetInteractable = null;
            }
        }
        else
        {
            target = null;
            targetInteractable = null;
        }
        SetGameplayMessage();
    }
    public void Interact()
    {
        switch(targetInteractable.type)
        {
            case Interactable.InteractableOBJ.Door:
                targetInteractable.Acitivate();
                break;
            case Interactable.InteractableOBJ.Button:
                targetInteractable.Acitivate();
                break;
            case Interactable.InteractableOBJ.Pickup:
                CoinCount++;
                targetInteractable.Acitivate();
                break;
        }
    }
    void SetGameplayMessage()
    {
        string message = " ";
        if(target != null)
        {
            switch (targetInteractable.type)
            {
                case Interactable.InteractableOBJ.Door:
                    message = "Press LMB to open door";
                    break;
                case Interactable.InteractableOBJ.Button:
                    message = "Press LMB to press button";
                    break;
                case Interactable.InteractableOBJ.Pickup:
                    message = "Press LMB to pick up";
                    break;
            }
        }
        uiManager.UpdateGamePlayMessage(message);
    }
}

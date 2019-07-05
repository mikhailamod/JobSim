using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{

    public SteamVR_Action_Boolean grabAction = null;
    public SteamVR_Action_Boolean interactAction = null;
    public CupGenerator cupGenerator;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint fixedJoint = null;

    public Interactable currentInteractable = null;
    public List<Interactable> interactables = new List<Interactable>();

    void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        fixedJoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabAction.GetStateDown(pose.inputSource))
        {
            Debug.Log(pose.inputSource + " Trigger Down");
            Pickup();
        }

        if(grabAction.GetStateUp(pose.inputSource))
        {
            Debug.Log(pose.inputSource + " Trigger Up.");
            Drop();
        }

        if(grabAction.GetStateDown(pose.inputSource) && !GameManager.Instance.hasStarted)
        {
            GameManager.Instance.StartGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        interactables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        interactables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        currentInteractable = GetNearestInteractable();

        if (!currentInteractable)
            return;

        if (currentInteractable.activeHand)
            currentInteractable.activeHand.Drop();

        currentInteractable.transform.position = transform.position;
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        fixedJoint.connectedBody = targetBody;
        currentInteractable.activeHand = this;

        if(currentInteractable.IsInSpawn())
        {
            Vector3 temp = new Vector3(currentInteractable.transform.position.x,
                                        currentInteractable.transform.position.y,
                                        currentInteractable.transform.position.z);
            cupGenerator.generateCup(currentInteractable.isSmall, currentInteractable.transform.position);
        }
    }

    public void Drop()
    {
        if (!currentInteractable)
            return;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();
        targetBody.transform.rotation = Quaternion.identity;

        fixedJoint.connectedBody = null;
        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    public void Drop(FixedJoint targetJoint)
    {
        if (!currentInteractable)
            return;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        fixedJoint.connectedBody = null;
        currentInteractable.activeHand = null;
        currentInteractable = null;

        targetJoint.connectedBody = targetBody;
    }

    Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float min = float.MaxValue;
        float current = 0.0f;

        foreach(Interactable i in interactables)
        {
            current = (i.transform.position - transform.position).sqrMagnitude;
            if(current < min)
            {
                min = current;
                nearest = i;
            }
        }

        return nearest;
    }

    public void ForceRemove(Interactable i)
    {
        interactables.Remove(i);
    }
}

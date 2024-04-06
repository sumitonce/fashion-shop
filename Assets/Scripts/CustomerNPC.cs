using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum CustomerState
{
    Idle,
    Entering,
    Waiting,
    Leaving
}

public class CustomerNPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;
    public SkinnedMeshRenderer meshRenderer;
    
    public NPCManager npcManager { get; set; }

    private CustomerState currentState;
    
    [SerializeField]
    private ClothType requiredClothType; // Set random cloth type
    private Vector3 spawnPosition;
    private Texture defaultTexture;

    private void Awake()
    {
        currentState = CustomerState.Idle;
        spawnPosition = transform.position;
        defaultTexture = meshRenderer.material.mainTexture;

        requiredClothType = (ClothType)Random.Range(0, 2);
    }

    private void Update()
    {
        if (agent.hasPath)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    public ClothType GetRequiredClothType()
    {
        return requiredClothType;
    }

    public CustomerState GetCurrentState()
    {
        return currentState;
    }
    
    public void ChangeCloth(Texture2D newTexture)
    {
        meshRenderer.material.mainTexture = newTexture;

        StartCoroutine(LeaveStore());
    }
    
    public void SetState(CustomerState state)
    {
        currentState = state;
    }

    public void SetDeliveryTarget(Vector3 targetPosition)
    {
        currentState = CustomerState.Entering;
        agent.SetDestination(targetPosition);
    }

    private void ResetNPC()
    {
        meshRenderer.material.mainTexture = defaultTexture;
        currentState = CustomerState.Idle;
    }

    IEnumerator LeaveStore()
    {
        yield return new WaitForSeconds(2f);

        currentState = CustomerState.Leaving;
        agent.SetDestination(spawnPosition);

        yield return new WaitForSeconds(5f);
        
        ResetNPC();
        npcManager.ReturnNPCToWaiting(this);
    }
}

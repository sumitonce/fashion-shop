using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public int initialCustomersSize = 8; // Total number of customers to spawn
    public int customersPoolSize = 10;
    public Transform npcSpawnPoint;
    public GameObject npcPrefab;

    public List<ClothDeliveryPoint> deliveryPoints;
    
    private Queue<CustomerNPC> waitingNPCs = new Queue<CustomerNPC>();
    private List<GameObject> npcPool = new List<GameObject>();

    private void Start()
    {
        ClothDeliveryPoint.OnDeliveryPointFreed += HandleDeliveryPointFreed;
        
        InitializeNPCPool();
        SpawnInitialNPCs();
    }

    private void InitializeNPCPool()
    {
        GameObject npcGO = Instantiate(npcPrefab, npcSpawnPoint.position, npcPrefab.transform.rotation);
        npcGO.SetActive(false);
        npcPool.Add(npcGO);
    }

    private GameObject GetNPCFromPool()
    {
        foreach (var npc in npcPool)
        {
            if (!npc.activeInHierarchy)
            {
                npc.SetActive(true);
                return npc;
            }
        }
        
        GameObject npcGO = Instantiate(npcPrefab, npcSpawnPoint.position, npcPrefab.transform.rotation);
        npcPool.Add(npcGO);

        return npcGO;
    }

    private void SpawnInitialNPCs()
    {
        for (int i = 0; i < initialCustomersSize; i++)
        {
            SpawnNPC();
        }
    }

    private void SpawnNPC()
    {
        GameObject npcGO = GetNPCFromPool();
        CustomerNPC newNPC = npcGO.GetComponent<CustomerNPC>();
        newNPC.npcManager = this;
        AssignDeliveryPoint(newNPC);
    }

    private void AssignDeliveryPoint(CustomerNPC npc)
    {
        ClothDeliveryPoint freePoint = deliveryPoints.FirstOrDefault(dp => !dp.IsOccupied);

        if (freePoint != null)
        {
            npc.SetDeliveryTarget(freePoint.transform.position);
            freePoint.Occupy();
        }
        else
        {
            waitingNPCs.Enqueue(npc);
        }
    }

    private void HandleDeliveryPointFreed(ClothDeliveryPoint freeDeliveryPoint)
    {
        if (waitingNPCs.Count > 0)
        {
            var npc = waitingNPCs.Dequeue();
            npc.SetDeliveryTarget(freeDeliveryPoint.transform.position);
            freeDeliveryPoint.Occupy();
        }
        else
        {
            SpawnNPC();
        }
    }

    public void ReturnNPCToPool(GameObject npc)
    {
        npc.SetActive(false);
    }

    public void ReturnNPCToWaiting(CustomerNPC npc)
    {
        waitingNPCs.Enqueue(npc);
    }

    private void OnDestroy()
    {
        ClothDeliveryPoint.OnDeliveryPointFreed -= HandleDeliveryPointFreed;
    }
}

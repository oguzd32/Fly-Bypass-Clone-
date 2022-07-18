using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject wing = ObjectPooler.Instance.GetPooledObject("Wing");
            wing.transform.position = spawnPoint.position;
            wing.SetActive(true);
        }
    }
}

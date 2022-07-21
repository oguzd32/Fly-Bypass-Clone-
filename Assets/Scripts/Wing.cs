using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 5f;
    [SerializeField] private Collider m_Collider;
    
    // private variables
    private Vector3 initialScale;
    private void Start()
    {
        initialScale = transform.localScale;
    }

    public void Collect()
    {
        m_Collider.enabled = false;
        transform.DOScale(Vector3.zero, .5f);
        
        GameObject particle = ObjectPooler.Instance.GetPooledObject("Collect");
        particle.transform.position = transform.position;
        particle.SetActive(true);

        GameObject popUp = ObjectPooler.Instance.GetPooledObject("PopUpText");
        popUp.transform.position = transform.position;
        popUp.SetActive(true);
        
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        transform.DOScale(initialScale, .5f).OnComplete(() => m_Collider.enabled = true);
    }
}

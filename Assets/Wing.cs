using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 5f;
    [SerializeField] private Collider m_Collider;

    private bool once = false;
    private Vector3 initialScale;
    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameReferenceHolder.Instance.playerController.WingCount += 3;
            Collect();    
        }
    }

    public void Collect()
    {
        m_Collider.enabled = false;
        transform.DOScale(Vector3.zero, .5f);
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        transform.DOScale(initialScale, .5f).OnComplete(() => m_Collider.enabled = true);
    }
}

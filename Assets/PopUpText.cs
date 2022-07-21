using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    
    private void OnEnable()
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;
    }

    private void Update()
    {
        Color color = text.color;
        color.a -= Time.deltaTime;
        text.color = color;

        transform.position += Vector3.up * Time.deltaTime * 5;
        
        if (color.a <= 0)
        {
            transform.DOKill();
            gameObject.SetActive(false);
        }
    }
}

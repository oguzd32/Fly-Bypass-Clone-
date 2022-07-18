using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{
    [SerializeField] private Score scorePrefab;
    [SerializeField] private int scoreCubeLenght;
    [SerializeField] private float cubeOffSet;
    [SerializeField] private Gradient scoreColor;
    
    private void Start()
    {
        for (int i = 0; i < scoreCubeLenght; i++)
        {
            Score scoreInstance = Instantiate(scorePrefab, transform);
            scoreInstance.gameObject.name = $"{i + 1}X";
            scoreInstance.transform.localPosition = new Vector3(0, 0, 1 + (i * cubeOffSet));
            scoreInstance.SetCube(i + 1, scoreColor.Evaluate((float)i / scoreCubeLenght));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private TextMeshPro text;

    public void SetCube(int score, Color color)
    {
        cubeRenderer.material.color = color;
        text.text = $"{score}X";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private TextMeshPro text;

    public int scoreAmount;

    public void SetCube(int score, Color color)
    {
        cubeRenderer.material.color = color;
        text.text = $"{score}X";
    }
}

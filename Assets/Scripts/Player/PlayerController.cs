using UnityEngine;
using  DG.Tweening;
using static Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;

    // cached components
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    internal void StartGame()
    {
        movement.StartGame();
        characterAnimator.SetBool("Start", true);
    }

}

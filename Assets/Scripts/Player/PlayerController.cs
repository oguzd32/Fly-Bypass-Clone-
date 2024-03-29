using System;
using UnityEngine;
using  DG.Tweening;
using TMPro;
using static Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform wing;
    [SerializeField] private GameObject windParticle;
    [SerializeField] private TextMeshPro wingCountText;
    public float WingCount
    {
        get => wingCount;
        set
        {
            wingCount = value;
            wingCountText.text = ((int) wingCount).ToString();
            if (wingCount > 0)
            {
                wing.gameObject.SetActive(true);
                Vector3 tempScale = wing.localScale;
                tempScale.x = .4f;
                wing.localScale = tempScale;
            }
        }
    }

    public bool isFinish { get; set; } = false;
    
    // cached components
    private PlayerMovement movement;
    private Rigidbody m_Rigidbody;

    // private variables
    private float wingCount = 0;
    
    private bool isFly = false;
    private bool isFailed = false;
    
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        m_Rigidbody = GetComponent<Rigidbody>();
        
        WingCount = 0;
    }

    internal void StartGame()
    {
        movement.StartGame();
        characterAnimator.SetBool("isStarted", true);
    }

    private void Update()
    {
        if(isFinish || isFailed) return;
        SetFlyAnimation(!IsGrounded());
        _GameReferenceHolder.cameraFollow.ChangeFov(!IsGrounded());

        if(transform.position.y < -5) FailProcess();
        
        if (Input.GetMouseButton(0) && WingCount > 0 && isFly)
        {
            FlyProcess();
        }
        else
        {
            movement.SetMaxForwardSpeed(10);
        }
    }

    private void FlyProcess()
    {
        movement.SetMaxForwardSpeed(12);
        
        WingCount = Mathf.Max(0, WingCount - Time.deltaTime * 5);

        Vector3 tempWingScale = wing.transform.localScale;
        tempWingScale.x = (WingCount / 5);
        wing.gameObject.SetActive(tempWingScale.x > 0);
        wing.transform.localScale = tempWingScale;

        if (m_Rigidbody.velocity.y < 0)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
        }
        
        m_Rigidbody.AddRelativeForce(Vector3.up * 2);
    }

    private void FailProcess()
    {
        isFailed = true;
        movement.enabled = false;
        _GameReferenceHolder.cameraFollow.SetTarget(null, Vector3.zero);
        _GameManager.EndGame(false);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + Vector3.up * .1f, transform.TransformDirection(Vector3.down), out hit, .5f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
            isFly = false;
            windParticle.SetActive(false);
            //wing.gameObject.SetActive(false);
            wing.DOScaleX(.4f, .5f);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
            //wing.gameObject.SetActive(true);
            wing.DOKill();
            windParticle.SetActive(true);
            return false;
        }
    }

    public void SetVictoryAnim()
    {
        characterAnimator.SetTrigger("Finish");
        windParticle.SetActive(false);
    } 

    public void SetFlyAnimation(bool value)
    {
        characterAnimator.SetBool("OnGround", !value);
        if (value && !isFly)
        {
            isFly = true;
            transform.DOMoveY(2, 1f);
        }
    }
}

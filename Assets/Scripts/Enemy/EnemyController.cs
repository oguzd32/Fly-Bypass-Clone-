using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Renderer characterRenderer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform wing;
    
    public float WingCount
    {
        get => wingCount;
        set => wingCount = value;
    }

    public bool isFinish { get; set; } = false;
    
    // cached components
    private EnemyMovement movement;
    private Rigidbody m_Rigidbody;

    // private variables
    private float wingCount = 0;
    private float rotateAmount = 2f;
    private bool isFly = false;
    private bool isFailed = false;

    public void SetColor(Color color) => characterRenderer.materials[1].color = color; 
    
    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        m_Rigidbody = GetComponent<Rigidbody>();
        
        wingCount = 4;
    }

    internal void StartGame()
    {
        movement.StartGame();
        characterAnimator.SetBool("isStarted", true);
    }
    
    private void Update()
    {
        if(isFinish || isFailed) return;

        if (transform.position.x == 2.7f)
        {
            movement.rotateDir += Random.Range(-rotateAmount, 0f);
        }
        else if (transform.position.x == -2.7f)
        {
            movement.rotateDir += Random.Range(0f, rotateAmount);
        }
        else
        {
            movement.rotateDir += Random.Range(-rotateAmount, rotateAmount);
        }
        
        
        SetFlyAnimation(!IsGrounded());

        if(transform.position.y < -5) FailProcess();
        
        if (WingCount > 0 && isFly)
        {
            FlyProcess();
        }
    }

    private void FlyProcess()
    {
        WingCount = Mathf.Max(0, WingCount - Time.deltaTime * 5);

        Vector3 tempWingScale = wing.transform.localScale;
        tempWingScale.x = 1 + (WingCount / 5);
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
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + Vector3.up * .1f, transform.TransformDirection(Vector3.down), out hit, .5f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
            isFly = false;
            wing.gameObject.SetActive(false);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
            wing.gameObject.SetActive(true);
            return false;
        }
    }
    
    public void SetVictoryAnim() => characterAnimator.SetTrigger("Finish");
    
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

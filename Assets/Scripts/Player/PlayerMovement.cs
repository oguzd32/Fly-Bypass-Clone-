using System.Timers;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxForwardSpeed = 10f;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private float accelerateSpeed = 10;
    [SerializeField] private float rotationAmount = 45;

    public bool inFinal = false;
    
    // cached components
    private Rigidbody m_Rigidbody;

    // private variables
    private bool isStarted = false;
    private bool isTouchGround = true;
    private float _ForwardSpeed;

    private Vector3 speed;
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        _ForwardSpeed = maxForwardSpeed;
    }

    public void SetMaxForwardSpeed(float speed) => maxForwardSpeed = speed; 
    
    internal void StartGame()
    {
        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStarted) return;

        Rotate(TouchInput.SwerveDeltaX * 0.05f, rotateSpeed);

        if (isTouchGround)
        {
            _ForwardSpeed = Mathf.Clamp( _ForwardSpeed + Time.deltaTime * accelerateSpeed, 0, maxForwardSpeed);
        }
        
        transform.position += transform.forward * Time.deltaTime * _ForwardSpeed;

        if(inFinal) Clamp();
    }

    private void Rotate(float direction, float speed)
    {
        transform.Rotate(Vector3.up, direction * Time.deltaTime * speed);
        float yRot = transform.eulerAngles.y;
        if (yRot > 180f) yRot -= 360f;
        yRot = Mathf.Clamp(yRot, -rotationAmount, rotationAmount);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yRot, transform.eulerAngles.z);
    }

    private void Clamp()
    {
        Vector3 tempPos = transform.position;
        tempPos.x = Mathf.Clamp(tempPos.x, -2.7f, 2.7f);
        transform.position = tempPos;
    }
}

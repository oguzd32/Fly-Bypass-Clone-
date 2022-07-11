using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float extraSpeed = 3;
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private float clampX = 1.7f;
    [SerializeField] private GameObject windParticle;

    // cached components
    private CharacterController cc;
    
    // private variables
    private bool isStarted = false;
    private float _ForwardSpeed;

    private Vector3 speed;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        _ForwardSpeed = forwardSpeed;
    }

    internal void StartGame()
    {
        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStarted) return;
        
        speed.x = TouchInput.SwerveDeltaX * 0.05f * horizontalSpeed;
        speed.z = _ForwardSpeed;
        cc.SimpleMove(speed);
        
        Clamp();
    }

    private void Clamp()
    {
        Vector3 tempPos = transform.position;

        tempPos.x = Mathf.Clamp(tempPos.x, -clampX, clampX);

        transform.position = tempPos;
    }

    public void IncreaseSpeed()
    {
        _ForwardSpeed += extraSpeed;
        float totalSpeed = _ForwardSpeed + extraSpeed;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => windParticle.SetActive(true));
        sequence.AppendCallback(() => DOTween.To(() => _ForwardSpeed,
            x => _ForwardSpeed
                = x, totalSpeed,
            1));

        sequence.AppendInterval(5f);
        sequence.AppendCallback(() => windParticle.SetActive(false));
        sequence.AppendCallback(() => DOTween.To(() => _ForwardSpeed,
            x => _ForwardSpeed
                = x, forwardSpeed,
            1));
    }
}

using UnityEngine;
using  DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private bool followOnX = false;
    
    // cached components
    private Camera mainCamera;
    
    // private variables
    private Transform target = default;

    private Vector3 velocity = Vector3.zero;
    private Vector3 offSet = Vector3.zero;
    private Vector3 targetPosition;

    private void Start()
    {
        target = GameReferenceHolder.Instance.playerController.transform;
        offSet = transform.position - target.position;
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if(!target) return;

        targetPosition = target.position + offSet;
        targetPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (!followOnX)
        {
            targetPosition.x = transform.position.x;
        }

        transform.position = targetPosition;
    }

    public void SetTarget(Transform target, Vector3 offSet)
    {
        this.target = target;
        this.offSet = offSet;
    }

    public void ChangeFov(bool isFly)
    {
        transform.DOKill();
        
        if (isFly)
        {
            DOTween.To(() => mainCamera.fieldOfView,
                x => mainCamera.fieldOfView
                    = x, 75,
                1);
        }
        else
        {
            DOTween.To(() => mainCamera.fieldOfView,
                x => mainCamera.fieldOfView
                    = x, 65,
                1);
        }
    }
}

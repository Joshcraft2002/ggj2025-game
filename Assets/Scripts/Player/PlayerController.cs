using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private Transform _bubble;
    [SerializeField] private float _bubbleSizeChangeRate = 50f;
    [Tooltip("Force modifier based on bubble size")]
    [SerializeField] private float _floatForce = 2f;

    [SerializeField] private Animator animation;

    private Vector3 _moveDirection;
    private bool _isBlowing = false;

    private Rigidbody _rb;

    private void Start()
    {
        // Get our references
        _rb = GetComponent<Rigidbody>();

        GameManager.Instance.GamePaused.AddListener(CancelBlow);
    }

    public void OnBlow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isBlowing = true;
        }
        else if (context.canceled)
        {
            _isBlowing = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = context.ReadValue<Vector2>();
        _moveDirection = new(inputDirection.x, 0f, 0f);
    }

    private void CancelBlow()
    {
        _isBlowing = false;
    }

    void Update()
    {
        float currentScale = _bubble.localScale.y; //arbitrary axis so bubble doesn't go negative
        if (_isBlowing)
        {
            _bubble.localScale = (currentScale + _bubbleSizeChangeRate) * Vector3.one;
            _rb.AddForce(_floatForce * Vector3.up, ForceMode.Impulse);
        }
        else
        {
            _bubble.localScale = Mathf.Max(currentScale - _bubbleSizeChangeRate, 0f) * Vector3.one;
        }
        
        animation.SetFloat("velocity", _rb.linearVelocity.x);

        /*if (!Mathf.Approximately(_rb.linearVelocity.y, 0f))
        {
            animation.SetFloat("isFloat", _rb.linearVelocity.y);
        }*/
    }

    private void FixedUpdate()
    {      
        _rb.AddForce(_moveDirection * _acceleration, ForceMode.Acceleration);
    }
}

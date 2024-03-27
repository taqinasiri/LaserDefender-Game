using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingButtom;

    private Vector2 rawInput;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()

    {
        InitBounds();
    }

    private void Update()
    {
        Move();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;

        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    private void Move()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new()
        {
            x = Mathf.Clamp(transform.position.x + delta.x,minBounds.x + paddingLeft,maxBounds.x - paddingRight),
            y = Mathf.Clamp(transform.position.y + delta.y,minBounds.y + paddingButtom,maxBounds.y - paddingTop)
        };
        transform.position = newPos;
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if(shooter is not null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
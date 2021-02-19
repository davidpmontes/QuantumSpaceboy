using UnityEngine;

public class Spaceboy : MonoBehaviour, IGravityInfluenced
{
    private UnityEngine.InputSystem.PlayerInput playerInput;
    private Vector2 leftStickInput;
    private bool fireInput;
    private bool towInput;
    private bool aimLockInput;
    private bool boostInput;
    private bool isTowingInputDownThisFrame;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] basicRotationSprites;
    [SerializeField] private Sprite[] lowFireRotationSprites;
    [SerializeField] private Sprite[] highFireRotationSprites;
    private int rotationIdx = 0;
    private float nextThrustTime;
    private float nextFireTime;
    private bool isLowThrust;
    private float shipRotationRads;
    [SerializeField] private float rotationDegrees;

    private Vector2 thrust;

    private const float ROTATION_SPEED = 350f;
    private const float THRUST_SPEED = 500000;
    private const float THRUST_BOOST_SPEED_MULTIPLIER = 2f;
    private const float BULLET_SPEED = 300f;

    private const float TOW_DISTANCE = 2f;

    private bool isTowing = false;
    private DistanceJoint2D distanceJoint2d;
    private LineRenderer lineRenderer;

    void Awake()
    {
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        distanceJoint2d = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        GetInput();
        Rotate();
        CalculateThrust();
        DrawShip();
        Fire();
        Tow();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        leftStickInput = playerInput.actions["Move"].ReadValue<Vector2>();
        fireInput = playerInput.actions["Fire"].ReadValue<float>() > 0;
        boostInput = playerInput.actions["Boost"].ReadValue<float>() > 0;
        var newTowInput = playerInput.actions["Tow"].ReadValue<float>() > 0;
        if (towInput == false && newTowInput == true)
        {
            isTowingInputDownThisFrame = true;
        }
        else
        {
            isTowingInputDownThisFrame = false;
        }
        towInput = newTowInput;
    }

    private void Fire()
    {
        if (!fireInput)
            return;

        if (Time.time < nextFireTime)
        {
            return;
        }
        else
        {
            nextFireTime = Time.time + 0.1f;
            var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.Bullet);
            bullet.SetActive(true);
            var position = transform.position + (new Vector3(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * 0.6f;
            var velocity = rb2d.velocity + (new Vector2(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * BULLET_SPEED * Time.fixedDeltaTime;
            bullet.GetComponent<Bullet>().Init(position, velocity);
        }
    }

    private void Rotate()
    {
        if (leftStickInput.x > 0)
        {
            rotationDegrees += Time.deltaTime * ROTATION_SPEED;
            if (rotationDegrees > 360) rotationDegrees -= 360f;
            rotationIdx = Mathf.FloorToInt(rotationDegrees / ((180 / 17) / 2));
        }

        if (leftStickInput.x < 0)
        {
            rotationDegrees -= Time.deltaTime * ROTATION_SPEED;
            if (rotationDegrees < 0) rotationDegrees += 360f;
            rotationIdx = Mathf.FloorToInt(rotationDegrees / ((180 / 17) / 2));
        }
    }

    private void CalculateThrust()
    {
        shipRotationRads = Mathf.Deg2Rad * (rotationDegrees + 90);

        if (leftStickInput.y > 0)
        {            
            thrust = (new Vector2(-Mathf.Cos(shipRotationRads), Mathf.Sin(shipRotationRads))) * Time.fixedDeltaTime * THRUST_SPEED;
            if (boostInput) thrust *= THRUST_BOOST_SPEED_MULTIPLIER;

            var fuelUsed = Time.fixedDeltaTime;
            if (boostInput) fuelUsed *= THRUST_BOOST_SPEED_MULTIPLIER;

            CanvasManager.Instance.ChangeFuelBar(-fuelUsed);
        }
        else
        {
            thrust = Vector2.zero;
        }
    }

    private void Move()
    {
        rb2d.AddForce(thrust);
    }

    private void DrawShip()
    {
        int idx = 0;

        float pieWidth = (360f / 32f);
        float pieWidthHalf = (360f / 32f) / 2f;

        if ((rotationDegrees > (pieWidth * 0) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 0) + pieWidthHalf)) idx = 0;
        else if ((rotationDegrees > (pieWidth * 1) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 1) + pieWidthHalf)) idx = 1;
        else if ((rotationDegrees > (pieWidth * 2) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 2) + pieWidthHalf)) idx = 2;
        else if ((rotationDegrees > (pieWidth * 3) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 3) + pieWidthHalf)) idx = 3;
        else if ((rotationDegrees > (pieWidth * 4) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 4) + pieWidthHalf)) idx = 4;
        else if ((rotationDegrees > (pieWidth * 5) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 5) + pieWidthHalf)) idx = 5;
        else if ((rotationDegrees > (pieWidth * 6) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 6) + pieWidthHalf)) idx = 6;
        else if ((rotationDegrees > (pieWidth * 7) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 7) + pieWidthHalf)) idx = 7;
        else if ((rotationDegrees > (pieWidth * 8) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 8) + pieWidthHalf)) idx = 8;
        else if ((rotationDegrees > (pieWidth * 9) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 9) + pieWidthHalf)) idx = 9;
        else if ((rotationDegrees > (pieWidth * 10) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 10) + pieWidthHalf)) idx = 10;
        else if ((rotationDegrees > (pieWidth * 11) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 11) + pieWidthHalf)) idx = 11;
        else if ((rotationDegrees > (pieWidth * 12) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 12) + pieWidthHalf)) idx = 12;
        else if ((rotationDegrees > (pieWidth * 13) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 13) + pieWidthHalf)) idx = 13;
        else if ((rotationDegrees > (pieWidth * 14) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 14) + pieWidthHalf)) idx = 14;
        else if ((rotationDegrees > (pieWidth * 15) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 15) + pieWidthHalf)) idx = 15;
        else if ((rotationDegrees > (pieWidth * 16) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 16) + pieWidthHalf)) idx = 16;
        else if ((rotationDegrees > (pieWidth * 17) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 17) + pieWidthHalf)) idx = 15;
        else if ((rotationDegrees > (pieWidth * 18) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 18) + pieWidthHalf)) idx = 14;
        else if ((rotationDegrees > (pieWidth * 19) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 19) + pieWidthHalf)) idx = 13;
        else if ((rotationDegrees > (pieWidth * 20) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 20) + pieWidthHalf)) idx = 12;
        else if ((rotationDegrees > (pieWidth * 21) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 21) + pieWidthHalf)) idx = 11;
        else if ((rotationDegrees > (pieWidth * 22) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 22) + pieWidthHalf)) idx = 10;
        else if ((rotationDegrees > (pieWidth * 23) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 23) + pieWidthHalf)) idx = 9;
        else if ((rotationDegrees > (pieWidth * 24) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 24) + pieWidthHalf)) idx = 8;
        else if ((rotationDegrees > (pieWidth * 25) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 25) + pieWidthHalf)) idx = 7;
        else if ((rotationDegrees > (pieWidth * 26) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 26) + pieWidthHalf)) idx = 6;
        else if ((rotationDegrees > (pieWidth * 27) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 27) + pieWidthHalf)) idx = 5;
        else if ((rotationDegrees > (pieWidth * 28) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 28) + pieWidthHalf)) idx = 4;
        else if ((rotationDegrees > (pieWidth * 29) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 29) + pieWidthHalf)) idx = 3;
        else if ((rotationDegrees > (pieWidth * 30) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 30) + pieWidthHalf)) idx = 2;
        else if ((rotationDegrees > (pieWidth * 31) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 31) + pieWidthHalf)) idx = 1;
        else if ((rotationDegrees > (pieWidth * 32) - pieWidthHalf) && (rotationDegrees <= (pieWidth * 32) + pieWidthHalf)) idx = 0;

        if (rotationDegrees >= 0 && rotationDegrees < 180)
        {
            spriteRenderer.flipX = false;            
        }
        else
        {
            spriteRenderer.flipX = true;            
        }

        if (leftStickInput.y > 0)
        {
            if (Time.time > nextThrustTime)
            {
                isLowThrust = !isLowThrust;
                nextThrustTime = Time.time + 0.1f;
            }            

            if (isLowThrust)
            {
                spriteRenderer.sprite = lowFireRotationSprites[idx];
            }
            else
            {
                spriteRenderer.sprite = highFireRotationSprites[idx];
            }
        }
        else
        {
            spriteRenderer.sprite = basicRotationSprites[idx];
        }
    }

    public void Influence(Vector2 force)
    {
        rb2d.AddForce(force);
    }

    private void Tow()
    {
        if (isTowingInputDownThisFrame)
        {
            if (isTowing)            
            {
                isTowing = false;
                distanceJoint2d.connectedBody.bodyType = RigidbodyType2D.Static;
                distanceJoint2d.connectedBody = null;
                distanceJoint2d.enabled = false;
            }
            else
            {
                Collider2D[] collider2DArr = Physics2D.OverlapCircleAll(transform.position, TOW_DISTANCE);
                foreach (Collider2D collider2D in collider2DArr)
                {
                    if (collider2D.gameObject.layer == LayerMask.NameToLayer("fuel"))
                    {
                        isTowing = true;
                        distanceJoint2d.connectedBody = collider2D.gameObject.GetComponent<Rigidbody2D>();
                        distanceJoint2d.connectedBody.bodyType = RigidbodyType2D.Dynamic;
                        distanceJoint2d.distance = TOW_DISTANCE;
                        distanceJoint2d.enabled = true;
                        break;
                    }
                }
            }
        }

        if (isTowing)
        {
            lineRenderer.enabled = true;
            UpdateTowLine();
        }
        else
        {
            lineRenderer.enabled = false;
        }            
    }

    private void UpdateTowLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, distanceJoint2d.connectedBody.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectableComponent))
        {
            collectableComponent.Collect();
        }
    }
}
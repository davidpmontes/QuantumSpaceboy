using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceboy : MonoBehaviour, IGravityInfluenced, ITower, IPlayerDamageable
{
    private IPlayerInput playerInput;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] basicRotationSprites;
    [SerializeField] private Sprite[] lowFireRotationSprites;
    [SerializeField] private Sprite[] highFireRotationSprites;
    private int rotationIdx = 0;
    private float nextThrustTime;
    private bool isLowThrust;
    private float shipRotationRads;
    [SerializeField] private float rads;
    [SerializeField] private float rotationDegrees;

    private Vector2 thrust;

    private const float ROTATION_SPEED = 350f;
    private const float THRUST_SPEED = 500000;
    private const float THRUST_BOOST_SPEED_MULTIPLIER = 2f;
    private const float INVINCIBLE_TIME = 1f;

    private const float FUEL_USAGE_RATE = 0.03f;

    private const float TOW_DISTANCE_DETECTION = 3f;
    private const float TOW_DISTANCE_PHYSICAL = 2f;

    private bool isTowing = false;
    private FrictionJoint2D frictionJoint2D;
    private DistanceJoint2D distanceJoint2D;
    private LineRenderer lineRenderer;

    [SerializeField] private GameObject primaryWeaponGO;
    private IWeapon primaryWeapon;

    [SerializeField] private GameObject secondaryWeaponGO;
    private IWeapon secondaryWeapon;

    [SerializeField] private GameObject secondaryTarget;
    private float invincibleEndTime;
    private int idx;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        frictionJoint2D = GetComponent<FrictionJoint2D>();
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        primaryWeapon = primaryWeaponGO.GetComponent<IWeapon>();
        primaryWeapon.Init();
        secondaryWeapon = secondaryWeaponGO.GetComponent<IWeapon>();
        secondaryWeapon.Init();
    }

    void Update()
    {
        Rotate();
        CalculateThrust();
        DrawShip();        
        Fire();
        Secondary();
        UpdateTowing();
    }

    private void FixedUpdate()
    {
        Move();        
    }


    private void Fire()
    {
        if (playerInput.PrimaryFireInput())
        {
            primaryWeapon.Fire(shipRotationRads, rb2d, transform.position, secondaryTarget, idx);
        }
    }

    private void Secondary()
    {
        if (playerInput.SecondaryFireInput())
        {
            secondaryWeapon.Fire(shipRotationRads, rb2d, transform.position, secondaryTarget, idx);
        }
    }

    private void Rotate()
    {
        var leftStickInput = playerInput.GetLeftStickInput();

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
        var thrusterInput = playerInput.GetThrusterInput();

        var degs = rotationDegrees;
        rads = Mathf.Deg2Rad * (degs);

        float maxVelocityMagnitude = 20;
        Vector2 targetVelocity = rb2d.velocity + new Vector2(Mathf.Sin(rads), Mathf.Cos(rads)) * maxVelocityMagnitude;
        targetVelocity = Vector2.ClampMagnitude(targetVelocity, maxVelocityMagnitude);
        thrust = (targetVelocity - rb2d.velocity) * thrusterInput * 20000 * Time.fixedDeltaTime;

        shipRotationRads = Mathf.Deg2Rad * (rotationDegrees + 90);
        
        var fuelUsed = thrusterInput * FUEL_USAGE_RATE * Time.fixedDeltaTime;
        GameplayCanvasManager.Instance.UpdateFuelBar(idx, -fuelUsed);
    }

    private void Move()
    { 
        rb2d.AddForce(thrust);
    }

    private void DrawShip()
    {
        var thrusterInput = playerInput.GetThrusterInput();

        if (Time.time < invincibleEndTime)
        {
            if (spriteRenderer.color.a == 1)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            
        } else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }

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

        if (thrusterInput > 0)
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

    private void Tow(InputAction.CallbackContext ctx)
    {
        //if (playerInput.TowInput())
        //{
        if (isTowing) // Was towing so now disconnecting
        {
            isTowing = false;

            distanceJoint2D.connectedBody.GetComponent<ITowable>().StopTow();

            frictionJoint2D.connectedBody = null;
            frictionJoint2D.enabled = false;

            distanceJoint2D.connectedBody = null;
            distanceJoint2D.enabled = false;
        }
        else // was not towing so start towing
        {
            Collider2D[] collider2DArr = Physics2D.OverlapCircleAll(transform.position, TOW_DISTANCE_DETECTION);
            foreach (Collider2D collider2D in collider2DArr)
            {
                if (collider2D.gameObject.layer == LayerMask.NameToLayer("towable"))
                {
                    if (collider2D.gameObject.GetComponent<ITowable>().Tractored)
                    {
                        continue;
                    }

                    var towableRb2d = collider2D.gameObject.GetComponent<Rigidbody2D>();

                    if (towableRb2d.GetComponent<ITowable>().IsBeingTowed)
                    {
                        continue;
                    }

                    towableRb2d.GetComponent<ITowable>().StartTow(this);

                    isTowing = true;

                    frictionJoint2D.connectedBody = towableRb2d;
                    frictionJoint2D.autoConfigureConnectedAnchor = true;
                    frictionJoint2D.maxForce = 200;
                    frictionJoint2D.enabled = true;

                    distanceJoint2D.connectedBody = towableRb2d;
                    distanceJoint2D.distance = TOW_DISTANCE_PHYSICAL;
                    distanceJoint2D.enabled = true;
                    break;
                }
            }
        }
        //}
    }

    private void UpdateTowing()
    {
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
        lineRenderer.SetPosition(1, distanceJoint2D.connectedBody.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectableComponent))
        {
            collectableComponent.Collect(idx);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollideWithTerrain(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollideWithTerrain(collision);
    }

    private void CollideWithTerrain(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("bounds")) return;

        if (Time.time < invincibleEndTime)
        {
            return;
        }
        else
        {
            invincibleEndTime = Time.time + INVINCIBLE_TIME;
        }
        
        var dot = Mathf.Abs(Vector2.Dot(collision.GetContact(0).normal, rb2d.velocity.normalized));
        var fuelUsed = 1 + rb2d.velocity.magnitude * dot;
        GameplayCanvasManager.Instance.UpdateFuelBar(idx, -fuelUsed);

        for (int i = 0; i < 3; i++)
        {
            var damageEffect = ObjectPool.Instance.GetFromPoolInactive(Pools.DamageEffect);
            damageEffect.SetActive(true);
            damageEffect.GetComponent<DamageEffect>().Init(transform.position, Random.Range(0, 360), 3 + rb2d.velocity.magnitude * dot);
        }
    }

    public void SetPlayerInput(IPlayerInput playerInput)
    {
        this.playerInput = playerInput;
        playerInput.RegisterTowStartEvent(Tow);
    }

    public void SetIdx(int idx)
    {
        this.idx = idx;
    }

    public void RelinquishTow()
    {
        if (isTowing)
        {
            isTowing = false;
            lineRenderer.enabled = false;

            frictionJoint2D.connectedBody = null;
            frictionJoint2D.enabled = false;

            distanceJoint2D.connectedBody = null;
            distanceJoint2D.enabled = false;
        }
    }

    public void DealDamage(int value)
    {
        var fuelUsed = value;
        GameplayCanvasManager.Instance.UpdateFuelBar(idx, -fuelUsed);
    }
}
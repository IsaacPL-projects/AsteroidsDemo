using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement Parameters")]
    public float MAX_VELOCITY;
    public float velocity;
    public float speed;
    public float speedIncrement;
    public float rotationSpeed;
    public float inertiaModifier;


    [Header("Children Stuff")]
    public ProjectileController projectilePrefab;
    public int projectileXOffset;
    public int projectileYOffset;
    public float shootingTimer;
    private float _shootingTimerPlaceholder;
    public bool shootingFlag;
    public GameObject projectilesParent;

    private AudioSource _shoot1;
    public List<GameObject> projectileList;
    public CanvasController canvas;
    public GameObject fireImpulse;
    private float inertiaFriction;
    public int lifes;
    public int currentScore;
    float translation;

    private Transform _initialPosition;
    private float _initialSpeed;
    void Start()
    {
        projectileList = new List<GameObject>();
        _shoot1 = GetComponent<AudioSource>();
        _shootingTimerPlaceholder = shootingTimer;
        shootingFlag = true;
        fireImpulse.GetComponent<RectTransform>().localScale = new Vector2(1, 0);
        canvas.UpdateLifeText(lifes);
    }

    // Update is called once per frame
    void Update()
    {
        InputControl();
        RotationControl();

        if (shootingTimer <= 0)
        {
            shootingTimer = _shootingTimerPlaceholder;
            shootingFlag = true;
        }
        else

            shootingTimer -= Time.deltaTime;

        FireImpulseControl();
        //Debug.Log($"Position: {transform.GetChild(2).position} LocalPosition: {transform.GetChild(2).localPosition}");
        transform.Translate(0, translation, 0);
    }

    private void InputControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            IncreaseSpeed();

        // if (!Input.GetKey(KeyCode.UpArrow))
        //     PlayerInertia();

        if (Input.GetKeyDown(KeyCode.Space) && shootingFlag)
            ShootProjectile();
    }

    private void RotationControl()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        rotation *= Time.deltaTime;

        transform.Rotate(0, 0, -rotation);
    }


    private void IncreaseSpeed()
    {
        if (velocity < MAX_VELOCITY)
        {
            speed += speedIncrement;

            velocity += speed * Time.deltaTime;
        }

        if (velocity > MAX_VELOCITY)
            velocity = MAX_VELOCITY;

        translation += Input.GetAxis("Vertical") * velocity;
        translation *= Time.deltaTime;
    }
    private void PlayerInertia()
    {
        if (speed > 0)
        {
            // inertiaFriction -= (velocity * Time.deltaTime / (inertiaFriction * Time.deltaTime)) * Time.deltaTime;
            speed -= speedIncrement;
            velocity -= speed * Time.deltaTime;

            float translation = Input.GetAxis("Vertical") * velocity * Time.deltaTime;
            transform.Translate(0, translation, 0);

            if (speed <= 0)
                velocity = 0;
        }
    }

    private void ShootProjectile()
    {
        ProjectileController projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, projectilesParent.transform.position.z), transform.rotation);
        projectile.transform.SetParent(projectilesParent.transform);
        projectile.canvas = canvas;
        shootingFlag = false;
        _shoot1.Play();
    }

    void FireImpulseControl()
    {
        RectTransform rectTransform = fireImpulse.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.Lerp(new Vector2(0, 0), new Vector2(1, 2), (velocity - 40) * Time.deltaTime);
        // Debug.Log(new Vector2(velocity, velocity * 2).normalized);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Asteroid"))
        {
            lifes--;
            canvas.UpdateLifeText(lifes);
            Destroy(other.gameObject);
        }
    }

    public void ResetPlayerVariables()
    {
        transform.position = new Vector3(0, 0, gameObject.transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        lifes = 3;
        velocity = 0;
        speed = 0;
        shootingFlag = false;
        shootingTimer = 0.25f;
    }
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     Destroy(gameObject);
    // }
}




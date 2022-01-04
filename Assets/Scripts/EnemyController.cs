using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject playerController;
    public ProjectileController projectilePrefab;
    public float shootingTimer;
    private float _shootingTimerPlaceholder;
    public bool isPlayerInsideShootingArea;
    public bool shootingFlag;
    public float rotationSample;
    public float velocityTowardsPlayer;

    private AudioSource _shoot1;
    // Start is called before the first frame update
    void Start()
    {
        shootingFlag = true;
        _shoot1 = GetComponent<AudioSource>();
        _shootingTimerPlaceholder = shootingTimer;
        playerController = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInsideShootingArea)
        {
            if (shootingFlag)
                ShootProjectile();

            if (shootingTimer <= 0)
            {
                shootingTimer = _shootingTimerPlaceholder;
                shootingFlag = true;
            }
            else

                shootingTimer -= Time.deltaTime;
        }

        transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, velocityTowardsPlayer);
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, transform.position,
        Quaternion.Euler(0, 0, Mathf.Atan2(transform.position.x - playerController.transform.position.x,
         transform.position.y - playerController.transform.position.y) * -Mathf.Rad2Deg + 180));
        shootingFlag = false;
        _shoot1.Play();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}

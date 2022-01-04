using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeSpan;
    private float _timer;
    public CanvasController canvas;
    private AudioSource explosion;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector2 velocity = new Vector2(0, speed * Time.deltaTime);
        position += transform.rotation * velocity;

        transform.position = position;

        if (_timer > lifeSpan)
            Destroy(gameObject);


        _timer += Time.deltaTime;

        if (transform.localScale.y < 50f)
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + 100f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (/*transform.parent != null && transform.parent.name.Equals("Projectiles")*/gameObject.name.Contains("ProjectilePlayer"))
        {
            if (other.name.Contains("Asteroid"))
            {
                AsteroidController asteroid = other.GetComponent<AsteroidController>();

                asteroid.HP -= damage;

                if (asteroid.HP <= 0)
                {
                    canvas.UpdateScoreText(asteroid.destructionScore);
                    canvas.currentAsteroids.Remove(asteroid);
                    Destroy(asteroid);
                    explosion.Play();
                }
            }

            if (other.name.Contains("UFO"))
            {
                {
                    Destroy(other.gameObject);
                }
            }
        }
        else
        {
            if (other.name.Contains("Player"))
            {
                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

                player.lifes--;
            }
        }

    }
}
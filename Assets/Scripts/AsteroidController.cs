using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{
    public int HP;
    public int destructionScore;
    public Vector2 startPosition;
    const string ASTEROID_IDLE = "Asteroid_idle";
    const string ASTEROID_EXPLOSION = "Asteroid_explosion";
    private Animator _animator;
    // private Rigidbody2D _rigidbody2D;
    private int _randomRotationDirection;

    public int minVelocity, maxVelocity;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private bool doesAsteroidRotate;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0, 361));
        // _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _randomRotationDirection = Random.Range(0, 10) % 2 == 0 ? 1 : -1;
        doesAsteroidRotate = Random.Range(0, 2) % 2 == 0 ? true : false;

        int aux = Random.Range(1, 4);
        HP = aux;
        destructionScore = 50 * aux;
        transform.localScale = new Vector2(aux, aux) * 0.5f;

        var a = gameObject.transform.GetChild(0).GetComponent<RawImage>();

        if (aux == 2)
        {
            gameObject.transform.GetChild(0).GetComponent<RawImage>().color = new Color(176f / 255, 135f / 255, 72f / 255);
        }
        else if (aux == 3)
            gameObject.transform.GetChild(0).GetComponent<RawImage>().color = new Color(236f / 255, 155f / 255, 210f / 255);

        aux = Random.Range(minVelocity, maxVelocity);
        velocity = new Vector2(aux, aux) * _randomRotationDirection;

        StartCoroutine(AlphaIncrease());
    }

    // Update is called once per frame
    void Update()
    {
        if (doesAsteroidRotate)
            transform.Rotate(new Vector3(0, 0, minVelocity * Time.deltaTime * _randomRotationDirection));
        // _rigidbody2D.MovePosition(new Vector2(transform.position.x + 1000 * Time.deltaTime, transform.position.y + 1000 * Time.deltaTime));
        transform.position = new Vector2(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime);
    }

    IEnumerator AlphaIncrease()
    {
        Color color = transform.GetChild(0).GetComponent<RawImage>().color;
        for (float alpha = 0; alpha <= 1f; alpha += 0.05f)
        {
            color.a = alpha;
            transform.GetChild(0).GetComponent<RawImage>().color = color;
            yield return null;
        }
    }

    public void DestroyAsteroid()
    {
        Destroy(gameObject);
    }
    // IEnumerator Destroy()
    // {
    //     Color c = renderer.material.color;
    //     for (float i = 1f; i >= 0; i -= 0.1f)
    //     {
    //         c.a = alpha;
    //         renderer.material.color = c;
    //         yield return null;
    //     }
    // }

}

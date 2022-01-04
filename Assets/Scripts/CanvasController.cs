using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasController : MonoBehaviour
{
    [Header("Asteroid settings")]
    public AsteroidController asteroidPrefab;
    public List<AsteroidController> currentAsteroids;
    public GameObject asteroidsParent;
    public int maxAsteroids;
    public Vector2 destructionOffset;
    public float minAsteroidVelocity;
    public float maxAsteroidVelocity;
    [Header("Ufo settings")]
    public EnemyController ufoPrefab;
    public List<EnemyController> currentUfos;
    public GameObject ufosParent;
    public int maxUfos;
    public float spawnXOffset;
    public float spawnYOffset;
    private BoxCollider2D _bgTriggerArea;
    private Vector2 _teleportDestination;
    private RectTransform _recTransform;
    private int _asteroidNumber;
    public PlayerController player;
    public GameObject startScreen;
    public GameObject gameScene;
    public GameObject gameOverScreen;
    private float delayItemInstances;

    [SerializeField]
    private const float timerCleanAsteroidsOffset = 2f;

    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        _bgTriggerArea = GetComponent<BoxCollider2D>();
        currentAsteroids = new List<AsteroidController>();
        currentUfos = new List<EnemyController>();
        _recTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                startScreen.SetActive(false);
                gameScene.SetActive(true);
            }

        }

        if (gameScene.activeInHierarchy)
        {
            delayItemInstances += Time.time / 200;

            if (delayItemInstances >= 1f && asteroidsParent.transform.childCount < maxAsteroids)
                InstanceAsteroid(maxAsteroids - asteroidsParent.transform.childCount);

            if (delayItemInstances >= 10f && ufosParent.transform.childCount < maxUfos)
                InstanceUfo();

            if (Time.time > timerCleanAsteroidsOffset)
                CleanLostAsteroids();


            if (player.lifes <= 0)
            {
                gameScene.SetActive(false);
                gameOverScreen.SetActive(true);
            }
        }

        if (gameOverScreen.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                player.ResetPlayerVariables();
                gameOverScreen.SetActive(false);
                startScreen.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.name.Equals("TeleportTrigger"))
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y) * -1;

            // Debug.Log(_player.transform.position + "   -   " + _player.transform.position * -1);
        }
    }

    public void UpdateLifeText(int currentLife)
    {
        UI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText($"LIFES: {currentLife}");
    }

    public void UpdateScoreText(int score)
    {
        UI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText($"Score: {player.currentScore += score}");
    }

    void InstanceUfo()
    {
        EnemyController ufo = Instantiate(ufoPrefab, RandomSpawnLocation(), Quaternion.identity);

        ufo.transform.SetParent(ufosParent.transform);

    }
    void InstanceAsteroid(int asteroidSlots)
    {
        do
        {
            AsteroidController asteroid = Instantiate(asteroidPrefab, RandomSpawnLocation(), Quaternion.Euler(0, 0, Random.Range(0, 360 + 1)));

            currentAsteroids.Add(asteroid);

            _asteroidNumber++;

            asteroid.name = $"Asteroid {_asteroidNumber}";

            asteroid.transform.SetParent(asteroidsParent.transform);

            // asteroid.GetComponent<Rigidbody2D>().MovePosition(new Vector2(Random.Range(minAsteroidVelocity, maxAsteroidVelocity) * RandomDirection(), Random.Range(minAsteroidVelocity, maxAsteroidVelocity) * RandomDirection()));

            //asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2());
            asteroidSlots--;
        } while (asteroidSlots > 0);
    }

    int RandomDirection()
    {
        return Random.Range(0, 10) % 2 == 0 ? 1 : -1;
    }

    private Vector2 RandomSpawnLocation()
    {
        float xCoordinate = Random.Range(_recTransform.position.x - _recTransform.rect.width / 2, _recTransform.position.x + _recTransform.rect.width / 2);
        float yCoordinate = Random.Range(_recTransform.position.y - _recTransform.rect.height / 2, _recTransform.position.y + _recTransform.rect.height / 2);

        // Debug.Log("x: " + xCoordinate + " y: " + yCoordinate);
        return new Vector2(yCoordinate, xCoordinate);
    }

    private Vector2 RandomizeAsteroidVelocity()
    {
        Vector2 result = new Vector2();

        return result;
    }

    void CleanLostAsteroids()
    {
        foreach (Transform asteroid in asteroidsParent.transform)
        {
            Vector2 asteroidStartPosition = asteroid.gameObject.GetComponent<AsteroidController>().startPosition;
            Vector2 asteroidCurrentPosition = asteroid.position;

            if ((Mathf.Abs(asteroidCurrentPosition.x) - Mathf.Abs(_recTransform.rect.width) / 2) >= (Mathf.Abs(asteroidStartPosition.x) + Mathf.Abs(_recTransform.rect.width) / 2)
             || ((Mathf.Abs(asteroidCurrentPosition.y) - Mathf.Abs(_recTransform.rect.height) / 4) >= (Mathf.Abs(asteroidStartPosition.y) + Mathf.Abs(_recTransform.rect.height) / 4)))
            {
                //     Debug.Log(Mathf.Abs(asteroidCurrentPosition.x - _recTransform.rect.width) + " " + (asteroidStartPosition.x + _recTransform.rect.width)
                //  + " " + Mathf.Abs(asteroidCurrentPosition.y - _recTransform.rect.height) + " " + (asteroidStartPosition.y + _recTransform.rect.height));
                // int index = currentAsteroids.IndexOf(asteroid.gameObject.GetComponent<AsteroidController>());
                // currentAsteroids.RemoveAt(index);
                // GameObject currentAsteroidChild = asteroidsParent.transform.GetChild(index).gameObject;
                // GameObject currentAsteroidChild = asteroidsParent.transform.GetChild(index).gameObject;

                currentAsteroids.Remove(asteroid.gameObject.GetComponent<AsteroidController>());
                // asteroid.gameObject.GetComponent<AsteroidController>().DestroyAsteroid();
                Destroy(asteroid.gameObject);
            }
            // asteroid.gameObject.GetComponent<AsteroidController>().DestroyAsteroid();
        }
    }
}

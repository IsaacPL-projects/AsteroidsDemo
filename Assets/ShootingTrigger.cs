using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyController ufoParent;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
            ufoParent.isPlayerInsideShootingArea = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
            ufoParent.isPlayerInsideShootingArea = false;
    }
}

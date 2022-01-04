using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController Player;
    public GameObject UI;
    private RectTransform _UIRectTransform;
    void Start()
    {
        _UIRectTransform = UI.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 newPosition = new Vector2(Player.transform.position.x, Player.transform.position.y);
        // transform.position = new Vector3(newPosition.x, newPosition.y, -100);
        // _UIRectTransform.position = newPosition;
    }
}

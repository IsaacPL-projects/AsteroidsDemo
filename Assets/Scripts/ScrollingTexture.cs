using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    // [SerializeField]
    // private float _scrollX = 0.5f;
    // [SerializeField]
    // private float _scrollY = 0.5f;
    public PlayerController player;
    public float velocityDividerDecrement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.velocity > 0)
        {
            float offsetX = Time.time * player.transform.position.x / velocityDividerDecrement;
            float offsetY = Time.time * player.transform.position.y / velocityDividerDecrement;

            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }

    public void MoveTexture()
    {

    }
}

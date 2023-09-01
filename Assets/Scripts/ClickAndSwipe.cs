using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer),typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private Camera myCamera;
    private TrailRenderer trail;
    private BoxCollider col;
    private Vector3 mousePos;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        myCamera = Camera.main;

        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();

        trail.enabled = false;
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                trail.enabled = true;
                col.enabled = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                trail.enabled = false;
                col.enabled = false;
            }

            if (trail.enabled)
            {
                mousePos = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                transform.position = mousePos;
            }
        }
    }

    void updateComponent()
    {
        
    }

    
}

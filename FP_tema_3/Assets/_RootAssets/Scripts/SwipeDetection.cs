using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    [SerializeField] private float maxMoveDistance = 1f;
    [SerializeField] private float distanceMultiplyer = 2f;
    
    [SerializeField] private float minMoveThreshold = 0.1f;

    private InputManager inputManager;


    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 currentTouchPosition;

    private Vector2 initialPosition;

    private bool isTouchOn = false;
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void Start()
    {
        initialPosition = transform.position;
        // print(initialPosition);
        // StartCoroutine(ShowCurrentTouchPositionCoroutine());
    }

    private void Update()
    {
        ProcessSwipe();
    }


    private void SwipeStart(Vector2 position, float time)
    {
        isTouchOn = true;
        transform.position = initialPosition;
        startTouchPosition = position;
        // print($"Started touch at x:{position.x} | y:{position.y}");
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        isTouchOn = false;
        transform.position = initialPosition;
        endTouchPosition = position;
        // print($"Ended touch at x:{position.x} | y:{position.y}");
    }


    private void ProcessSwipe()
    {
        if (!isTouchOn) 
            return;
        
        currentTouchPosition = inputManager.TouchPosition();

        if (Vector2.Distance(currentTouchPosition, startTouchPosition) < minMoveThreshold)
            return;
        
        var diffPosRaw = currentTouchPosition - startTouchPosition;
        var movePos = Vector2.ClampMagnitude(diffPosRaw * distanceMultiplyer, maxMoveDistance);
            
        transform.position = initialPosition + movePos;
    }


    private IEnumerator ShowCurrentTouchPositionCoroutine()
    {
        while (true)
        {
            print($"Current touch at x:{currentTouchPosition.x} | y:{currentTouchPosition.y}");
            yield return new WaitForSeconds(1);
        }
    }
    
}

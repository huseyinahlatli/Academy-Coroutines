using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestCoroutines : MonoBehaviour
{
    [SerializeField] private Transform[] path;
    private IEnumerator _currentMoveCoroutine;
    private string[] messages = { "Welcome", "to", "this", "amazing", "game" };
    
    private void Start()
    {
        StartCoroutine(PrintMessages(messages, .5f));
        StartCoroutine(FollowPath());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_currentMoveCoroutine != null)
                StopCoroutine(_currentMoveCoroutine);
            
            _currentMoveCoroutine = Move(Random.onUnitSphere * 5, 8f);
            StartCoroutine(_currentMoveCoroutine);
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
            StopAllCoroutines();
    }

    private IEnumerator FollowPath()
    {
        foreach (Transform waypoint in path)
            yield return StartCoroutine(Move(waypoint.position, 8));
    }
    
    private IEnumerator Move(Vector3 destination, float speed)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
    
    private IEnumerator PrintMessages(string[] message, float delay)
    {
        foreach (string msg in message)
        {
            print(msg);
            yield return new WaitForSeconds(delay);
        }
    }
}

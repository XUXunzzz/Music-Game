using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    public float speed;
    public bool canMove = false;

    private void Start()
    {
        StartCoroutine(nameof(DestoryBox));
    }
    IEnumerator DestoryBox()
    {
        yield return new WaitForSecondsRealtime(5f);
        Destroy(gameObject);
    }
    private void Update()
    {
        UpMove();
    }
    private void UpMove()
    {
        
        if(canMove)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}

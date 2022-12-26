using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryNote : MonoBehaviour
{
    [SerializeField] VoidEventChannel MissEventChannel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            MissEventChannel.Broadcast();
            Destroy(collision.gameObject);
        }
    }
}

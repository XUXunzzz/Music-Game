using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackDownController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite pressSprite;
    [SerializeField] Sprite defaultSperite;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
            spriteRenderer.sprite = pressSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
            spriteRenderer.sprite = defaultSperite;
    }
}

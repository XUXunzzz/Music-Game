using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer SR;

    [SerializeField] KeyCode keyCode;
    [SerializeField] Sprite buttonImage;
    [SerializeField] Sprite defaultImage;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {//按下按钮后反馈

        if (Input.GetKeyDown(keyCode)) SR.sprite = buttonImage;
        if (Input.GetKeyUp(keyCode)) SR.sprite = defaultImage;
    }
}

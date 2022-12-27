using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDanceController : MonoBehaviour
{
    [SerializeField] VoidEventChannel FirstDanceEventChannel;
    [SerializeField] VoidEventChannel SecondDanceEventChannel;
    private Animator anim;
    private bool firstAction = false, secondAction = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        FirstDanceEventChannel.AddListener(FirstDance);
        SecondDanceEventChannel.AddListener(SecondDance);
    }
    private void OnDisable()
    {
        FirstDanceEventChannel.RemoveListener(FirstDance);
        SecondDanceEventChannel.RemoveListener(SecondDance);
    }

    private void Update()
    {
        PlayAction();
    }
    private void PlayAction()
    {
        if(firstAction)
        {
            //anim.Play("FirstAction");
            Debug.Log($"{this.gameObject.name}�����˵�һ������");
            firstAction = false;
        }
        if(secondAction)
        {
            //anim.Play("SecondAction");
            Debug.Log($"{this.gameObject.name}�����˵ڶ�������");
            secondAction = false;
        }
    }
    public void FirstDance()
    {
        firstAction = true;
    }
    public void SecondDance()
    {
        secondAction = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSFXManager : MonoBehaviour
{
    public AudioSource VoicePlayer { get; private set; }
    public static SecondSFXManager _instance;
    public static SecondSFXManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        VoicePlayer = GetComponent<AudioSource>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSFXManager : MonoBehaviour
{
    public static FirstSFXManager _instance;
    public static FirstSFXManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public AudioSource VoicePlayer { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        VoicePlayer = GetComponent<AudioSource>();
    }

    
}

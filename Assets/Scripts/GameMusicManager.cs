using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    private AudioSource gameMusic;
    [SerializeField] VoidEventChannel gameOverEventChannel;

    private bool IsStart = false;
    private void Start()
    {
        gameMusic = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(gameMusic.isPlaying && !IsStart)
        {
            Debug.Log("游戏开始");
            IsStart = true;
            StartCoroutine(AudioPlayFinished(gameMusic.clip.length));
        }
    }
    IEnumerator AudioPlayFinished(float time)
    {
        yield return new WaitForSeconds(time);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("游戏结束");
        gameMusic.Stop();
        gameOverEventChannel.Broadcast();
    }
}

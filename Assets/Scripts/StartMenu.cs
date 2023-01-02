using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] AudioSource startMenuMusicPlayer;//游戏开始菜单音乐
    [SerializeField] AudioSource gameMusicAudioSource;//游戏游玩音乐
    [SerializeField] AudioSource changeAudioSource;//切换时的音效

    //UI
    [SerializeField] AnimationCurve showCurve;
    [SerializeField] AnimationCurve hideCurve;
    [SerializeField] GameObject startMenuUI;
    [SerializeField] float showSpeed;
    [SerializeField] List<GameObject>playUI;

    //音效
    [SerializeField] AudioClip changeSFX;
    private void Awake()
    {
        startMenuUI.SetActive(true);
        StartCoroutine(nameof(StartMenuAnimationShow));
    }

    public void StartGame()
    {
        StartCoroutine(nameof(StartMenuAnimationHide));
        GameUI_Dis();
    }

    public void GameUI_Dis()
    {
        foreach(GameObject ui in playUI)
            ui.SetActive(true);
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    IEnumerator StartMenuAnimationShow()
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            startMenuUI.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * showSpeed;
            yield return null;
        }
    }
    IEnumerator StartMenuAnimationHide()
    {
        startMenuMusicPlayer.Stop();
        changeAudioSource.PlayOneShot(changeSFX);
        float timer = 0f;
        while(timer <= 1f)
        {
            startMenuUI.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * showSpeed;
            yield return null;
        }
        gameMusicAudioSource.Play();
        startMenuUI.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisposeScore : MonoBehaviour
{
    [SerializeField] Animation ScoreAnim;
    [SerializeField] Animation ComboAnim;
    [SerializeField] Text ScoreText;
    [SerializeField] Text ComboText;
    [SerializeField] Text RatioText; 

    [SerializeField] VoidEventChannel HitEventChannel;
    [SerializeField] VoidEventChannel MissEventChannel;

    private float ratio = 1.0f;//倍率
    private float score = 0f;//分数
    private float comboCount = 0;//连击次数
    public float oneNoteScore = 200f;

    private void OnEnable()
    {
        HitEventChannel.AddListener(HitDispose);
        MissEventChannel.AddListener(ClearComboCount);
    }
    private void OnDisable()
    {
        HitEventChannel.RemoveListener(HitDispose);
        MissEventChannel.RemoveListener(ClearComboCount);
    }
    private void Start()
    {
        GameInit();
    }
    private void Update()
    {
        CalculationOfScores();
    }
    public void GameInit()
    {
        ratio = 1.0f;
        score = 0f;
        comboCount = 0f;
    }
    private void CalculationOfScores()
    {
        ratio = (comboCount / 5f) * 0.1f + 1.0f;
        ratio = Mathf.Clamp(ratio, 1.0f, 2.0f);
        Debug.Log($"{ratio}");
        ScoreText.text = score.ToString();
        RatioText.text = ratio.ToString("0.0");
        ComboText.text = comboCount.ToString();
    }

    public void HitDispose()
    {
        ComboAnim.Play(ComboAnim.clip.name);
        ScoreAnim.Play(ScoreAnim.clip.name);
        comboCount++;
        score += ratio * oneNoteScore;
    }

    public void ClearComboCount()
    {
        comboCount = 0;
    }
}

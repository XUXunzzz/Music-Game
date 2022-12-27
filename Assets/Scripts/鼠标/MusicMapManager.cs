using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMapManager : MonoBehaviour
{
    [SerializeField] GameObject Rocker;//鼠标
    [SerializeField] float rockerMoveSpeed , lerpSpeed;//鼠标移动速度
    float Th = 1.6f, Tw = 1.6f;
    
    private void Start()
    {
        GameInit();
    }
    public void GameInit()
    {
        Rocker.transform.localPosition = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        MoveRocker();
    }
    private void MoveRocker()//鼠标移动
    {
        //Vector3 mouseDir = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0f).normalized;
        //Vector3 targetPos = Rocker.transform.localPosition;

        //targetPos += rockerMoveSpeed * mouseDir * Time.deltaTime;
        //if (mouseDir.magnitude >= .6f)
        //{
        //    if (IsInsideTheDiamond(targetPos))
        //    {
        //        Rocker.transform.localPosition = Vector3.Lerp(Rocker.transform.localPosition, targetPos, rockerMoveSpeed * Time.deltaTime);
        //    }
        //    else
        //        Debug.Log("无法移动");
        //}
        float mouseX = Mathf.Abs(Input.GetAxis("Mouse X")) < 0.2f? 0f : Input.GetAxis("Mouse X");
        float mouseY = Mathf.Abs(Input.GetAxis("Mouse Y")) < 0.2f? 0f : Input.GetAxis("Mouse Y");
        Vector3 mouseDir = new Vector3(mouseX, mouseY, 0f);

        Vector3 targetPos = Rocker.transform.localPosition;

        targetPos += rockerMoveSpeed * mouseDir * Time.deltaTime;
        if (mouseDir.magnitude >= 0.05f)
        {
            if (IsInsideTheDiamond(targetPos))
            {
                Rocker.transform.localPosition = Vector3.Lerp(Rocker.transform.localPosition, targetPos, lerpSpeed * Time.deltaTime);
            }
            else
                Debug.Log("无法移动");
        }
    }
    private bool IsInsideTheDiamond(Vector2 pos)
    {
        if (Mathf.Abs(pos.x * Th) + Mathf.Abs(pos.y * Tw) <= Th * Tw * 0.5f)
            return true;
        else
            return false;
    }
}

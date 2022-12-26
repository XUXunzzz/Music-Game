using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceNote_Right : DanceNote
{
    private void Update()
    {
        transform.position -= Vector3.right * noteSpeed * Time.deltaTime;
    }
}

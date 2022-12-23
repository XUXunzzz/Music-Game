using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOB : MonoBehaviour
{
    [SerializeField] VoidEventChannel HitEventChannel;
    [SerializeField] VoidEventChannel DanceEventChannel;
    public bool canPress = false;
    [SerializeField] KeyCode pressButton;
    [SerializeField] float noteSpeed;
    [SerializeField] AudioClip SFX;
    void Update()
    {
        if (Input.GetKeyDown(pressButton))
            if (canPress)
            {
                HitEventChannel.Broadcast();
                FirstSFXManager.Instance.VoicePlayer.PlayOneShot(SFX);
                Destroy(this.gameObject);
            }

         transform.position -= new Vector3(0, noteSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            if (!canPress)
                canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Activator"))
        {
            if (canPress)
                canPress = false;
        }
    }
}

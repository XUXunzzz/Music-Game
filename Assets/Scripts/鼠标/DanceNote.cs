using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceNote : MonoBehaviour
{
    [SerializeField] AudioClip SFX;
    [SerializeField] VoidEventChannel HitEventChannel;
    public VoidEventChannel DanceEventChannel;
    public bool canCrackDown = false;
    public float noteSpeed;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
            canCrackDown = true;

        if (collision.CompareTag("Mouse"))
            if (canCrackDown)
            {
                HitEventChannel.Broadcast();
                SecondSFXManager.Instance.VoicePlayer.PlayOneShot(SFX);
                DanceEventChannel.Broadcast();
                Destroy(this.gameObject);
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
            canCrackDown = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.MyDemo
{
    public class SphereJump : MonoBehaviour
    {
        [EventID]
        public string eventID;

        Animation anim;

        private void Awake()
        {
            anim = GetComponent<Animation>();
            Koreographer.Instance.RegisterForEvents(eventID, MusicJump);
        }
        private void MusicJump(KoreographyEvent koreoEvent)
        {
            anim.Stop("Jump");
            anim.Play("Jump");
        }
    }
}

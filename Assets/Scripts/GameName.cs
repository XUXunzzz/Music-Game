using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.MyDemo
{
    public class GameName : MonoBehaviour
    {
        [EventID]
        public string eventID;
        private Animation gameNameAnimation;
        // Start is called before the first frame update
        void Start()
        {
            gameNameAnimation = GetComponent<Animation>();
            Koreographer.Instance.RegisterForEvents(eventID, GameNameJump);
        }

        private void GameNameJump(KoreographyEvent koreoEvent)
        {
            gameNameAnimation.Stop();
            gameNameAnimation.Play();
        }

        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.MyDemo
{
    public class Test : MonoBehaviour
    {
        [EventID]
        public string eventID;
        public GameObject mousicalNote;

        // Start is called before the first frame update
        void Start()
        {
            Koreographer.Instance.RegisterForEvents(eventID, MyAction);
        }

        private void MyAction(KoreographyEvent koreoEvent)
        {
            GameObject note = Instantiate(mousicalNote, transform.position, Quaternion.identity);
            note.GetComponent<BoxMove>().canMove = true;
        }

        void OnDestroy()
        {
            // Sometimes the Koreographer Instance gets cleaned up before hand.
            //  No need to worry in that case.
            if (Koreographer.Instance != null)
            {
                Koreographer.Instance.UnregisterForAllEvents(this);
            }
        }
    }
}

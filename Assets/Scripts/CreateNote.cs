using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.MyDemo
{
    public class CreateNote : MonoBehaviour
    {
        [EventID]
        public string eventID;
        [SerializeField] GameObject[] notes;
        [SerializeField] Transform[] noteCreatePos;

        Dictionary<Transform, GameObject> noteDic = new Dictionary<Transform, GameObject>();

        int noteType;
        private void Awake()
        {
            for(int i=0;i< 4;++i)
            {
                noteDic.Add(noteCreatePos[i], notes[i]);
            }
        }
        private void Start()
        {

            Koreographer.Instance.RegisterForEventsWithTime(eventID, NoteMap);
        }

        private void NoteMap(KoreographyEvent koreoEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
        {
            noteType = koreoEvent.GetIntValue();
            switch (noteType)
            {
                case 0:
                    Instantiate(noteDic[noteCreatePos[noteType]], noteCreatePos[noteType].position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(noteDic[noteCreatePos[noteType]], noteCreatePos[noteType].position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(noteDic[noteCreatePos[noteType]], noteCreatePos[noteType].position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(noteDic[noteCreatePos[noteType]], noteCreatePos[noteType].position, Quaternion.identity);
                    break;
                default:
                    //Debug.Log("ÓÒÂÖÅÌ³ö¼ü");
                    break;
            }
        }

        private void OnDestroy()
        {
            if(Koreographer.Instance !=null)
            {
                Koreographer.Instance.UnregisterForAllEvents(this);
            }
        }
    }

}
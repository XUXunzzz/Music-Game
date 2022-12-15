using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.MyDemo
{
    public class CreateMouseNote : MonoBehaviour
    {
        [SerializeField] GameObject[] notes;
        [SerializeField] Transform[] noteCreatePosTable;
        [EventID]
        public string eventID;
        private Dictionary<Transform, GameObject> noteDic = new Dictionary<Transform, GameObject>();

        private void Awake()
        {
            Koreographer.Instance.RegisterForEvents(eventID,CreateDanceNote);
            for (int i = 0; i < 4; ++i)
            {
                noteDic.Add(noteCreatePosTable[i], notes[i]);
            }
        }

        private void CreateDanceNote(KoreographyEvent koreoEvent)
        {
            int note = UnityEngine.Random.Range(0, 4);
            switch (note)
            {
                case 0:
                    Instantiate(noteDic[noteCreatePosTable[note]], noteCreatePosTable[note].position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(noteDic[noteCreatePosTable[note]], noteCreatePosTable[note].position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(noteDic[noteCreatePosTable[note]], noteCreatePosTable[note].position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(noteDic[noteCreatePosTable[note]], noteCreatePosTable[note].position, Quaternion.identity);
                    break;
                default:
                    Debug.LogError("Éú³ÉÒô·û´íÎó");
                    break;
            }
        }
    }
}

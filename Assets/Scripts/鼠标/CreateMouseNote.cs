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
            //int note = UnityEngine.Random.Range(0, 2);
            int note = koreoEvent.GetIntValue();
            switch (note)
            {
                case 4:
                    Instantiate(noteDic[noteCreatePosTable[0]], noteCreatePosTable[0].position, noteDic[noteCreatePosTable[0]].transform.rotation);
                    break;
                case 5:
                    Instantiate(noteDic[noteCreatePosTable[1]], noteCreatePosTable[1].position, Quaternion.identity);
                    break;
                case 6:
                    Instantiate(noteDic[noteCreatePosTable[2]], noteCreatePosTable[2].position, noteDic[noteCreatePosTable[2]].transform.rotation);
                    break;
                case 7:
                    Instantiate(noteDic[noteCreatePosTable[3]], noteCreatePosTable[3].position, Quaternion.identity);
                    break;
                default:
                    //Debug.Log("×óÂÖÅÌ³ö¼ü");
                    break;
            }
        }
    }
}

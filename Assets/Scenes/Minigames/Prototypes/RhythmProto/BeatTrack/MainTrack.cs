﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RythumProto.BeatTrack
{
    public class MainTrack : MonoBehaviour
    {
        // Spawning Points
        [SerializeField] SpawnNotes leftTrack   = null;
        [SerializeField] SpawnNotes downTrack   = null;
        [SerializeField] SpawnNotes upTrack     = null;
        [SerializeField] SpawnNotes rightTrack  = null;


        // Input
        public BeatTrackSource beatSource;

        // Queue for Beats
        Queue<NoteBinary> beatTrack = new Queue<NoteBinary>();

        private void Start()
        {
            InsertIntoQueue();
        }


        // Prepare Queue
        void InsertIntoQueue()
        {
            foreach (NoteBinary note in beatSource.beatTrackSource)
            {
                beatTrack.Enqueue(note);
            }
        }


        // Spawn Beat
        public void NextBeat()
        {
            //End Song Without Beats
            if (beatTrack.Count == 0)
            {
                BPMTempo.currentTempo.active = false;
                return;
            }

            // Set Spawn Note
            NoteBinary note = beatTrack.Dequeue();



            // Spawn
            if (note.left)  leftTrack.Spawn();
            if (note.down)  downTrack.Spawn();
            if (note.up)    upTrack.Spawn();
            if (note.right) rightTrack.Spawn();
        }
    }
}
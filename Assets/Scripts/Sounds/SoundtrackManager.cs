using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    [SerializeField] AudioClip[] Stages;

    AudioSource player;
    int stageIndex;

    private void Start()
    {
        player = GetComponent<AudioSource>();
        stageIndex = 0;
    }

    public void AdvanceToNextStage()
    {
        stageIndex = Math.Min(stageIndex + 1, Stages.Length - 1);
    }

    private void Update()
    {
        if (!player.isPlaying)
            player.PlayOneShot(Stages[stageIndex]);
    }
}

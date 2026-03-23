using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioListener _listener;

    public void ChangeVolume(float value)
    {
        ///_listener.volume = value;
    }
}

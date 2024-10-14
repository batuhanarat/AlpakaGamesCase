using Game.Managers;
using UnityEngine;

public class AudioManager : MonoBehaviour, IProvidable
{
    private void Awake()
    {
        ServiceProvider.Register(this);
    }

    public void PlayAudio(AudioClip audioClip) {
        //audioClip
    }
}
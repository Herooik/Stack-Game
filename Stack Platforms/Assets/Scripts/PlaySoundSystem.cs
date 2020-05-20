using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundSystem : MonoBehaviour
{
    [SerializeField] private AudioClip putSound;
    [SerializeField] private AudioClip perfectMatchSound;
    
    public void PlayPutSound()
    {
        AudioSource.PlayClipAtPoint(putSound, transform.position);
    }
    
    public void PlayPerfectMatchSound()
    {
        AudioSource.PlayClipAtPoint(perfectMatchSound, transform.position);
    }
}

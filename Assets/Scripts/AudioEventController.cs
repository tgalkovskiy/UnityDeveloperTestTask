using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AudioEventController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip[] _clips = default;
    private Animator _animator;
    private AudioSource _audioSource;
    
    #endregion

    #region LifeCycle

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    #endregion

    #region PublicMetods

    public void PlaySoundClip(int index)
    {
        if (_audioSource.isPlaying && _clips.Length>0) return;
        _audioSource.PlayOneShot(_clips[index]);
    }

    public void StopSoundClip()
    {
        _audioSource.Stop();
    }

    #endregion
    
}

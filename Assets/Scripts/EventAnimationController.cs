
using System;
using UnityEngine;
using DG.Tweening;

public enum SurfaceType
{
    Opaque,
    Transparent
}
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EventAnimationController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip[] _clips = default;
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMesh = default;
    private Animator _animator;
    private AudioSource _audioSource;
    private float alpha = 1;
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
        if (_clips.Length <= 0 && _audioSource.isPlaying) return;
        _audioSource.PlayOneShot(_clips[index]);
    }

    public void StopSoundClip()
    {
        _audioSource.Stop();
    }

    public void ChangeAlphaMaterial(float endAlphaValue)
    {
        foreach (var I in _skinnedMesh)
        {
            Material _material = I.sharedMaterial;
            _material.SetFloat("_Surface",1);
            DOTween.To(()=> alpha, x=> alpha = x, endAlphaValue, 1f).OnUpdate((() =>
            {
                 _material.SetColor("_BaseColor", new Color(1,1,1,alpha));
                I.sharedMaterial = _material;
            }));
        }
    }
    
    #endregion
    
}

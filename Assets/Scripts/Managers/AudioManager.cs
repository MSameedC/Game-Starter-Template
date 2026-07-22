using System.Collections;
using Redcode.Pools;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Mixer")]
    [SerializeField] AudioMixer masterMixer;

    // Mixer groups
    private AudioMixerGroup sfxGroup;
    private AudioMixerGroup musicGroup;
    private AudioMixerGroup ambientGroup;

    // Audio sources
    [Header("Audio Sources")]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource ambientSource;

    // Pooling
    private Pool<AudioSource> sfxPool;
    private int poolCount = 10;

    // ---

    private void Start()
    {
        InitializePool();
        InitializeMixerGroups();
    }

    // Mixer groups
    private void InitializeMixerGroups()
    {
        var sfxGroups = masterMixer.FindMatchingGroups("Sfx");
        if (sfxGroups.Length > 0) sfxGroup = sfxGroups[0];

        var musicGroups = masterMixer.FindMatchingGroups("Music");
        if (musicGroups.Length > 0) musicGroup = musicGroups[0];

        var ambientGroups = masterMixer.FindMatchingGroups("Ambient");
        if (ambientGroups.Length > 0) ambientGroup = ambientGroups[0];
    }

    // Pooling
    private void InitializePool()
    {
        if (sfxSource == null) return;
        sfxPool = Pool.Create(sfxSource, poolCount).NonLazy();
        sfxPool.SetContainer(transform, false);
    }

    private IEnumerator ReturnWhenFinished(AudioSource source)
    {
        // Wait until the audio source stops playing
        while (source != null && source.isPlaying)
        {
            yield return null;
        }

        // If the source was destroyed because of a scene change, stop immediately
        if (source == null || sfxPool == null)
            yield break;

        source.clip = null;
        source.outputAudioMixerGroup = null;

        sfxPool.Take(source);
    }

    // Sound playback
    public void PlaySound(AudioClip clip, float pitch, float spatialBlend)
    {
        if (sfxPool == null || clip == null)
            return;

        var source = sfxPool.Get();

        if (source == null)
            return;

        // Apply data
        SetAudioSource(source, clip, sfxGroup, pitch, spatialBlend);

        // Play
        source.Play();

        // Return to pool when finished, saves memory allocation
        StartCoroutine(ReturnWhenFinished(source));
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null)
            return;

        var source = musicSource;

        if (source == null) return;
        // If the same track is already playing, don't restart it!
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        SetAudioSource(source, clip, musicGroup, 1, 0);

        // Play
        source.Play();
    }

    public void PlayAmbient(AudioClip clip)
    {
        if (ambientSource == null || clip == null)
            return;

        var source = ambientSource;

        if (source == null) return;
        if (ambientSource.clip == clip && ambientSource.isPlaying) return;

        SetAudioSource(source, clip, ambientGroup, 1, 0);

        // Play
        source.Play();
    }

    // Volume control
    public void SetMasterVolume(float volume) => masterMixer.SetFloat("MasterVolume", FloatToDb(volume));

    public void SetEffectsVolume(float volume) => masterMixer.SetFloat("EffectsVolume", FloatToDb(volume));

    public void SetMusicVolume(float volume) => masterMixer.SetFloat("MusicVolume", FloatToDb(volume));

    // Helper methods
    private float FloatToDb(float volume)
    {
        // Clamp to avoid log(0) and ensure valid range
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        // Convert linear volume to decibels to avoid weird volume scaling
        return Mathf.Log10(volume) * 20f;
    }

    private void SetAudioSource(AudioSource source, AudioClip clip, AudioMixerGroup group, float pitch, float spatialBlend)
    {
        source.clip = clip;
        source.outputAudioMixerGroup = group;
        source.pitch = pitch;
        source.spatialBlend = spatialBlend;
    }
}

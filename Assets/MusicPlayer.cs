using UnityEngine;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> playlist; // Drag & drop AudioClips here in Inspector
    private AudioSource audioSource;
    private int currentTrack = 0;

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        if (playlist.Count > 0)
        {
            audioSource.clip = playlist[0];
            audioSource.Play();
        }
    }

    private void Update()
    {
        // Play next track when current finishes
        if (!audioSource.isPlaying && playlist.Count > 0)
        {
            currentTrack = (currentTrack + 1) % playlist.Count;
            audioSource.clip = playlist[currentTrack];
            audioSource.Play();
        }
    }
}

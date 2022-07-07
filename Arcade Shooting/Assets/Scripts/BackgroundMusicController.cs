using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] backgroundMusic;

    void Awake()
    {
        RandomizeClipsSecuence();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.25f;
        StartCoroutine(playAudioSequentially());
    }

    void RandomizeClipsSecuence()
    {
        for (int i = backgroundMusic.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            AudioClip temp = backgroundMusic[i];
            backgroundMusic[i] = backgroundMusic[randomIndex];
            backgroundMusic[randomIndex] = temp;
        }
    }
    IEnumerator playAudioSequentially()
    {
        yield return null;

        //loop enternally
        while (true)
        {
            //1.Loop through each AudioClip
            for (int i = 0; i < backgroundMusic.Length; i++)
            {
                //2.Assign current AudioClip to audiosource
                audioSource.clip = backgroundMusic[i];

                //3.Play Audio
                audioSource.Play();

                //4.Wait for it to finish playing
                while (audioSource.isPlaying)
                {
                    yield return null;
                }

                //5. Go back to #2 and play the next audio in the backgroundMusic array
            }
        }
    }

    public void StopMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.Play();
    }
}

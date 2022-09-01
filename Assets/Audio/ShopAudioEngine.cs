using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShopAudioEngine : MonoBehaviour
{
    // Intro
    public AudioClip introSoundTrack;
    public int IntroLength = 8;

    // SoundTrack Audio sources
    public AudioClip[] soundTrack;

    // Outro (Game End)
    public AudioClip outroSoundTrack;

    // Character Audio Tracks
    public AudioClip[] characterSounds;

    // Item Sounds
    public AudioClip[] SFXSounds;

    // Current track playing
    public int AudioTrack;

    // Unity Audio handler
    public AudioSource audioData;
    public AudioSource characterAudioData;
    public AudioSource soundFXAudioData;
 
    // Counters
    public float bpm = 70;
    public int beatCount;
    public int barCount;

    // internal Parameters
    private float beatInterval;
    private float beatTimer;
    private bool beatHit;

    public bool introOver;
    private bool introPlaying;

    // Sequencer
    private int currentAudioTrackPlaying;


    public static ShopAudioEngine Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        audioData = audios[0];
        characterAudioData = audios[1];
        soundFXAudioData = audios[2];

//        audioData = GetComponent<AudioSource>();
        audioData.loop = true;

        // init
        barCount = 1;
        introOver = false;
        beatHit = false;
        introPlaying = false;

        // set the First Track (intro) to be played
        AudioTrack = -1;

        // set up audiosources
//        characterAudioSource = new AudioSource();
//        soundFXSource = new AudioSource();
    }

    // Update is called once per frame
    void Update()
    {
        BPMTimer();

        // Intro music first in any case
        if (IntroMusic()) Sequencer();
    
        // For testing purposes, loop through the tracks in Array
        if (Input.GetKeyDown("space"))
        {
            print(soundTrack.Length);
            print("space + schedule next audio!");

            // Set the next track to be scheduled
            AudioTrack += 1;

            if (AudioTrack > (soundTrack.Length - 1))
            {
                AudioTrack = 0;
            }
        }
        // Set the end song
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("Outro scheduled");
            AudioTrack = 99;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            print("Link");
            PlayCharacterAudio(1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            print("Zelda");
            PlayCharacterAudio(2);
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            print("Ganon");
            PlayCharacterAudio(3);
        }
    }

    // BPM and Time counter
    void BPMTimer()
    {
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
//            print("beat!");     // Debugging
            beatCount++;

            beatHit = true;

            barCount++;
            if (barCount > 4)
            {
                barCount = 1;
            }
        }
        else
        {
            beatHit = false;
        }
    }

    bool IntroMusic()
    {
        // Check if we are done with the intro music already
        if (introOver) return true;

        if (beatHit)
        {
            // If this is the first time here, play the intro music
            if (!introPlaying)
            {
                audioData.clip = introSoundTrack;
                AudioTrack = -1;
                currentAudioTrackPlaying = -1;
                introPlaying = true;
                audioData.Play(0);
                barCount = 1;
            }

            if (beatCount >= IntroLength)
            {
                introOver = true;
                AudioTrack = 0;
                return false;
            }
        }

        return false;
    }

    // Sound scheduling - change track (if needed) when we are on the first beat of a bar
    void Sequencer()
    {
        if (currentAudioTrackPlaying != AudioTrack)
        {
            if (beatHit)
            {
                if (barCount == 1)
                {
                    // Was the Outro Set
                    if (AudioTrack == 99)
                    {
                        audioData.clip = outroSoundTrack;
                        audioData.loop = false;
                    }
                    else
                    {
                        audioData.clip = soundTrack[AudioTrack];
                    }

                    currentAudioTrackPlaying = AudioTrack;
                    audioData.Play(0);
                }
            }
        }
    }

    // Character audio
    public void PlayCharacterAudio(int CharacterID)
    {
        if (CharacterID >=1 && CharacterID < (characterSounds.Length + 1))
        {
            characterAudioData.clip = characterSounds[CharacterID - 1];
            characterAudioData.loop = false;
            characterAudioData.Play(0);
        }
    }

    // Play item sound
    public void PlaySoundFX(int SoundFXID)
    {
        if (SoundFXID >= 1 && SoundFXID < (SFXSounds.Length + 1))
        {
            soundFXAudioData.clip = SFXSounds[SoundFXID - 1];
            soundFXAudioData.loop = false;
            soundFXAudioData.Play(0);
        }
    }
}

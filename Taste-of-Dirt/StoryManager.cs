// (~˘▾˘)~ spaghettiSyntax
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText = null;
    [SerializeField] private SO_State startingState = null;
    [SerializeField] private AudioSource shamisenClip = null;
    [SerializeField] private AudioSource footstepsClip1 = null;
    [SerializeField] private AudioSource footstepsClip2 = null;
    [SerializeField] private AudioSource lightFireWhooshClip = null;
    [SerializeField] private AudioSource fireClip = null;
    [SerializeField] private AudioSource deathClip = null;
    [SerializeField] private AudioSource coughDeathClip = null;
    [SerializeField] private AudioSource woodCrashDeathClip = null;
    [SerializeField] private AudioSource gunShotDeathClip = null;
    [SerializeField] private AudioSource whereClip = null;

    private bool wooshPlayed = false;
    private bool deathPlayed = false;
    private bool coughPlayed = false;
    private bool woodCrashPlayed = false;
    private bool gunShotPlayed = false;
    private bool wherePlayed = false;

    private SO_State so_State;

    private void Start()
    {
        wooshPlayed = false;
        deathPlayed = false;
        coughPlayed = false;
        woodCrashPlayed = false;
        gunShotPlayed = false;
        so_State = startingState;
        Prologue();
    }

    private void Update()
    {
        ManageState();

        if (so_State.ToString().Equals("ReturnToMain (SO_State)"))
        {
            fireClip.Stop();
            ResetBools();
            StopShamisenClip();
            SceneManager.LoadScene(0);
        }

        if (!so_State.ToString().Equals("P0_Prologue (SO_State)"))
        {
            StopShamisenClip();
        }

        if (so_State.ToString().Equals("E0_Prologue (SO_State)"))
        {
            if (!shamisenClip.isPlaying)
            {
                PlayShamisenClip();
            }            
        }

        // Footsteps Clips
        if (so_State.ToString().Equals("10F1_Listen (SO_State)"))
        {
            PlayFootStepsClip();
        }

        if (so_State.ToString().Equals("15F1_Push (SO_State)"))
        {
            StopFootStepsClip();
        }

        // Fire Clips
        if (so_State.ToString().Equals("13F1_Listen (SO_State)"))
        {
            StartFireClips();
        }

        // Cough Death Clips
        if (so_State.ToString().Equals("6F1_Move (SO_State)"))
        {
            PlayCoughDeathClip();
        }

        // Where Clip
        if (so_State.ToString().Equals("12F1_Listen (SO_State)"))
        {
            PlayWhereClip();
        }

        // Wood Crash Death Clips
        if (so_State.ToString().Equals("17F1_CallOutAgain (SO_State)"))
        {
            PlayWoodCrashDeathClip();
        }
        else if (so_State.ToString().Equals("18F1_Stop (SO_State)"))
        {
            PlayWoodCrashDeathClip();
        }
        else if (so_State.ToString().Equals("19F1_Stand (SO_State)"))
        {
            PlayWoodCrashDeathClip();
        }        

        // Gun Shot Death Clips
        if (so_State.ToString().Equals("12F1_CallOut (SO_State)"))
        {
            PlayGunShotDeathClip();
        }
        else if (so_State.ToString().Equals("14F1_CallOut (SO_State)"))
        {
            PlayGunShotDeathClip();
        }

        // Choice Death Clips
        if (so_State.ToString().Equals("X06F1_TheDark (SO_State)"))
        {
            PlayDeathClip();
        }
        else if (so_State.ToString().Equals("X012F1_TheDark (SO_State)"))
        {
            PlayDeathClip();
        }
        else if (so_State.ToString().Equals("X014F1_TheDark (SO_State)"))
        {
            PlayDeathClip();
        }
        else if (so_State.ToString().Equals("X017F1_TheDark (SO_State)"))
        {
            PlayDeathClip();
        }
        else if (so_State.ToString().Equals("X018F1_TheDark (SO_State)"))
        {
            PlayDeathClip();
        }

        // Reset Death Bool
        if (so_State.ToString().Equals("5F1_ContinueMoving (SO_State)"))
        {
            ResetDeathBool();
        }
        else if (so_State.ToString().Equals("11F1_Listen (SO_State)"))
        {
            ResetDeathBool();
        }
        else if (so_State.ToString().Equals("13F1_Listen (SO_State)"))
        {
            ResetDeathBool();
        }
        else if (so_State.ToString().Equals("16F1_CallOut (SO_State)"))
        {
            ResetDeathBool();
        }
        else if (so_State.ToString().Equals("17F1_Force (SO_State)"))
        {
            ResetDeathBool();
        }
    }

    private void Prologue()
    {
        storyText.text = so_State.GetStoryState();
        PlayShamisenClip();
    }

    private void ManageState()
    {
        SO_State[] nextState = so_State.GetNextState();

        for (int i = 0; i < nextState.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)
                || Input.GetKeyDown(KeyCode.Keypad1 + i))
            {
                so_State = nextState[i];
            }
        }

        storyText.text = so_State.GetStoryState();
    }

    private void StartFireClips()
    {
        if (!lightFireWhooshClip.isPlaying)
        {
            if (!wooshPlayed)
            {
                lightFireWhooshClip.Play();
                wooshPlayed = true;
            }       
        }

        if (!fireClip.isPlaying)
        {
            StartCoroutine(AudioFadeIn.FadeIn(fireClip, 3f));
        }
    }

    private void PlayFootStepsClip()
    {
        if (!footstepsClip1.isPlaying)
        {
            StartCoroutine(AudioFadeIn.FadeIn(footstepsClip1, 3f));
        }

        if (!footstepsClip2.isPlaying)
        {
            StartCoroutine(AudioFadeIn.FadeIn(footstepsClip2, 2f));
        }
    }

    private void StopFootStepsClip()
    {
        if (footstepsClip1.isPlaying)
        {
            StartCoroutine(AudioFadeOut.FadeOut(footstepsClip1, 3f));
        }

        if (!footstepsClip2.isPlaying)
        {
            StartCoroutine(AudioFadeOut.FadeOut(footstepsClip2, 2f));
        }
    }

    private void PlayShamisenClip()
    {
        if (!shamisenClip.isPlaying)
        {
            StartCoroutine(AudioFadeIn.FadeIn(shamisenClip, 15f));
        }
    }

    private void StopShamisenClip()
    {
        if (shamisenClip.isPlaying)
        {
            StartCoroutine(AudioFadeOut.FadeOut(shamisenClip, 200f));
        }
    }

    private void PlayWhereClip()
    {
        if (!whereClip.isPlaying)
        {
            if (!wherePlayed)
            {
                StartCoroutine(AudioFadeIn.FadeIn(whereClip, 0.5f));
                wherePlayed = true;
            }
        }
    }

    private void PlayDeathClip()
    {
        if (!deathClip.isPlaying)
        {
            if (!deathPlayed)
            {
                StartCoroutine(AudioFadeIn.FadeIn(deathClip, 0.5f));
                deathPlayed = true;
            }
        }

        ResetBools();
    }

    private void ResetBools()
    {
        wooshPlayed = false;
        coughPlayed = false;
        woodCrashPlayed = false;
        gunShotPlayed = false;
        wherePlayed = false;
    }

    private void ResetDeathBool()
    {
        deathPlayed = false;
    }

    private void PlayCoughDeathClip()
    {
        if (!coughDeathClip.isPlaying)
        {
            if (!coughPlayed)
            {
                StartCoroutine(AudioFadeIn.FadeIn(coughDeathClip, 0.2f));
                coughPlayed = true;
            }
        }
    }

    private void PlayWoodCrashDeathClip()
    {
        if (!woodCrashDeathClip.isPlaying)
        {
            if (!woodCrashPlayed)
            {
                StartCoroutine(AudioFadeIn.FadeIn(woodCrashDeathClip, 0.5f));
                woodCrashPlayed = true;
            }
        }
    }

    private void PlayGunShotDeathClip()
    {
        if (!gunShotDeathClip.isPlaying)
        {
            if (!gunShotPlayed)
            {
                StartCoroutine(AudioFadeIn.FadeIn(gunShotDeathClip, 0.1f));
                gunShotPlayed = true;
            }
        }
    }
}

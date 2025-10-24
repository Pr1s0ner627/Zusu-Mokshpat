using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource moveSfx, climbSfx, fallSfx;

    void Awake() => Instance = this;

    public void PlayMove() => moveSfx?.Play();
    public void PlayClimb() => climbSfx?.Play();
    public void PlayFall() => fallSfx?.Play();
}

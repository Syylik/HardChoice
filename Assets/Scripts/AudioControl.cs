using UnityEngine;
using UnityEngine.UI;

namespace Control.Audio
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] private Toggle musicToggle, soundToggle;

        private AudioSource music;

        private string soundStr = "Sound";
        private string musicStr = "Music";

        private bool playSounds = true;
        private bool playMusic = true;
        private void Start()
        {
            music = GameObject.FindGameObjectWithTag("BgMusic")?.GetComponent<AudioSource>();
            playSounds = PlayerPrefs.GetInt(soundStr, 1) == 0 ? false : true;
            playMusic = PlayerPrefs.GetInt(musicStr, 1) == 0 ? false : true;
            musicToggle.isOn = playMusic ? true : false;
            soundToggle.isOn = playSounds ? true : false;
        }
        public void SetSoundsState(bool state)
        {
            playSounds = state;
            PlayerPrefs.SetInt(soundStr, playSounds == true ? 1 : 0);
        }
        public void SetMusicState(bool state)
        {
            playMusic = state;
            PlayerPrefs.SetInt(musicStr, playMusic == true ? 1 : 0);
            if (music?.GetComponent<AudioSource>() != null)
            {
                if (playMusic) music.Play();
                else music.Stop();
            }
        }
        public void PlaySound(AudioSource sound)
        {
            if (playSounds)
            {
                sound.pitch = Random.Range(0.9f, 1.1f);
                sound.Play();
            }
        }
    }
}

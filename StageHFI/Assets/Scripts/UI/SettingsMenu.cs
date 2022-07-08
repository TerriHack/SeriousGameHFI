using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
    
        public Slider musicSlider;
        public Slider soundSlider;

        public void Start()
        {
            audioMixer.GetFloat("Music", out float musicValueForSlider);
            musicSlider.value = musicValueForSlider;

            audioMixer.GetFloat("Sound", out float soundValueForSlider);
            soundSlider.value = soundValueForSlider;
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("Music", volume);
        }

        public void SetSoundVolume(float volume)
        {
            audioMixer.SetFloat("Sound", volume);
        }

        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
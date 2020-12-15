using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaPlayerManager : MonoBehaviour
{
    #region Public Property
    public static MediaPlayerManager Instance { get; set; }
    public MediaPlayer mediaPlayer;


    [Header("Component")]
    public Text txtTime;
    public Slider slider;
    public Image iconPause;
    #endregion



    #region Private Property
    #endregion


    
    #region Monobehavior Callback    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if(!mediaPlayer.gameObject.activeInHierarchy)
        {
            return;
        }

        float time = (mediaPlayer.Info.GetDurationMs() / 1000f);
        int min = (int)(time / 60);
        int sec = (int)(time % 60);

        string totalMax = min.ToString("0#") + ":" + sec.ToString("0#");

        time = (mediaPlayer.Control.GetCurrentTimeMs() / 1000f);
        min = (int)(time / 60);
        sec = (int)(time % 60);

        string totalCurrent = min.ToString("0#") + ":" + sec.ToString("0#");

        txtTime.text = totalCurrent + " / " + totalMax;
        float value = mediaPlayer.Control.GetCurrentTimeMs() / mediaPlayer.Info.GetDurationMs();

        if(!Double.IsNaN(value) && !Double.IsInfinity(value))
        {
            slider.value = value;
        }
    }
    #endregion



    #region Public Method
    public void Restart()
    {
        mediaPlayer.Rewind(false);
        mediaPlayer.Play();
    }


    public void ToggleStartAndPause()
    {
        if(mediaPlayer.Control.IsPlaying())
        {
            mediaPlayer.Pause();
            iconPause.gameObject.SetActive(true);
        }
        else
        {
            mediaPlayer.Play();
            iconPause.gameObject.SetActive(false);
        }
    }
    #endregion



    #region Private Method
    #endregion
}

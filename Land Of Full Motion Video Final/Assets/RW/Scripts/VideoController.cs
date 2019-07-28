/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer displayVideoPlayer;
    public TextMesh timeDisplay;

    private string videoURL = "https://www.videvo.net/videvo_files/converted/2016_01/preview/Forest_15_3b_Videvo.mov47209.webm";
    private string videoFilePath = "/RW/Videos/ForestFlyover.webm";
    private VideoPlayer fullScreenVideoPlayer;
    private FirstPersonInteractor firstPersonInteractor;
    private bool displayVideoIsPaused = false;

    void Start()
	{
		videoFilePath = Application.dataPath + videoFilePath;
        firstPersonInteractor = GameObject.FindObjectOfType<FirstPersonInteractor>() as FirstPersonInteractor;
	}

    void Update()
    {
        //1
        if (fullScreenVideoPlayer && fullScreenVideoPlayer.isPlaying)
        {
            //2
            if (Input.GetKey(KeyCode.Escape))
            {
                //3
                fullScreenVideoPlayer.Stop();
                firstPersonInteractor.SetImmobile(false);
            }
        }

        //1
        if (displayVideoPlayer && timeDisplay)
        {
            DisplayTime();
        }
    }

    private void DisplayTime()
    {
        //1
        string minutes = Mathf.Floor ((int)displayVideoPlayer.time / 60).ToString ("00");
        string seconds = ((int)displayVideoPlayer.time % 60).ToString ("00");
        string lengthMinutes = Mathf.Floor ((int)displayVideoPlayer.clip.length / 60).ToString ("00");
        string lengthSeconds = ((int)displayVideoPlayer.clip.length % 60).ToString ("00");
        //2
        timeDisplay.text = minutes + ":" + seconds + " / " + lengthMinutes + ":" +
            lengthSeconds;
    }

    public void PlayInWorldVideo()
    {
        //1
        if (!displayVideoIsPaused) {
            //2
            displayVideoPlayer.playOnAwake = false;
            displayVideoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            displayVideoPlayer.url = videoFilePath;
            displayVideoPlayer.frame = 0;
            displayVideoPlayer.isLooping = true;
        }

        //3
        displayVideoPlayer.Play();
        //4
        displayVideoIsPaused = false;
    }

    public void PauseInWorldVideo()
    {
        //1
        if (displayVideoIsPaused)
        {
            //2
            displayVideoPlayer.Play();
            displayVideoIsPaused = false;
        }
        else
        {
            //3
            displayVideoPlayer.Pause();
            displayVideoIsPaused = true;
        }
    }

    public void StopInWorldVideo()
    {
        //1
        displayVideoPlayer.Stop();
        //2
        displayVideoIsPaused = false;
    }

    public void PlayFullScreenOnlineVideo()
	{
        StartFullScreenVideo(videoURL);
	}

    //1
    public void PlayFullScreenOfflineVideo()
    {
        StartFullScreenVideo(videoFilePath);
    }

    private void StartFullScreenVideo(string path)
    {
        //2
        if (fullScreenVideoPlayer)
        {
            Destroy(fullScreenVideoPlayer);
        }
        
        //3
        fullScreenVideoPlayer = Camera.main.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();

        //4
        firstPersonInteractor.SetImmobile(true);

        //5
        fullScreenVideoPlayer.playOnAwake = false;
        fullScreenVideoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        fullScreenVideoPlayer.targetCameraAlpha = 1F;
        fullScreenVideoPlayer.url = path;
        fullScreenVideoPlayer.frame = 0;
        fullScreenVideoPlayer.isLooping = false;

        //6
        fullScreenVideoPlayer.Play();
    }
}

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

public class WorldUIElement : MonoBehaviour
{
	public enum ButtonTypes
	{
		playFullScreenOnlineVideo,
		playFullScreenOfflineVideo,
		playInWorldVideo,
		pauseInWorldVideo,
		stopInWorldVideo
	}
	
	public AudioClip buttonSound;
	public GameObject displayMesh;
	public ButtonTypes buttonType = ButtonTypes.playFullScreenOnlineVideo;
	public Material materialHighlight;
	private Material materialNormal;
	private bool isActivated = false;
	private Renderer thisRenderer;
	private AudioSource audioSource;
	private Vector3 buttonStartPosition;
	private Vector3 buttonPressedPosition;
	private VideoController videoController;

	void Start()
	{
		videoController = GameObject.FindObjectOfType<VideoController>() as VideoController;
		thisRenderer = displayMesh.GetComponent<Renderer>();
		audioSource = GetComponent<AudioSource>();
		materialNormal = thisRenderer.material;
		buttonStartPosition = displayMesh.transform.localPosition;
		buttonPressedPosition = new Vector3(buttonStartPosition.x, buttonStartPosition.y - 0.05f, buttonStartPosition.z - 0.05f);
	}

	public void Interact()
	{
		if (!isActivated)
		{
			if (buttonType == ButtonTypes.playFullScreenOnlineVideo)
			{
				videoController.PlayFullScreenOnlineVideo();
			}
			else if (buttonType == ButtonTypes.playFullScreenOfflineVideo)
			{
				videoController.PlayFullScreenOfflineVideo();
			}
			else if (buttonType == ButtonTypes.playInWorldVideo)
			{
				videoController.PlayInWorldVideo();
			}
			else if (buttonType == ButtonTypes.pauseInWorldVideo)
			{
				videoController.PauseInWorldVideo();
			}
			else if (buttonType == ButtonTypes.stopInWorldVideo)
			{
				videoController.StopInWorldVideo();
			}

			isActivated = true;
			displayMesh.transform.localPosition = buttonPressedPosition;
			audioSource.PlayOneShot(buttonSound);
			thisRenderer.material = materialNormal;
			StartCoroutine(DeactivateButton());
		}
	}

	void OnMouseOver()
	{
		if (!isActivated)
		{
			thisRenderer.material = materialHighlight;
		}
	}

	void OnMouseExit()
	{
		thisRenderer.material = materialNormal;
	}

	private IEnumerator DeactivateButton()
	{
		yield return new WaitForSeconds(1.5f);
		displayMesh.transform.localPosition = buttonStartPosition;
		isActivated = false;
	}
}

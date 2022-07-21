using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
	public float FadeDuration;

	public Fader(float fadeDuraion)
	{
		FadeDuration = fadeDuraion;
	}

	public void FadeInCanvasGroup(CanvasGroup canvasGroup)
	{
		StartCoroutine(FadeCanvasGroupCoroutine(canvasGroup, 1));
	}

	public void FadeOutCanvasGroup(CanvasGroup canvasGroup)
	{
		StartCoroutine(FadeCanvasGroupCoroutine(canvasGroup, 0));
	}

	public IEnumerator FadeCanvasGroupCoroutine(CanvasGroup canvasGroup, float targetAlphaValue)
	{
		if (targetAlphaValue > 0)
		{
			canvasGroup.gameObject.SetActive(true);
		}

		float currentTime = 0;
		float startingAlpha = canvasGroup.alpha;

		while(currentTime < FadeDuration)
		{
			canvasGroup.alpha = Mathf.Lerp(startingAlpha, targetAlphaValue, currentTime / FadeDuration);
			currentTime += Time.deltaTime;
			yield return null;
		}

		canvasGroup.alpha = targetAlphaValue;
		UpdateCanvasGameObjectActiveState(canvasGroup);
	}

	private void UpdateCanvasGameObjectActiveState(CanvasGroup canvasGroup)
	{
		if (canvasGroup.alpha == 0)
		{
			canvasGroup.gameObject.SetActive(false);
		}
		else
		{
			canvasGroup.gameObject.SetActive(true);
		}
	}

	public void FadeInImage(Image image)
	{
		StartCoroutine(FadeImageCoroutine(image, 255));
	}

	public void FadeOutImage(Image image)
	{
		StartCoroutine(FadeImageCoroutine(image, 0));
	}

	public IEnumerator FadeImageCoroutine(Image image, float targetAlphaValue)
	{
		if (targetAlphaValue > 0)
		{
			image.gameObject.SetActive(true);
		}

		float currentTime = 0;
		float startingAlpha = image.color.a;

		while (currentTime < FadeDuration)
		{
			float newAlphaValue = Mathf.Lerp(startingAlpha, targetAlphaValue, currentTime / FadeDuration);
			image.color = new Color(image.color.r, image.color.g, image.color.b, newAlphaValue);
			currentTime += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlphaValue);
		UpdateImageGameObjectActiveState(image);
	}

	private void UpdateImageGameObjectActiveState(Image image)
	{
		if (image.color.a == 0)
		{
			image.gameObject.SetActive(false);
		}
		else
		{
			image.gameObject.SetActive(true);
		}
	}
}

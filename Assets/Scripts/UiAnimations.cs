using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class UiAnimations 
{
    public static IEnumerator AnimateFadeOut(Image image, float duration, Action callback)
    {
        Color newColor = image.color;
        
        while (image.color.a > 0)
        {
            newColor.a -= Time.deltaTime / duration;
            image.color = newColor;
            yield return null;
        }

        newColor.a = 0;
        image.color = newColor;
        
        callback();
    }
}

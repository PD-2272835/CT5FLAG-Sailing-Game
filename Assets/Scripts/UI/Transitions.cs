using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Transitions
{

    public static float EaseInOutSine(float x) 
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }


    public static IEnumerator FadeOut(Image obj, float duration)
    {
        obj.raycastTarget = true;
        for (float time = 0f; time <= duration; time += Time.deltaTime)
        {
            float progress = EaseInOutSine(time/duration);
            float t = Mathf.Lerp(0, 1, progress);
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, t);
            yield return null;
        }
        obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, 1f);
    }

    static public IEnumerator FadeIn(Image obj, float duration)
    {
        obj.raycastTarget = false;
        for (float time = 0f; time <= duration; time += Time.deltaTime)
        {
            float progress = EaseInOutSine(time/duration);
            float t = Mathf.Lerp(0, 1, progress);
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, 1-t);

            yield return null;
        }
        obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, 0f);
    }

}

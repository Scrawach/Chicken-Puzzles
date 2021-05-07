using System.Collections;
using UnityEngine;

public static class Smooth
{
    public delegate void ChangeFunction<in T>(T start, T end, float t);

    public static IEnumerator Change<T>(T start, T end, float speed, ChangeFunction<T> func)
    {
        var step = speed * Time.fixedDeltaTime;
        var t = 0f;

        while (t <= 1.0f)
        {
            t += step;
            func(start, end, t);
            yield return new WaitForFixedUpdate();
        }
    }
}
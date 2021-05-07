public static class Functions
{
    public static float Bezier(float start, float middle, float end, float t)
    {
        if (t <= 0) 
            return start;

        if (t >= 1)
            return end;

        return start * (1 - t) * (1 - t) + middle * 2 * t * (1 - t) + end * (t * t);
    }
}
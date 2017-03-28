using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenUtils
{

    public static bool CheckIfIsOnScreen(Vector2 position)
    {
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(position);

        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    public static bool CheckIfIsOnLeftOfTheCamera(Vector2 position)
    {
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(position);

        return screenPoint.x < 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtils
{
    /// <summary>
    /// Metodo para verificar se um objeto está na tela
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool IsOnScreen(Vector2 position)
    {
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(position);

        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
    /// <summary>
    /// Metodo para verificar se um objeto está do lado esquerdo da tela
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool IsOnLeftOfTheCamera(Vector2 position)
    {
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(position);

        return screenPoint.x < 0;
    }

    public static bool IsInside(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x && position.y > bounds.min.y && position.y < bounds.max.y;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct screen
{
    [SerializeField]public Color[] colors;
    int width, height;

    public screen(int _width, int _height)
    {
        colors = new Color[_width * _height];
        width = _width;
        height = _height;
    }

    private int xyToIndex(int x, int y)
    {
        return y * width + x;
    }

    public void setColor(int x, int y, Color c)
    {
        try
        {
            colors[xyToIndex(x, y)] = c;
        }
        catch
        {
            Debug.LogError("colors array too short for index");
        }
    }

    public Color getColor(int x, int y)
    {
        try
        {
            return colors[xyToIndex(x, y)];
        }
        catch
        {
            Debug.LogError("colors array too short for index");
            return Color.red;
        }
    }
}

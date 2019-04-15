using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteMaker : MonoBehaviour
{
    public static int divider = 3;


    Color[] colors;
    int width, height;

    public Vector2[] v;
    public Vector2 test;

    public void MakeSpriteOuttaVertices(Vector2[] vertices)
    {

        //v = vertices;

        width = Screen.width / divider;
        height = Screen.height / divider;

        print(width);
        print(height);

        float camSize = Camera.main.orthographicSize;

        screen theScreen = new screen(width, height);

        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

        // set the pixel values
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (checkPoint(vertices, j, i))
                {
                    theScreen.setColor(j, i, Color.white);
                }
                else
                {
                    theScreen.setColor(j, i, Color.clear);
                }
            }
        }

        texture.SetPixels(theScreen.colors);

        // Apply all SetPixel calls
        texture.Apply();


        Sprite s = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), height / (camSize * 2));

        GetComponent<SpriteMask>().sprite = s;

    }

    private void Start()
    {
        print(checkPoint(v, (int)test.x, (int)test.y));
    }

    bool checkPoint(Vector2[] vertices, int x, int y)
    {
        int collides = 0;
        for (int i = 1; i <= vertices.Length; i++)
        {
            Vector2 vi = i==vertices.Length?vertices[0]:vertices[i], vii = vertices[i - 1];
            Vector2 superior = Vector2.zero;
            bool both = vi.x >= x && vii.x >= x;

            if (both && ((vi.y >= y && vii.y <= y) || (vi.y <= y && vii.y >= y)))
            {
                collides++;
            }

            else
            {
                if (vi.x >= x)
                {
                    superior = vi;
                }
                else
                {
                    if (vii.x >= x)
                    {
                        superior = vii;
                    }
                    else
                    {
                        continue;
                    }
                }
                float m = Mathf.Abs((vi.y - vii.y) / (vi.x - vii.x));
                float mi = Mathf.Abs((superior.y - y) / (x - superior.x));

                if ((m >= mi) && ((vi.y >= y && vii.y <= y) || (vi.y <= y && vii.y >= y)))
                {
                    collides++;
                }
            }

        }

        if (collides % 2 == 0)
            return false;
        else
            return true;
    }
}

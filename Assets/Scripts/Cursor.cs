using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private static Resources rss;
    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        rss = FindObjectOfType<Resources>();
        cursor = Instantiate(rss.generalCursor);
        cursor.transform.SetParent(rss.displayCanvas.transform, false);
        GameObject.Find("Cursors").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 onMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        onMousePosition = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, onMousePosition * 100);

        cursor.transform.localPosition = onMousePosition;
    }
}

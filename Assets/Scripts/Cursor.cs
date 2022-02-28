using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private static Resources _rss;
    public GameObject _cursor;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        _rss = FindObjectOfType<Resources>();
        _cursor = Instantiate(_rss.generalCursor);
        _cursor.transform.SetParent(_rss.displayCanvas.transform, false);
        _rss.cursorMisc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 onMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        onMousePosition = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, onMousePosition * 100);

        _cursor.transform.localPosition = onMousePosition;
    }
}

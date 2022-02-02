using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static Resources rss;

    private Vector3 onRightClickPosition;
    private Vector3 destination;

    float currentMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            onRightClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destination = FreeMatrix.Utility.Convert.ToLocal(rss.displayCanvas.transform.localScale, onRightClickPosition * 100);


            rss.player.GetComponent<HeroManager>().allowMove = true;
        }
    }

    void FixedUpdate()
    {
        if (rss.player.GetComponent<HeroManager>().allowMove)
        {
            rss.player.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween.PointTo2D(destination, rss.player.transform.localPosition, rss.player.transform.localRotation));
            rss.player.transform.localPosition = Vector2.MoveTowards(rss.player.transform.localPosition, destination, rss.player.GetComponent<HeroManager>().moveSpeed * Time.deltaTime);

            if (Vector2.Distance(rss.player.transform.localPosition, destination) < 300 * Time.deltaTime) rss.player.GetComponent<HeroManager>().allowMove = false;
        }
    }
}

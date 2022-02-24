using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static Resources rss;
    private static SceneManager scene;
    private static Keybind keybind;

    private Vector3 onRightClickPosition;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        scene = FindObjectOfType<SceneManager>();
        keybind = FindObjectOfType<Keybind>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            onRightClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destination = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, onRightClickPosition * 100);


            rss.player.GetComponent<HeroManager>().allowMove = true;
        }


        if (Input.GetKeyDown(keybind.ability1))
        {
            rss.player.GetComponent<HeroManager>().abilityToggle1 = true;
        }

        if (Input.GetKeyDown(keybind.ability2))
        {
            rss.player.GetComponent<HeroManager>().abilityToggle2 = true;
        }

        if (Input.GetKeyDown(keybind.ability3))
        {
            rss.player.GetComponent<HeroManager>().abilityToggle3 = true;
        }

        if (Input.GetKeyDown(keybind.ability4))
        {
            rss.player.GetComponent<HeroManager>().abilityToggle4 = true;
        }

        if (Input.GetKeyDown(keybind.holdPosition))
        {
            rss.player.GetComponent<HeroManager>().allowMove = false;
        }

    }

    void FixedUpdate()
    {
        if (rss.player.GetComponent<HeroManager>().allowMove)
        {
            rss.player.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(destination, rss.player.transform.localPosition, rss.player.transform.localRotation));
            Vector3 newPlayerPos = Vector2.MoveTowards(rss.player.transform.localPosition, destination, rss.player.GetComponent<HeroManager>().moveSpeed * Time.deltaTime);
            newPlayerPos = FreeMatrix.Utility.Convert2D.LocalToPixel(rss.displayCanvas.transform.localScale, newPlayerPos);
            newPlayerPos = FreeMatrix.Utility.Convert2D.PixelToWorld(newPlayerPos);
            rss.player.GetComponent<Rigidbody2D>().position = newPlayerPos;

            if (Vector2.Distance(rss.player.transform.localPosition, destination) < 300 * Time.deltaTime) rss.player.GetComponent<HeroManager>().allowMove = false;
        }
    }
}

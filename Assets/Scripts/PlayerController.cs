using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static Resources _rss;
    private static SceneManager _scene;
    private static Keybind _keybind;

    private Vector3 _onRightClickPosition;
    private Vector3 _destination;

    // Start is called before the first frame update
    void Start()
    {
        _rss = FindObjectOfType<Resources>();
        _scene = FindObjectOfType<SceneManager>();
        _keybind = FindObjectOfType<Keybind>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _onRightClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _destination = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, _onRightClickPosition * 100);


            _rss.player.GetComponent<HeroManager>().allowMove = true;
        }


        if (Input.GetKeyDown(_keybind.ability1))
        {
            _rss.player.GetComponent<HeroManager>().abilityToggle1 = true;
        }

        if (Input.GetKeyDown(_keybind.ability2))
        {
            _rss.player.GetComponent<HeroManager>().abilityToggle2 = true;
        }

        if (Input.GetKeyDown(_keybind.ability3))
        {
            _rss.player.GetComponent<HeroManager>().abilityToggle3 = true;
        }

        if (Input.GetKeyDown(_keybind.ability4))
        {
            _rss.player.GetComponent<HeroManager>().abilityToggle4 = true;
        }

        if (Input.GetKeyDown(_keybind.holdPosition))
        {
            _rss.player.GetComponent<HeroManager>().allowMove = false;
        }

    }

    void FixedUpdate()
    {
        if (_rss.player.GetComponent<HeroManager>().allowMove)
        {
            _rss.player.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(_destination, _rss.player.transform.localPosition, _rss.player.transform.localRotation));
            Vector3 newPlayerPos = Vector2.MoveTowards(_rss.player.transform.localPosition, _destination, (_rss.player.GetComponent<HeroManager>().hero.moveSpeed +
            _rss.player.GetComponent<HeroManager>().hero.bonusMoveSpeed) * Time.deltaTime);
            newPlayerPos = FreeMatrix.Utility.Convert2D.LocalToPixel(_rss.displayCanvas.transform.localScale, newPlayerPos);
            newPlayerPos = FreeMatrix.Utility.Convert2D.PixelToWorld(newPlayerPos);
            _rss.player.GetComponent<Rigidbody2D>().position = newPlayerPos;

            if (Vector2.Distance(_rss.player.transform.localPosition, _destination) < 300 * Time.deltaTime) _rss.player.GetComponent<HeroManager>().allowMove = false;
        }
    }
}

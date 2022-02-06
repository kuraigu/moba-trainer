using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blink", menuName = "Ability/Blink")]
public class Blink : Ability
{
    private static Resources rss;

    public override void Start()
    {
        rss = GameObject.Find("Game Script").GetComponent<Resources>();

        abilityName = "Blink";
        isPassive = false;
        range = 400;

        isActive = true;

        coolDownTime = 30;
        coolDownTimeRun = false;
        coolDownTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, coolDownTime);
    }

    /// <summary>
    /// Activate the ability upon pressing an assigned button set from the HeroManager. Only called once
    /// </summary>
    /// <param name="parent">GameObject reference where the Blink ability is attached to</param>
    public override void Activate()
    {
        if (isActive)
        {
            onMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onMousePosition = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, onMousePosition * 100);
            destination = onMousePosition;

            parent.GetComponent<HeroManager>().allowMove = false;

            parent.transform.localPosition = Vector3.MoveTowards(parent.transform.localPosition, destination, range);

            parent.GetComponent<Rigidbody2D>().position = FreeMatrix.Utility.Convert2D.LocalToWorld(rss.displayCanvas.transform.localScale, parent.transform.localPosition);

            parent.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(onMousePosition, parent.transform.localPosition, parent.transform.localRotation));

            coolDownTimeRun = true;
            isActive = false;
        }
    }

    public override void Update()
    {
        if (coolDownTimeRun)
        {
            if (coolDownTimer.Update())
            {
                isActive = true;
                coolDownTimeRun = false;
            }
        }
    }
}

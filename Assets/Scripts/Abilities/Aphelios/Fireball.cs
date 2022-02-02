using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/Fireball")]
public class Fireball : Ability
{
    private static Resources rss;

    private GameObject projectileReference;
    private List<GameObject> projectile;
    private List<Vector3> oldPositionList;

    private float thrust;

    private Vector2 direction;

    public override void Start()
    {
        rss = GameObject.Find("Game Script").GetComponent<Resources>();
        projectileReference = rss.fireBall;
        range = 500;

        projectile = new List<GameObject>();
        oldPositionList = new List<Vector3>();

        abilityName = "Fireball";
        isPassive = false;

        isActive = true;

        attackSpeed = 1000;

        coolDownTime = 5;
        coolDownTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, coolDownTime);
        coolDownTimeRun = false;

        thrust = FreeMatrix.Utility.Convert.ToLocal(rss.displayCanvas.transform.localScale.x, attackSpeed);
        thrust = FreeMatrix.Utility.Convert.ToWorld(rss.displayCanvas.transform.localScale.x, attackSpeed);

        Debug.Log(thrust);
    }

    public override void Activate()
    {
        if (isActive)
        {
            parent.GetComponent<HeroManager>().allowMove = false;

            onMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onMousePosition = FreeMatrix.Utility.Convert.ToLocal(rss.displayCanvas.transform.localScale, onMousePosition * 100);

            projectile.Add(Instantiate(projectileReference));
            int tempIndex = projectile.Count - 1;

            projectile[tempIndex].transform.SetParent(rss.displayCanvas.transform, false);
            projectile[tempIndex].transform.localPosition = parent.transform.localPosition;

            Vector2 direction = onMousePosition - projectile[tempIndex].transform.localPosition;

            Vector3 angle = FreeMatrix.Utility.Tween.PointTo2D(onMousePosition, projectile[tempIndex].transform.localPosition, projectile[tempIndex].transform.localRotation);

            projectile[tempIndex].transform.localRotation = Quaternion.Euler(angle);
            parent.transform.localRotation = Quaternion.Euler(angle);

            oldPositionList.Add(parent.transform.localPosition);

            isActive = false;
            coolDownTimeRun = true;
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

    public override void FixedUpdate()
    {
        if (projectile.Count > 0)
        {
            for (int i = 0; i < projectile.Count; i++)
            {
                projectile[i].GetComponent<Rigidbody2D>().velocity = projectile[i].transform.right * new Vector2(thrust, thrust);

                if (Vector2.Distance(oldPositionList[i], projectile[i].transform.localPosition) >= FreeMatrix.Utility.Convert.ToLocal(rss.displayCanvas.transform.localScale.x, range))
                {
                    Destroy(projectile[i]);
                    projectile.RemoveAt(i);
                    oldPositionList.RemoveAt(i);
                }
            }
        }
    }
}

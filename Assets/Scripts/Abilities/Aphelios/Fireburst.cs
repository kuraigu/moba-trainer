using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Firebust", menuName = "Ability/Firebust")]
public class Fireburst : Ability
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
        isDestroy = false;

        attackSpeed = 700;

        coolDownTimeRun = false;
        coolDownTime = 5;
        coolDownTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, coolDownTime);

        thrust = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale.x, attackSpeed);
        thrust = FreeMatrix.Utility.Convert2D.LocalToWorld(rss.displayCanvas.transform.localScale.x, attackSpeed);
    }

    public override void Activate()
    {
        if (isActive)
        {
            parent.GetComponent<HeroManager>().allowMove = false;

            Vector2 direction = new Vector3();
            Vector3 angle = new Vector3();

            projectile.Add(Instantiate(projectileReference));
            projectile.Add(Instantiate(projectileReference));
            projectile.Add(Instantiate(projectileReference));

            for (int i = projectile.Count - 3; i < projectile.Count; i++)
            {
                projectile[i].transform.SetParent(rss.displayCanvas.transform, false);
                projectile[i].transform.localPosition = parent.transform.localPosition;

                if (i == projectile.Count - 3)
                {
                    direction = onMousePosition - projectile[i].transform.localPosition;
                    angle = FreeMatrix.Utility.Tween2D.PointTo(onMousePosition, projectile[i].transform.localPosition, projectile[i].transform.localRotation);

                    projectile[i].transform.localRotation = Quaternion.Euler(angle);
                    parent.transform.localRotation = Quaternion.Euler(angle);
                }

                else if (i > projectile.Count - 3 && i % 2 == 0)
                {
                    float angleTemp = angle.z - 15;
                    projectile[i].transform.localRotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angleTemp));
                }

                else if (i > projectile.Count - 3 && i % 2 == 1)
                {
                    float angleTemp = angle.z + 15;
                    projectile[i].transform.localRotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angleTemp));
                }

                if (parent.GetComponent<HeroManager>().isPlayer) projectile[i].tag = "Player Projectile";
                else projectile[i].tag = "Enemy Projectile";
            }

            oldPositionList.Add(parent.transform.localPosition);
            oldPositionList.Add(parent.transform.localPosition);
            oldPositionList.Add(parent.transform.localPosition);

            isActive = false;
            coolDownTimeRun = true;

            Debug.Log(onMousePosition);
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
                if (projectile[i] == null)
                {
                    projectile.RemoveAt(i);
                    oldPositionList.RemoveAt(i);
                }
            }

            for (int i = 0; i < projectile.Count; i++)
            {

                projectile[i].GetComponent<Rigidbody2D>().velocity = projectile[i].transform.right * new Vector2(thrust, thrust);


                if (Vector2.Distance(oldPositionList[i], projectile[i].transform.localPosition) >= FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale.x, range))
                {
                    Destroy(projectile[i]);
                    projectile.RemoveAt(i);
                    oldPositionList.RemoveAt(i);
                }
            }
        }
    }
}

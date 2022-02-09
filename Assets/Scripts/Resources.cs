using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public SceneManager sceneManager;

    public Canvas displayCanvas;

    public GameObject player;

    // Misc
    public GameObject enemiesMisc;
    public GameObject heroesMisc;
    public GameObject projectilesMisc;

    // Projectiles
    public GameObject fireBall;

    // Hud 
    // 1. Ability HUD
    public GameObject abilityHud;
    public GameObject abilityHolder1;
    public GameObject abilityHolder2;
    public GameObject abilityHolder3;
    public GameObject abilityHolder4;
    public GameObject coolDownIndicator1;
    public GameObject coolDownIndicator2;
    public GameObject coolDownIndicator3;
    public GameObject coolDownIndicator4;

    public GameObject scoreGameObject;
    public GameObject scoreText;

    // Cursors
    public GameObject generalCursor;

    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        displayCanvas = GameObject.Find("Display Canvas").GetComponent<Canvas>();

        scoreGameObject = GameObject.Find("Score");
        scoreText = GameObject.Find("ScoreText").transform.Find("Value").gameObject;

        if (sceneManager.currentScene != sceneManager.gameOver)
        {
            player = GameObject.Find("Player");

            fireBall = GameObject.Find("Fireball Projectile");

            abilityHud = GameObject.Find("Ability Container");

            abilityHolder1 = GameObject.Find("Ability Holder 1");
            abilityHolder2 = GameObject.Find("Ability Holder 2");
            abilityHolder3 = GameObject.Find("Ability Holder 3");
            abilityHolder4 = GameObject.Find("Ability Holder 4");

            coolDownIndicator1 = GameObject.Find("Cooldown Indicator 1").gameObject;
            coolDownIndicator2 = GameObject.Find("Cooldown Indicator 2").gameObject;
            coolDownIndicator3 = GameObject.Find("Cooldown Indicator 3").gameObject;
            coolDownIndicator4 = GameObject.Find("Cooldown Indicator 4").gameObject;

            coolDownIndicator1.SetActive(false);
            coolDownIndicator2.SetActive(false);
            coolDownIndicator3.SetActive(false);
            coolDownIndicator4.SetActive(false);

            heroesMisc = GameObject.Find("Heroes");
            heroesMisc.SetActive(false);

            generalCursor = GameObject.Find("General Cursor");
        }
    }
}

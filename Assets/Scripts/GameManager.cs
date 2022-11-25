using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int health = 10;

    public static GameManager gm;

    public GameObject resetCanvas;
    public TextMeshProUGUI healthText;
    void Awake()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();

        if(health <= 0)
        {
            FindObjectOfType<SpawnManager>().enabled = false;
            resetCanvas.SetActive(true);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

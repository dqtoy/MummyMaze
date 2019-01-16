using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerCollision : MonoBehaviour
{
    public playerController controller;


    // Lose animation: The mummy kill the player and a new menu appear: Redo, Save, Try again
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;
        if (tag.Equals("enemy"))
        {
            controller.enabled = false;
            GetComponent<Renderer>().enabled = false;

            PlayerPrefs.SetString("MenuType", "LevelFailMenu");
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
        }
    }
}

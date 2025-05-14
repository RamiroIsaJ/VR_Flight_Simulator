using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Inicial : MonoBehaviour
{
    public void VueloLibre()
    {
        SceneManager.LoadScene(1);
    }

    public void TopGun()
    {
        SceneManager.LoadScene(2);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
   
}


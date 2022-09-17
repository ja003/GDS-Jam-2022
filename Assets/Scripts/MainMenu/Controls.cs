using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] Button btnMenu;

    private void Awake()
    {
        btnMenu.onClick.AddListener(Menu);
    }

    private void Menu()
    {
        SceneManager.LoadScene("S_Menu");
    }
}

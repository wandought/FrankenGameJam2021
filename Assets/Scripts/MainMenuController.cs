using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public GameObject mainMenu;
   public GameObject instructionsPage;

   public void OnPlayButtonPressed()
   {
	  SceneManager.LoadScene(1);
   }

   public void OnInstructionsButtonPressed()
   {
	  mainMenu.SetActive(false);
	  instructionsPage.SetActive(true);
   }

   public void OnBackButtonPressed()
   {
	  mainMenu.SetActive(true);
	  instructionsPage.SetActive(false);
   }

   public void OnExitButtonPressed()
   {
	  Application.Quit();
   }
}

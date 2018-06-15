using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCore : MonoBehaviour
{
  public void StartGame(int sceneIndex)
  {
    CreateGameMaster();
    SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
  }

  private void CreateGameMaster()
  {
    Debug.Log("create master");
  }
}

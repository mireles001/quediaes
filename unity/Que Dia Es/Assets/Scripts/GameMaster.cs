using UnityEngine;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour
{
  protected static GameMaster _instance;
  private SceneLoader _sceneLoader;

  void Awake()
  {
    if (_instance == null)
    {
      DontDestroyOnLoad(gameObject);
      _instance = this;
    }
    else if (_instance != this)
    {
      Destroy(gameObject);
    }

    _sceneLoader = transform.GetComponent<SceneLoader>();
  }

  public void GoToScene(int sceneIndex)
  {
    _sceneLoader.GoToScene(sceneIndex);
  }

  public SceneLoader GetSceneLoader()
  {
    return _sceneLoader;
  }

  public static GameMaster GetInstance()
  {
    if (_instance == null)
      _instance = Helpers.CreateGameMaster();

    return _instance;
  }
}
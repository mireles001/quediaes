using UnityEngine;

public class GameMaster : MonoBehaviour
{
  protected static GameMaster _instance;
  private SceneLoader _sceneLoader;
  private GameObject _player;
  private GameObject _ui;
  private GameProgress _progress;

  void Awake()
  {
    if (_instance == null)
    {
      DontDestroyOnLoad(gameObject);
      _instance = this;
    }
    else if (_instance != this)
      Destroy(gameObject);

    _progress = transform.GetComponent<GameProgress>();
    _sceneLoader = transform.GetComponent<SceneLoader>();
  }

  public void GoToScene(int sceneIndex)
  {
    if (sceneIndex == 13 && _progress._basementPhase)
      sceneIndex = 14;

    _sceneLoader.GoToScene(sceneIndex);
  }

  public void CreatePlayerAndUI(GameObject go, GameObject ui)
  {
    _player = Instantiate(go);
    _player.transform.parent = gameObject.transform;

    _ui = Instantiate(ui);
    _ui.transform.parent = gameObject.transform;

    _player.GetComponent<PlayerCore>().StartUp();
    _player.SetActive(false);
    _ui.SetActive(false);
  }

  public void ActivatePlayerAndUI()
  {
    _player.SetActive(true);
    _ui.SetActive(true);
  }

  public GameObject GetPlayer()
  {
    return _player;
  }

  public GameObject GetUI()
  {
    return _ui;
  }

  public SceneLoader GetSceneLoader()
  {
    return _sceneLoader;
  }

  public static GameMaster GetInstance()
  {
    if (_instance == null)
      _instance = ObjectFactory.CreateGameMaster();

    return _instance;
  }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
  protected static GameMaster _instance;
  private SceneLoader _sceneLoader;
  private int _phase = 0;
  private GameObject _currentInteractive;

  private string _spawnPointName;

  // Items
  private bool _tegami = false;
  private bool _smallKey = false;
  private bool _hammer = false;
  private bool _sword = false;
  private bool _bigKey = false;
  private bool _knob = false;

  private bool _armorEquipped = false;

  private Dictionary<string, bool> _pickedItems = new Dictionary<string, bool>();
  private List<Item> _inventory = new List<Item>();

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

  public int GetPhase()
  {
    return _phase;
  }

  public void SetPhase(int value)
  {
    _phase = value;
  }

  public void SetInteractive(GameObject go)
  {
    if (go != null)
    {
      Debug.Log("Target: " + go.name);
    }
    _currentInteractive = go;
  }

  public GameObject GetInteractive()
  {
    return _currentInteractive;
  }

  public void ReleaseInteractive()
  {
    Debug.Log("Release: " + _currentInteractive.name);
    SetInteractive(null);
  }

  public bool IsItemPicked(string key)
  {
    bool value = false;

    if (_pickedItems.ContainsKey(key))
    {
      value = true;
    }

    return value;
  }

  public void ItemPicked(string key, Item item = null)
  {
    _pickedItems.Add(key, true);

    if (item != null)
    {
      _inventory.Add(item);
    }
    else
    {
      switch (key)
      {
        case "tegami":
          _tegami = true;
          break;
        case "small_key":
          _smallKey = true;
          break;
        case "hammer":
          _hammer = true;
          break;
        case "sword":
          _sword = true;
          break;
        case "big_key":
          _bigKey = true;
          break;
        case "knob":
          _knob = true;
          break;
      }
    }
  }

  public int GetSceneIndex()
  {
    return SceneManager.GetActiveScene().buildIndex;
  }

  public void SetSpawnPoint(string value)
  {
    _spawnPointName = value;
  }

  public string GetSpawnPoint()
  {
    return _spawnPointName;
  }

  public bool HasRequirements(string keyName)
  {
    bool value = true;

    switch (keyName)
    {
      case "sword":
        if (!_hammer)
        {
          value = false;
        }
        break;
      case "big_key":
        if (!_sword)
        {
          value = false;
        }
        break;
      default:
        Debug.Log("no requirements");
        break;
    }

    return value;
  }
}
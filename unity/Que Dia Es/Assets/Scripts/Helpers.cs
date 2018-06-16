using UnityEngine;

public static class Helpers
{
  public static GameMaster CreateGameMaster()
  {
    GameObject master = new GameObject();
    master.AddComponent<SceneLoader>();
    master.name = "GameMaster";
    master.tag = "GameMaster";

    return master.AddComponent<GameMaster>();
  }

  public static GameObject CreateFadeInFadeOut()
  {
    GameObject fadeInFadeOut = new GameObject();
    fadeInFadeOut.AddComponent<FadeInFadeOut>();
    fadeInFadeOut.name = "FadeInFadeOut";

    return fadeInFadeOut;
  }

  public static string GetGameObjectPath(Transform targetTransform)
  {
    string path = targetTransform.name;
    while (targetTransform.parent != null)
    {
      targetTransform = targetTransform.parent;
      path = targetTransform.name + "/" + path;
    }
    return path;
  }
}

// Camspot variables
public enum CameraTypes
{
  Fixed,
  Free,
  Rail
}

public enum LookAtTypes
{
  Nothing,
  Player,
  Pivot
}

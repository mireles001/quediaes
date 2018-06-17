using UnityEngine;

public static class ObjectFactory
{
  public static GameMaster CreateGameMaster()
  {
    GameObject master = new GameObject();
    master.AddComponent<SceneLoader>();
    master.AddComponent<GameProgress>();
    master.name = "GameMaster";

    return master.AddComponent<GameMaster>();
  }

  public static GameObject CreateFadeInFadeOut()
  {
    GameObject fadeInFadeOut = new GameObject();
    fadeInFadeOut.AddComponent<FadeInFadeOut>();
    fadeInFadeOut.name = "FadeInFadeOut";

    return fadeInFadeOut;
  }
}

public static class Helpers
{
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

public static class TextContent
{
  public const string POTION = "Potion";
  public const string POTION_DESCRIPTION = "Al tomarse sube HP. Pero... Ni al caso verdad?";
  public const string ARMOR = "Lade's Armor";
  public const string ARMOR_DESCRIPTION = "Armadura estilera en muy buen estado, finders keepers!";
  public const string TEGAMI = "Carta";
  public const string TEGAMI_DESCRIPTION = "Paty, no hay tiempo que perder! Rapido! Baja al sotano, se va a enfriar la comida!";
  public const string KEY = "Llave Violeta";
  public const string KEY_DESCRIPTION = "Pequeña llave color violeta, que abrira?";
  public const string HAMMER = "Martillo";
  public const string HAMMER_DESCRIPTION = "Tiene un buen peso y agarradera firme. Momento de romper cosas!";
  public const string SWORD = "Espada de la familia";
  public const string SWORD_DESCRIPTION = "Para empezar, ni sabias que tu familia tenia una espada.";
  public const string BIGKEY = "Llavezota";
  public const string BIGKEY_DESCRIPTION = "Una buena llave, abre una buena puerta...";
  public const string KNOB = "Manija";
  public const string KNOB_DESCRIPTION = "Ni que fuera Resident Evil.";
}

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

public enum InteractiveTypes
{
  Read,
  Item,
  Door
}

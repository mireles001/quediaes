using System.Collections.Generic;
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

  // Dialogues
  public static List<string> TEGAMI_TEXT = new List<string> {
    "Una carta! La deslizaron por debajo de la puerta...\nQue simples, me la hubieran dado en la mano y ya.\nVeamos...",
    "'Paty, no hay tiempo que perder! Rapido! Baja al sotano, se va a enfriar la comida!'",
    "Al sotano? Pues bueno, aqui vamos!"
  };
  public static List<string> POTION_TEXT = new List<string> {
    "Una Potion! ... en un juego sin HP. /shrugs"
  };
  public static List<string> ATTIC0_TEXT = new List<string> {
    "Confesiones...",
    "Me cuesta trabajo hablarte de frente cuando estoy contigo.\nCuando te veo la cara no puedo pensar bien, y me da miedo.\nSiempre esta ese temor de que vaya decir una idiotez o algo que arruine el momento."
  };
  public static List<string> ATTIC1_TEXT = new List<string> {
    "Mas confesiones...",
    "Cuando me despido de ti, me odio por no ser normal como con todos...\nNunca he juntado el valor para darte el simple beso en la mejilla de despedida, o el abrazo de saludo.",
    "Es injusto que con los demas no tenga problemas pero contigo me siento abrumado...",
    "\n\nPorque soy asi? Que me pasa?"
  };
  public static List<string> ATTIC2_TEXT = new List<string> {
    "... Finalmente, continuando la nota en papel:",
    "\n\nNota complementaria (P2)",
    "Alguna vez mencionaste que al dibujar intentas llegar a los demas, dejar una marca en ellos - en aquellos. Bueno, yo busco lo mismo... Pero de un tiempo a aca yo lo hago viendo hacia tu dirección.",
    "Lo que pase fuera de mi no esta en mi control y por ninguna razon debo esperar que las cosas giren al ritmo que lo hacen en mi cabeza. Esto es tan solo una ventana de lo que pasa aqui adentro, en mi mundo.",
    "Ha sido una suerte el haberme topado contigo... Empaparme de todo esto, de la inspiracion y admiracion. Por todo esto, gracias!"
  };
  public static List<string> MAIN0_TEXT = new List<string> {
    "\n\nReceta de Crema de Brocoli:",
    "1. 3 tazas de brócoli\n2. 2 cucharadas de mantequilla\n3. 1 cebolla finamente picada\n4. 3 cucharadas de harina",
    "5. 2 tazas de caldo de pollo hecho en casa o de lata\n6. 1 taza de crema7. al gusto de pimienta\n8. al gusto de sal",
    "\n\n...\nMeh, pues si esto ya lo sabia."
  };
  public static List<string> MAIN1_TEXT = new List<string> {
    "Leyendas de los Recuerdos Familiares...",
    "Dicese de un cofre antiguo que otorga el acceso a las profundidades...\nPero, aventureros, han de saber que un cofre antiguo solo responde a reliquias antiguas!",
    "Los recuerdos familiares son la espada que corta el velo de la oscuridad e incertidumbre, arma de doble filo capaz de abrir todo tipo de misterio.",
    "\n\n... Que?"
  };
  public static List<string> TVROOM0_TEXT = new List<string> {
    "A ver si mas al rato veo, por fin, Unbreakable."
  };

  public static List<string> BASEMENT0_TEXT = new List<string> {
    "Hey! Te tardaste un monton! Creo que ya se enfrio todo! Pero no importa...\nFeliz cumple!"
  };
  public static List<string> BASEMENT1_TEXT = new List<string> {
    "Feliz cumpleaños! Patricia Sofia!"
  };
  public static List<string> BASEMENT2_TEXT = new List<string> {
    "Muy bien! El momento esperado... Lista para partir el pastel? (ya todos tenemos hambre!)"
  };
  public static List<string> BASEMENT3_TEXT = new List<string> {
    "Que tengas un lindo dia! -Paty, tendiste tu cama?"
  };
  public static List<string> BASEMENT4_TEXT = new List<string> {
    "Apenitas y alcance a tomar un vuelo de ultima hora.\nHappy Birthday!"
  };
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

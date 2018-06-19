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
  public const string TEGAMI_DESCRIPTION = "Paty, no hay tiempo que perder! Rápido! Baja al sótano, se va a enfriar la comida!";
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

  // TEGAMI
  public static List<string> TEGAMI_TEXT = new List<string> {
    "Una carta! La deslizaron por debajo de la puerta...\nQue simples, me la hubieran dado en la mano y ya.\nVeamos...",
    "'Paty, no hay tiempo que perder! Rapido! Baja al sotano, se va a enfriar la comida!'",
    "Al sotano? Pues bueno, aqui vamos!"
  };

  // POTION
  public static List<string> POTION_TEXT = new List<string> {
    "Una Potion! ... en un juego sin HP. /shrugs"
  };

  // LECTURES
  public static List<string> ATTIC0_TEXT = new List<string> {
    "Confesiones...",
    "Me cuesta trabajo hablarte de frente cuando estoy contigo.\nCuando te veo la cara no puedo pensar bien, y me da miedo.\nSiempre esta ese temor de que vaya decir una idiotez o algo que arruine el momento."
  };
  public static List<string> ATTIC1_TEXT = new List<string> {
    "Más confesiones...",
    "Cuando me despido de ti, me odio por no ser normal como con todos...\nNunca he juntado el valor para darte el simple beso en la mejilla de despedida, o el abrazo de saludo.",
    "Es injusto que con los demás no tenga problemas pero contigo me siento abrumado...",
    "\n\nPorque soy así? Qué me pasa?"
  };
  public static List<string> ATTIC2_TEXT = new List<string> {
    "... Finalmente, continuando la nota en papel:",
    "\n\nNota complementaria (P2)",
    "Alguna vez mencionaste que al dibujar intentas llegar a los demás, dejar una marca en ellos - en aquellos. Bueno, yo busco lo mismo... Pero de un tiempo a acá yo lo hago viendo hacia tu dirección.",
    "Lo que pase fuera de mi no esta en mi control y por ninguna razón debo esperar que las cosas giren al ritmo que lo hacen en mi cabeza. Esto es tan solo una ventana de lo que pasa aquí adentro, en mi mundo.",
    "Ha sido una suerte el haberme topado contigo... Empaparme de todo esto, de la inspiración y admiración. Por todo esto, gracias!"
  };
  public static List<string> MAIN0_TEXT = new List<string> {
    "\n\nReceta de Crema de Brocoli:",
    "1. 3 tazas de brócoli\n2. 2 cucharadas de mantequilla\n3. 1 cebolla finamente picada\n4. 3 cucharadas de harina",
    "5. 2 tazas de caldo de pollo hecho en casa o de lata\n6. 1 taza de crema7. al gusto de pimienta\n8. al gusto de sal",
    "\n...\nMeh, pues si esto ya lo sabia."
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

  // SMALL KEY
  public static List<string> SMALLKEY_TEXT = new List<string> {
    "Mira, un libro con guías e información de Final Fantasy XIV...Seguro Ivonne lo dejó aquí.\n\nEh? Qué es esto?",
    "*GASP* Como separador usó una llave púrpura...\nQue puerta abrirá? (Se molestará si la agarro??)"
  };
  public static List<string> SMALLKEY_OPTION = new List<string> {
    "Tomar la Llave Violeta?"
  };

  // ARMOR
  public static List<string> ARMOR_OPTION = new List<string> {
    "Usar la Llave Violeta en el cerrojo del Closet?\nSerías capaz de invadir el espacio privado de tu hermana? :o"
  };
  public static List<string> ARMOR_TEXT = new List<string> {
    "Mmm! Está cerrado con llave! Por favor, es tan solo un closet!\nNi al caso tanta seguridad. A menos... que.. tenga... LOOT?\nInteresting..."
  };
  public static List<string> GETARMOR_TEXT = new List<string> {
    "Whoaa! Hay una armadura de batalla dentro del closet!\nNice! Mira que esto no es robar, es tan solo pedir prestado... Además que que feo que este la pobre armadura arrumbada. n_n"
  };
  public static List<string> HASARMOR_TEXT = new List<string> {
    "Si, efectivamente es un Closet sin nada interesante...\nJaja, bueno, ya no hay nada interesante (ahora si)."
  };

  // HAMMER
  public static List<string> HAMMER_TEXT = new List<string> {
    "La caja de herramientas de mi papá. Esta hasta el tope de cosas útiles para reparaciones caseras, pero definitivamente, mi herramienta de construcción y destrucción es...",
    "\n\nEl MARTILLO! Matanga!"
  };

  // SWORD
  public static List<string> SWORD_TEXT = new List<string> {
    "Dentro de la vitrina se puede ver la Espada Familiar... Talvez si encuentro algo con que pueda destruir la vitrina (obvio, porque no hay tiempo para abrirla como gente civilizada) pueda tomar la espada.",
    "La pregunta es... Donde podre encontrar una herramienta digna de tal tarea?"
  };
  public static List<string> GETSWORD_TEXT = new List<string> {
    "Ahora si, ya sin la vitrina molestando... Venga para acá! Muajaja!"
  };
  public static List<string> SWORD_OPTION = new List<string> {
    "Quiza con el martillo - si le pego bien duro - podría romper esa vitrina del demonio. Necesito esa espada!\nUsar martillo?"
  };

  // BIGKEY
  public static List<string> BIGKEY_TEXT = new List<string> {
    "Este cofre ha estado aquí desde que  tengo memoria y ahora que lo pienso jamás le he prestado mucha importancia. Debería intentar abrirlo, algo en su interior podría ser de ayuda.",
    "Interesante, la cerradora tiene la forma de un rombo... Como la hoja de un cuchillo o espada...\nQuien habrá diseñado este cofre tan raro?"
  };
  public static List<string> GETBIGKEY_TEXT = new List<string> {
    "Por fin! Los gloriosos interiores del cofre misterioso de la familia! Dinero, riquezas, fama, poder... VENGAN A MI!",
    "\n\n...",
    "Solo una llave? Bueno, pero es de buen peso y tamaño... Es la llave maestra de la casa.\n*Sigh* Bueno, creo que con esto mínimo podre abrir el sótano."
  };
  public static List<string> BIGKEY_OPTION = new List<string> {
    "La cerradura del cofre tiene la forma del filo de la espada. Quiza si incerto la Espada Familiar bote la cerradura. Lo intento?"
  };

  // BASEMENT DOOR
  public static List<string> BASEMENTDOOR_TEXT = new List<string> {
    "Sip, esta definitivamente es la puerta que lleva al sótano.\nTiene un cerrojo grandote... De seguro se podrá abrir con una llave de buen tamaño. La pregunta es...",
    "\n\nDónde podré encontrar dicha llave?"
  };
  public static List<string> UNLOCKEDBASEMENTDOOR_TEXT = new List<string> {
    "Por fin! Ahora s-- Qué es esto? Se cayo la manija de la puerta... De haber sabido que estaba a nadita de caerse mejor le pego un patadon y me ahorro tanta vuelta!",
    "Aun asi, creo que guardaré la manija... Uno nunca sabe. De todas formas esta puerta ya se quedara abierta para siempre :p"
  };
  public static List<string> BASEMENTDOOR_OPTION = new List<string> {
    "Lista para usar la Llavezota? Los misterios del sótano estan pronto a ser revelados!\nUsar Llavezota?"
  };

  // BASEMENT
  public static List<string> BASEMENT_TEXT = new List<string> {
    "Uy, que oscurisimo esta aquí! Si me sigo moviendo me voy a venir pegando...\nMas vale que prenda la luz."
  };
  public static List<string> BASEMENT_OPTION = new List<string> {
    "Encender lámpara?"
  };

  // LADDER
  public static List<string> LADDER_TEXT = new List<string> {
    "Ah! Que cosa más escandaloza!\nDefinitivamente ha de estar todo sucio allá arriba... Se ve que nadie ha subido en mucho tiempo al ático.\nO si?"
  };
  public static List<string> LADDER_OPTION = new List<string> {
    "La cuerdita para bajar las escaleras que dan al ático. Que onda, la jalo? (Tengo añales sin subir alla... seguro esta todo sucio)"
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

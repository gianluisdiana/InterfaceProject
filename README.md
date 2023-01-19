# Proyecto final _Interfaces Inteligentes_.
- Gian Luis Bolivar Diana. _gian.diana.28@ull.edu.es_
- Marta Molina Fernández. _alu0101603360@ull.edu.es_
- Jorge Quintana García. _alu0101123547@ull.edu.es_

## Cuestiones importantes para el uso.
La aplicación funciona con un mando (conectado al dispositivo via Bluetooth) de forma que para interaccionar con los objetos pulsar el botón correspondiente.

Actualmente tenemos implementado 3 comandos:
- `GRAB`: Nos permite agarrar algunos objetos (se iluminan los disponibles).

- `DROP`: Si tenemos algún objeto, lo suelta al piso.

- `THROW`: Lanza el objeto en posesión en la dirección que apuntamos.

## Hitos de programación logrados relacionándolos con los contenidos que se han impartido.
Hemos utilizado una gran variedad de scripts en la realización de este proyecto. Empezando por los contenidos más básicos movemos objetos de la escena, tanto con el uso de fuerza (cuando los objetos tienen `Rigidbody`), como haciendo 'mini tele-transportes', modificando sus coordenadas directas; modificamos de forma dinámica el material de objetos (ilustrando cuales objetos se pueden agarrar). Destruimos objetos cuando están en zonas indeseables. De los tópicos más complicados de entender (no tanto por su complejidad, sino por su abstracción), los 'notifiers' y 'subscribers' fueron implementados reiteradamente según se vió oportuno.

Fuera de los contenidos de la asignatura, nos parece relevante destacar el uso de funcionalidades ofrecidas por _Unity_, tales como `SceneManagement`, `Tooltip`, `SerializeField`, llamadas a co-rutinas para ejecutar varias funciones a la vez.

## Aspectos que destacarías en la aplicación. Especificar si se han incluido sensores de los que se han trabajado en interfaces multi-modales.

En un inicio, teníamos planeado utilizar un reconocedor de voz, de forma que no hubiera hecho falta el mando para interactuar con la aplicación, de hecho, teníamos implementado los códigos necesarios y funcionales para windows pero nos encontramos con la dificultad de que la librería implementada en _Unity_ era solo para aplicaciones de pc (y no para Android o iOS), por lo que tuvimos que buscar alternativas pero no llegamos a ninguna que pudiéramos utilizar.

Aparte de lo mencionado, no se incorporó ninguna interfaz multi-modal.

### Niveles implementados:
1. Sencillo, con la canasta y el cubo en el piso (estáticos) y enseña a como usar la aplicación.
2. La canasta sigue siendo estática pero ahora el cubo está en una plataforma que se eleva y baja.
3. Tanto la canasta como el cubo son estáticos. Canasta considerablemente elevada.
4. La canasta se mueve de lado a lado, de forma solo es visible cuando esta a la derecha, la plataforma del cub es estática.
5. Canasta y cubo estáticos. Hay una pared (entre el usuario y la canasta) que se puede demoler con unas esferas dispuestas al costado del usuario.

## Gif animado de ejecución.

### Gif de cada nivel
<details>
  <summary> Nivel <c>1</c> </summary>

  ![Level1](./img/level_1.png)
</details>
<details>
  <summary> Nivel <c>2</c> </summary>

  ![Level2](./img/level_2.png)
</details>
<details>
  <summary> Nivel <c>3</c> </summary>

  ![Level3](./img/level_3.png)
</details>
<details>
  <summary> Nivel <c>4</c> </summary>

  ![Level4](./img/level_4.png)
</details>
<details>
  <summary> Nivel <c>5</c> </summary>

  ![Level4](./img/level_5.png)
</details>


### Ayuda mostrada en el primer nivel.

![HelpDisplayed](./img/help_displayed.png)

### Seguimiento de farola a canasta

![LightFollow](./img/light_follow.png)

### Transición entre niveles

![LevelTransactions](./img/level_transactions)

## Acta de los acuerdos del grupo respecto al trabajo en equipo: reparto de tareas, tareas desarrolladas individualmente, tareas desarrolladas en grupo, etc.

- Codificación de scripts:
  - `BasketMove`: Marta y Gian
  - `Box_Vertical`: Marta y Gian
  - `DisplayHelp`: Gian
  - `GameManager`: Jorge
  - `Interactions`: Gian
  - `LightFollowBasket`: Jorge
  - `PickUpScript`: Jorge y Gian
  - `Platform_Move`: Marta
  - `ReSpawnBox`: Gian y Jorge
  - `SelectionManager`: Jorge
  - `WinDetection`: Jorge

- Desarrollo de escenas: Grupal (Marta y Jorge en general)
- Búsqueda de ideas y assets: Grupal
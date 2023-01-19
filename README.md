# Proyecto final _Interfaces Inteligentes_.
- Gian Luis Bolivar Diana. _gian.diana.28@ull.edu.es_
- Marta Molina Fernández. _alu0101603360@ull.edu.es_
- Jorge Quintana García. _alu0101123547@ull.edu.es_

## Cuestiones importantes para el uso.
La aplicación funciona con un mando (conectado al dispositivo via Bluetooth) de forma que para interaccionar con los objetos debemos pulsar el botón correspondiente.

Actualmente tenemos implementado 3 comandos:
- `GRAB`: Nos permite agarrar algunos objetos (se iluminan los disponibles).

- `DROP`: Si tenemos algún objeto, lo suelta al piso.

- `THROW`: Lanza el objeto en posesión en la dirección que apuntamos.

## Hitos de programación logrados relacionándolos con los contenidos que se han impartido.
Hemos utilizado una gran variedad de scripts en la realización de este proyecto. Empezando por los contenidos más básicos movemos objetos de la escena, tanto con el uso de fuerza (cuando los objetos tienen `Rigidbody`), como haciendo 'mini tele-transportes', modificando sus coordenadas directas; modificamos de forma dinámica el material de objetos (ilustrando cuales objetos se pueden agarrar). Destruimos objetos cuando están en zonas indeseables. De los tópicos más complicados de entender (no tanto por su complejidad, sino por su abstracción), los 'notifiers' y 'subscribers' fueron implementados reiteradamente según se vió oportuno. Además se profundiza en la comprensión de jerarquía de escenas con el uso de funciones como 'SetParent'.

Fuera de los contenidos de la asignatura, nos parece relevante destacar el uso de funcionalidades ofrecidas por _Unity_, tales como `SceneManagement`, `Tooltip`, `SerializeField`, `RequireComponent` y llamadas a co-rutinas para ejecutar varias funciones a la vez.

## Aspectos que destacarías en la aplicación. Especificar si se han incluido sensores de los que se han trabajado en interfaces multi-modales.

En un inicio, teníamos planeado utilizar un reconocedor de voz, de forma que no hubiera hecho falta el mando para interactuar con la aplicación, de hecho, teníamos implementado los códigos necesarios y funcionales para windows pero nos encontramos con la dificultad de que la librería implementada en _Unity_ era solo para aplicaciones de pc (y no para Android o iOS), por lo que tuvimos que buscar alternativas pero no llegamos a ninguna que pudiéramos utilizar.

Aparte de lo mencionado, no se incorporó ninguna interfaz multi-modal.

### Niveles implementados:
1. Sencillo, con la canasta y el cubo en el piso (estáticos) y enseña a como usar la aplicación.
2. La canasta sigue siendo estática pero ahora el cubo está en una plataforma que se eleva y baja.
3. Tanto la canasta como el cubo son estáticos. Canasta considerablemente elevada.
4. La canasta se mueve de lado a lado, de forma solo es visible cuando esta a la derecha, la plataforma del cub es estática.
5. Canasta y cubo estáticos. Hay una pared (entre el usuario y la canasta) que se puede demoler con unas esferas dispuestas al costado del usuario.
6. La canasta es estática y está escondida de forma que no se pueda lanzar el cubo directamente a ella. El cubo comienza estático pero encontramos a la izquierda del jugador una plataforma móvil que llevará al cubo a la canasta. En el camino encontraremos una pared derribable.

## Gif animado de ejecución.

### Gif de cada nivel
<details>
  <summary> Nivel <c>1</c> </summary>
  Puesto que es el primer nivel, también es el más básico de todos. El objetivo del mismo es que el usuario se familiarice con los objetos de la escena y como puede interactuar con ellos.

  ![Level1](https://user-images.githubusercontent.com/72491269/213371736-b8b675e2-fc1b-48f6-bf7f-7e9e9d30c3e5.gif)
</details>
<details>
  <summary> Nivel <c>2</c> </summary>
  El siguiente nivel tiene un poco más de dificultad, puesto que la caja se encuentra en una plataforma movible que se eleva y se baja. Se busca incrementar en pequeñas medidas cada nivel, y la primera aproximación es esta.

  ![Level2](https://user-images.githubusercontent.com/72491269/213372761-d449d0dd-892d-45bc-9b61-4241f33c5681.gif)
</details>
<details>
  <summary> Nivel <c>3</c> </summary>
  Este en particular es para que el usuario practique su lanzamiento, puesto que la plataforma donde se sitúa la canasta esta en un piso mas elevado que el usuario.

  ![Level3](https://user-images.githubusercontent.com/72491269/213371827-d9be25e5-7c43-4b5d-83e2-67563944ddf4.gif)
</details>
<details>
  <summary> Nivel <c>4</c> </summary>
  En este nivel jugamos con la posición de la canasta, de manera que en ciertos puntos es visible y en otros no. En todo momento el usuario tiene la capacidad de encestar la caja, pero es mucho mas fácil cuando se tiene visión de la canasta

  ![Level4](https://user-images.githubusercontent.com/72491269/213371859-5a0caf4d-506e-4642-95f0-ab1759c9f899.gif)
</details>
<details>
  <summary> Nivel <c>5</c> </summary>
  Aquí introducimos una mecánica nueva: la pared endeble. Se nos dispone de proyectiles (aparte de la caja en si) para lanzar a esta pared demoliéndola gradualmente, y asi tener visión de la canasta.
  
  Observamos la forma de demoler la pared:
  
  ![Level5_1](https://user-images.githubusercontent.com/72491269/213373333-49d7897c-de18-4ae9-b993-bbf8a2fe581b.gif)
  
  Y completamos el nivel:

  ![Level5_2](https://user-images.githubusercontent.com/72491269/213371933-ca540125-557c-44a1-b128-0e007523aa23.gif)
</details>
<details>
  <summary> Nivel <c>6</c> </summary>
  En el último nivel tenemos una plataforma que al sostener la caja (soltar la caja encima) comienza su recorrido.
  
  ![Level6_1](https://user-images.githubusercontent.com/72491269/213371984-74bc82d6-7913-4955-8961-83f2079ac34b.gif)
  
  En este camino encontramos una pared derribable con las esferas dispuestas para ello. También se puede demoler por la repetida colisión de la plataforma móvil.
  
  ![Level6_2](https://user-images.githubusercontent.com/72491269/213372792-824566d1-64f4-429c-a2e3-bda85d476f93.gif)
  
  Para completar el nivel debemos hacer que la plataforma termine su recorrido sin ser obstaculizada.

  ![Level6_3](https://user-images.githubusercontent.com/72491269/213372050-52a782ce-5a86-4008-8077-68b67c7af22d.gif)
</details>


### Ayuda mostrada en el primer nivel.

![Captura1](https://user-images.githubusercontent.com/72491269/213374251-f9993727-14c5-4fa7-88e7-54e9530aec26.PNG)

### Seguimiento de farola a canasta

![Level4_Luz](https://user-images.githubusercontent.com/72491269/213374353-39d15f01-0973-4af8-bb06-f9a4b0c9f8b7.gif)

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

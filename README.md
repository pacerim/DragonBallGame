# 🎮 Dragon Ball Fighting Game
### Guion y desarrollo por **Pablo Díaz de Cerio**

---

## 📜 Introducción

Este proyecto es un **videojuego de peleas entre dos jugadores** en el mismo ordenador, desarrollado con el motor de videojuegos **Godot**.

Godot es un entorno de **código abierto**, ideal para juegos en 2D (perfecto para este proyecto), aunque aún tiene menor contenido comunitario en comparación con Unity.  
Además, permite trabajar con dos lenguajes de programación:
- **GDScript**: lenguaje propio de Godot, muy optimizado para este entorno.
- **C#**: el lenguaje que elegí para este proyecto, ya que es ampliamente utilizado en la industria del software y lo dominamos en la carrera.  
  Sin embargo, la documentación de C# en Godot es limitada y a veces confusa, por lo que fue necesario buscar ejemplos externos.

Después de esta introducción, paso a explicar algunos principios básicos del código que implementé.

---

## 🛠️ Explicación del Código

Godot estructura el desarrollo de juegos mediante **nodos**, que se organizan en una jerarquía visual en el panel izquierdo del editor.  
Cada nodo tiene sus propios atributos, funciones y servicios.

- Ejemplos de nodos:
  - `CharacterBody2D`: cuerpos de personajes en 2D.
  - `CollisionShape2D`: forma física para detectar colisiones.

En mi proyecto:
- La **escena principal** es el mapa donde se libra la pelea.
- Los **personajes** son nodos `CharacterBody2D`, que tienen como hijos nodos `CollisionShape2D` para gestionar las colisiones.
- Los **scripts en C#** agregan funcionalidad a los nodos, permitiendo controlar eventos y comportamientos.

### 🎨 Animaciones

Godot **no facilita** la importación automática de animaciones como Unity.  
Para animar a un personaje caminando, por ejemplo, tuve que:
- Buscar imágenes de cada frame.
- Recortar cada frame manualmente usando **GIMP**.
- Crear animaciones ajustadas a 5 FPS.

💡 **Consejo:** Utiliza **spritesheets** (plantillas de frames) para importar personajes más fácilmente.  
Aquí un ejemplo de spritesheet usado para el personaje de Goku:

*(Aquí podrías insertar una imagen del spritesheet si quieres)*

---

## 📥 Guía de Uso

### 🔽 Descarga
- He incluido el **código fuente** para su descarga y estudio.
- También encontrarás la **aplicación de escritorio para Windows**, compuesta por:
  - Una carpeta comprimida `data/` con los archivos necesarios.
  - Un archivo `.exe` para ejecutar el juego.  
**Importante:** ambos deben estar en la misma carpeta para que funcione correctamente.

### 🛠️ Edición
Para editar el juego:
- Instala **Godot** (recomiendo **Godot Portable** por su facilidad de instalación y traslado entre PCs).
- Importa el proyecto desde el menú de inicio de Godot, usando el archivo ZIP proporcionado que contiene todo el código.

---

## 🎮 Controles de los Personajes

**Goku**
- Movimiento:  
  `W` (arriba) | `A` (izquierda) | `D` (derecha)
- Ataques:  
  `E` (puño) | `F` (patada) | `Barra espaciadora` (ataque especial)

**Vegeta**
- Movimiento:  
  `Flecha arriba` (arriba) | `Flecha izquierda` (izquierda) | `Flecha derecha` (derecha)
- Ataques:  
  `1` (puño) | `2` (patada) | `3` (ataque especial) (usando el teclado numérico)

---

## Vídeo resumen del videojuego

![Video resumen del videojuego - Dragon Ball Game](/DragonBallGame-Video.gif)

---

# ¡Espero que disfrutes explorando y jugando a este proyecto de **Dragon Ball Fighting Game**! 🔥🕹️



# Гайд по Unity Spline Package

## 1. Создание Сплайна

### Способ 1: Через меню
1. В сцене: `GameObject → Spline → Draw Splines Tool`
2. Кликайте левой кнопкой мыши в Scene view, чтобы добавить точки (knots)
3. Нажмите `Enter` или `Escape` для завершения рисования

### Способ 2: Через GameObject
1. `GameObject → Spline → Spline` - создает пустой GameObject со Spline Container
2. В инспекторе можно добавлять точки вручную через `Spline Container` компонент

### Способ 3: Добавить к существующему объекту
1. Выберите GameObject
2. `Add Component → Spline Container`
3. В инспекторе нажмите `+` чтобы добавить точки

## 2. Редактирование Сплайна

### Инструменты
- **Element Mode**: Выбор и перемещение отдельных точек (knots)
- **Tangent Mode**: Редактирование кривых между точками
- **Draw Tool**: Рисование новых точек

### Горячие клавиши
- `Ctrl + Click` - добавить точку на сплайне
- `Delete` - удалить выбранную точку
- `Alt + Drag` - редактировать касательные (tangents)

### Настройки в Inspector
- **Closed**: Замкнуть сплайн (последняя точка соединится с первой)
- **Knot Type**: Linear, Auto Smooth, Bezier, Catmull-Rom

## 3. Компоненты для работы со Сплайнами

### SplineContainer
Хранит данные сплайна. Может содержать несколько сплайнов.

### SplineAnimate
Автоматическое движение объекта по сплайну:
- **Duration**: Время прохождения всего пути
- **Easing Mode**: Тип интерполяции (Linear, EaseIn, EaseOut и т.д.)
- **Loop Mode**: Once, Loop, PingPong
- **Object Up/Forward Axis**: Настройка ориентации объекта

### SplineInstantiate
Размещение объектов вдоль сплайна (например, забор, дорога)

## 4. Программное управление сплайном

### Получение позиции на сплайне
```csharp
using UnityEngine.Splines;

SplineContainer splineContainer = GetComponent<SplineContainer>();
Spline spline = splineContainer.Spline;

// t от 0 до 1 (0 = начало, 1 = конец)
float t = 0.5f;
Vector3 position = spline.EvaluatePosition(t);
Vector3 tangent = spline.EvaluateTangent(t);
Vector3 up = spline.EvaluateUpVector(t);
```

### Получение длины сплайна
```csharp
float length = spline.GetLength();
```

### Конвертация расстояния в t
```csharp
float distance = 10f;
float t = spline.GetNormalizedInterpolation(distance);
```

## 5. Практические примеры использования

### Камера следует по пути
- Добавить SplineAnimate на камеру
- Настроить Duration и Easing
- Можно триггерить через код: `splineAnimate.Play()`

### NPC патрулирование
- Создать сплайн-путь
- Использовать скрипт с GetComponent<SplineContainer>()
- Двигать персонажа по точкам сплайна

### Железная дорога / Дорога
- SplineInstantiate для размещения рельсов/секций дороги
- SplineExtrude для создания геометрии дороги

## 6. Полезные советы

- **Visualize**: В SplineContainer включите "Always Draw Spline" для постоянной видимости
- **Performance**: Используйте меньше точек с Auto Smooth вместо множества Linear точек
- **Rotation**: Используйте `EvaluateTangent()` для направления движения объекта
- **Multiple Splines**: SplineContainer может содержать массив сплайнов для сложных путей


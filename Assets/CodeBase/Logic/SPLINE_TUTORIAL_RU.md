# Пошаговое руководство: Как работать со Spline в Unity

## Часть 1: Создание Сплайна

### Метод 1: Быстрое создание через Draw Tool
1. В Unity Editor откройте вашу сцену
2. В меню выберите: **GameObject → Spline → Draw Splines Tool**
3. В Scene View:
   - **Левый клик** - добавить точку (knot)
   - Продолжайте кликать, чтобы создать путь
   - **Enter** или **Escape** - завершить рисование
4. Готово! У вас создан GameObject с компонентом SplineContainer

### Метод 2: Создание пустого сплайна
1. **GameObject → Spline → Spline**
2. Выберите созданный объект
3. В Inspector найдите **Spline Container**
4. Нажмите на сплайн в списке
5. Используйте Scene View инструменты для добавления точек

### Метод 3: Добавление к существующему объекту
1. Выберите GameObject (например, пустой объект для пути камеры)
2. **Add Component → Spline Container**
3. Редактируйте сплайн в Scene View

---

## Часть 2: Редактирование Сплайна

### Режимы редактирования (вверху Scene View)
- **Element Mode** - выбор и перемещение точек
- **Tangent Mode** - настройка кривизны между точками

### Как добавить точку
- **Метод 1**: `Ctrl + Left Click` на сплайне
- **Метод 2**: В Inspector → Spline → кнопка "+" в списке knots

### Как удалить точку
- Выберите точку → нажмите **Delete**
- Или в Inspector удалите из списка knots

### Настройка кривых
1. Выберите точку (knot)
2. В Inspector найдите **Knot Type**:
   - **Linear** - прямая линия к следующей точке
   - **Auto Smooth** - автоматическая плавная кривая (рекомендуется)
   - **Bezier** - ручная настройка кривой через касательные
   - **Catmull-Rom** - плавная кривая через все точки

### Замкнуть сплайн (Loop)
- В Inspector → SplineContainer → поставьте галочку **Closed**

---

## Часть 3: Движение камеры по сплайну

### Вариант A: Использование готового компонента SplineAnimate

1. **Создайте сплайн** (см. Часть 1)
2. **Выберите вашу камеру**
3. **Add Component → Spline Animate**
4. **Настройте параметры**:
   - **Container**: перетащите ваш SplineContainer
   - **Duration**: время прохождения пути (например, 10 секунд)
   - **Loop Mode**: 
     - **Once** - один раз
     - **Loop** - бесконечный цикл
     - **PingPong** - туда-обратно
   - **Easing Mode**: Linear, EaseIn, EaseOut и т.д.
   - **Auto Play**: включить для автоматического старта

5. **Настройте ориентацию**:
   - **Object Up Axis**: ось "вверх" объекта (обычно Y)
   - **Object Forward Axis**: ось "вперед" объекта (обычно Z)

### Вариант B: Использование CameraSplineMover (кастомный скрипт)

1. **Создайте сплайн**
2. **Выберите камеру**
3. **Add Component → CameraSplineMover**
4. **Настройте в Inspector**:

#### Spline Settings:
   - **Spline Container**: перетащите GameObject со сплайном
   - **Spline Index**: индекс сплайна (обычно 0)

#### Movement Settings:
   - **Auto Move**: автоматическое движение при старте
   - **Speed**: скорость движения (юнитов в секунду)
   - **Loop**: повторять движение заново
   - **Ping Pong**: двигаться туда-обратно

#### Rotation Settings:
   - **Rotate With Spline**: камера поворачивается по направлению сплайна
   - **Rotation Speed**: скорость поворота (5 = плавно)
   - **Rotation Offset**: дополнительный поворот (X, Y, Z в градусах)

#### Look At Settings:
   - **Use Look At**: камера смотрит на цель (отключает Rotate With Spline)
   - **Look At Target**: объект, на который смотрит камера

---

## Часть 4: Управление через код

### Запуск/остановка движения
```csharp
CameraSplineMover mover = GetComponent<CameraSplineMover>();

// Запустить
mover.Play();

// Пауза
mover.Pause();

// Остановить и вернуть в начало
mover.Stop();

// Изменить направление
mover.Reverse();
```

### Изменение параметров
```csharp
// Установить скорость
mover.SetSpeed(2.5f);

// Перейти к определенной точке (0.0 - 1.0)
mover.SetProgress(0.5f); // середина пути

// Узнать текущий прогресс
float progress = mover.GetProgress();
```

### Пример: Триггер для запуска камеры
```csharp
using UnityEngine;
using CodeBase.Logic;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CameraSplineMover _cameraMover;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraMover.Play();
        }
    }
}
```

---

## Часть 5: Практические примеры

### Пример 1: Кат-сцена на рельсах
1. Создайте сплайн вдоль нужного пути
2. Добавьте CameraSplineMover на камеру
3. Настройте:
   - Auto Move: ✓
   - Speed: 3
   - Rotate With Spline: ✓
   - Look At Target: можно указать персонажа

### Пример 2: Обзорная камера (Loop)
1. Создайте замкнутый сплайн (Closed: ✓)
2. CameraSplineMover:
   - Auto Move: ✓
   - Speed: 2
   - Loop: ✓
   - Look At Target: центр сцены

### Пример 3: Патруль врага
1. Создайте путь патрулирования
2. На враге используйте CameraSplineMover или свой скрипт
3. Настройте:
   - Ping Pong: ✓ (туда-обратно)
   - Speed: зависит от скорости врага

---

## Часть 6: Полезные советы

### Визуализация
- **Always Draw Spline**: SplineContainer → Always Draw Gizmo
- Цвет сплайна можно изменить в настройках Gizmos

### Оптимизация
- Меньше точек с Auto Smooth = лучше производительность
- Избегайте слишком частых точек

### Отладка
- CameraSplineMover показывает зеленую сферу на текущей позиции (в режиме Gizmos)
- Используйте Debug.Log(mover.GetProgress()) для проверки позиции

### Сложные пути
- Один SplineContainer может содержать несколько сплайнов
- Используйте Spline Index для выбора нужного пути

---

## Частые проблемы и решения

**Проблема**: Объект не двигается
- ✓ Проверьте, что SplineContainer назначен
- ✓ Auto Move включен
- ✓ Speed больше 0

**Проблема**: Объект вращается странно
- → Попробуйте Rotation Offset
- → Проверьте оси Forward/Up
- → Используйте Look At вместо Rotate With Spline

**Проблема**: Движение слишком быстрое/медленное
- → Настройте Speed
- → Для SplineAnimate используйте Duration вместо Speed

**Проблема**: Сплайн не виден в сцене
- → GameObject → Always Show Gizmo
- → Или включите Gizmos в Scene View

---

## Дополнительные возможности

### SplineExtrude
Создает меш вдоль сплайна (дороги, трубы)

### SplineInstantiate
Размещает префабы вдоль сплайна (фонари, деревья)

### Curve
Программное изменение формы сплайна в runtime

---

Удачи в работе со сплайнами! 🎮


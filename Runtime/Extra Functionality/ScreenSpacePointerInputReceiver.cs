#if UNITY_UI_PRESENT && IMGUI_MODULE_PRESENT
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Receives pointer input events anywhere on screen. Anyone can subscribe to these events.
    /// </summary>
    // We use a canvas in screen space and an empty Image to get those events.
    [RequireComponent(typeof(GraphicRaycaster), typeof(Canvas)), RequireComponent(typeof(StandaloneInputModule), typeof(EventSystem))]
    [AddComponentMenu("Event/Screen Space Pointer Input Receiver")]
    public class ScreenSpacePointerInputReceiver : EventTrigger
    {
        /// <summary>
        /// Add an event listener.
        /// WARNING: There is currently no way to remove a listener.
        /// </summary>
        /// <param name="type">The type of event to listen to.</param>
        /// <param name="callback">The desired callback.</param>
        public void AddListener(EventTriggerType type, UnityAction<BaseEventData> callback)
        {
            Entry entry = new Entry();
            entry.eventID = type;
            entry.callback.AddListener((data) => { callback?.Invoke(data); });
            triggers.Add(entry);
        }

        private void Awake()
        {
            //Set everything up
            EventSystem eventSystem = gameObject.GetComponent<EventSystem>();
            eventSystem.firstSelectedGameObject = null;
            eventSystem.sendNavigationEvents = false;

            GraphicRaycaster raycaster = gameObject.GetComponent<GraphicRaycaster>();
            raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.TwoD;

            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            if (!canvas.worldCamera) Debug.LogWarning("ScreenSpacePointerInputReceiver needs a camera on its canvas to work.", this);

            Image image = gameObject.AddComponent<Image>();
            image.sprite = null;
            image.color = new Color(0, 0, 0, 0);
        }
    }
}
#endif
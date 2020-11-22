# if INPUT_SYSTEM_PRESENT

using UnityEngine.InputSystem;

namespace Hairibar.EngineExtensions
{
    public static class InputActionExtensions
    {
        public static bool IsPressed(this InputAction action, float threshold = 0.5f)
        {
            return action.ReadValue<float>() > threshold;
        }
    }
}

#endif
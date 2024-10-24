using UnityEngine;

namespace Client.Helpers
{
    public static class FireInput
    {
        public static bool IsFirePressDown()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}

namespace SampleGame
{
    public sealed class ApplicationExiter
    {
        public void ExitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit(0);
#endif
        }
    }
}
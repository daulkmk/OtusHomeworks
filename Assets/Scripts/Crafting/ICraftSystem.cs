namespace Crafting
{
    public interface ICraftSystem
    {
        bool CanCraft(Recipe recipe);
        void Craft(Recipe recipe);
    }
}
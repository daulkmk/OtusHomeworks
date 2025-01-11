using System;

namespace Crafting
{
    public class CannotCraftException : Exception
    {
        public CannotCraftException() : base("Cannot craft recipe.") { }
    }
}
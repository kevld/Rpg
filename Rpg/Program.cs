namespace Rpg;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new Rpg.Game1();
        game.Run();
    }
}
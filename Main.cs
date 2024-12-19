//Here I implemented a romanian game which is called "Cruce" (which means "Cross")

namespace Nicolae_Balatici_Cross
{
    class MainClass
    {
        static void Main()
        {
            List<String> colours = Card.AddColour();
            List<int> numbers = Card.AddNumber();
            
            Bunch bunch = new Bunch(colours.Count, numbers.Count);
            bunch.AddCards(numbers, colours);
            
            List<Player> players = new List<Player>(4);
            for (int i = 0; i < 4; i++) { players.Add(new Player()); }
            for (int i = 0; i < 4; i++) { players[i].AddDeck(bunch, players.Count, colours.Count, numbers.Count); }
            
            Game game = new Game(players);
            game.StartGame();
        }
    }
}
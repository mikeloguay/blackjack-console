using Blackjack.Entities;
using Blackjack.Services;
using Blackjack.Services.Interface;
using Blackjack.UI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the service and controller
            IDeckService deckService = new DeckService();
            IGameService gameService = new GameService(deckService);
            IPlayerModel playerModel = new PlayerModel();
            IPlayerView playerView = new PlayerView();
            IPlayerController playerController = new PlayerController(playerModel, playerView, gameService);
            
            string option;
            playerView.GreetUser();
            do
            {
                playerView.ShowMenu();
                option = Console.ReadLine();
                playerController.ManageUserOption(option);
            } while (option != "9");
        }
    }
}

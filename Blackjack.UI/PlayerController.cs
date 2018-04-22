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
    public class PlayerController : IPlayerController
    {
        private IPlayerModel _playerModel;
        private IPlayerView _playerView;
        private IGameService _gameService;
        private Player _player;

        public PlayerController(IPlayerModel playerModel, IPlayerView playerView, IGameService gameService)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _gameService = gameService;
        }

        public void ManageUserOption(string option)
        {
            if (option == "1")
            {
                PlayRound();
            }
        }

        public void PlayRound()
        {
            if (_player == null)
            {
                _player = InitializePlayer();
            }

            // Bet
            int bet = _playerView.AskForBet(_player);
            _player.Bet = bet;

            // Initialize round
            _gameService.TableUpdated += HandleTableUpdated;
            _gameService.JoinGame(_player);

            // The game could finish on the first round if the player has blackjack
            if (!_playerModel.Table.IsGameFinished())
            {
                // Start round
                PlayerAction playerAction;
                do
                {
                    playerAction = _playerView.AskForAction();

                    switch (playerAction)
                    {
                        case PlayerAction.Hit:
                            _gameService.Hit(_player.Name, _playerModel.Table);
                            break;
                        case PlayerAction.Stand:
                            _gameService.Stand(_player.Name, _playerModel.Table);
                            break;
                        default:
                            break;
                    }

                } while (playerAction != PlayerAction.Stand && !_playerModel.Table.IsGameFinished());
            }

            // Round ended
            _gameService.TableUpdated -= HandleTableUpdated;
        }

        private Player InitializePlayer()
        {
            Player player = new Player() { Credit = 1000 };

            // Name
            string name = _playerView.AskForName();
            player.Name = name;

            return player;
        }

        private void HandleTableUpdated(object sender, TableEventArgs e)
        {
            _playerModel.Table = e.Table;
            _playerView.ShowTableStatus(_playerModel);
        }
    }
}

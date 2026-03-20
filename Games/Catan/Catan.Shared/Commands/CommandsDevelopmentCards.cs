using Catan.Shared.Interfaces;

namespace Catan.Shared.Commands
{
    public class ShowDevelopmentCardsCommand : ICommand { }

    public class BuyDevelopmentCardCommand : ICommand { }

    public class DevelopmentCardsCanceledCommand : ICommand { }

    public class CardSelectionAcceptedCommand : ICommand { }

    public class DevelopmentCardClickedPlayedCommand : ICommand
    {
        public int DevelopmentCardId;
        public DevelopmentCardClickedPlayedCommand(int devCardId)
        {
            DevelopmentCardId = devCardId;
        }
    }
}
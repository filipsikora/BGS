using Catan.Backend.Helpers;
using Catan.Backend.Models;
using Catan.Application.Commands;
using Catan.Application.Interfaces;
using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using BGS.Shared.Dtos;
using Newtonsoft.Json.Linq;

namespace Catan.Backend.GameManagement
{
    public class CatanCommandRegistry
    {
        private readonly Dictionary<EnumCommandType, Func<JObject, ICommand>> _commandDictionary;

        public CatanCommandRegistry()
        {
            _commandDictionary = new Dictionary<EnumCommandType, Func<JObject, ICommand>>();

            Register();
        }

        private void Register()
        {
            // BankTradeCommands

            _commandDictionary[EnumCommandType.BankTradeCanceledCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new BankTradeCanceledCommand();
            };

            _commandDictionary[EnumCommandType.BankTradeOfferedResourceSelectedCommand] = json =>
            {
                var dto = Deserialize<BankTradeOfferedResourceSelectedDto>(json);

                return new BankTradeOfferedResourceSelectedCommand(dto.Type.Value);
            };

            _commandDictionary[EnumCommandType.BankTradeDesiredResourceSelectedCommand] = json =>
            {
                var dto = Deserialize<BankTradeDesiredResourceSelectedDto>(json);

                return new BankTradeDesiredResourceSelectedCommand(dto.Type);
            };

            _commandDictionary[EnumCommandType.BankTradeCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new BankTradeCommand();
            };

            // BoardCommands

            _commandDictionary[EnumCommandType.VertexClickedCommand] = json =>
            {
                var dto = Deserialize<VertexClickedDto>(json);

                return new VertexClickedCommand(dto.VertexId.Value);
            };

            _commandDictionary[EnumCommandType.EdgeClickedCommand] = json =>
            {
                var dto = Deserialize<EdgeClickedDto>(json);

                return new EdgeClickedCommand(dto.EdgeId.Value);
            };

            _commandDictionary[EnumCommandType.HexClickedCommand] = json =>
            {
                var dto = Deserialize<HexClickedDto>(json);

                return new HexClickedCommand(dto.HexId.Value);
            };

            // BuildingsCommands

            _commandDictionary[EnumCommandType.BuildVillageCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new BuildVillageCommand();
            };

            _commandDictionary[EnumCommandType.BuildRoadCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new BuildRoadCommand();
            };

            _commandDictionary[EnumCommandType.UpgradeVillageCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new UpgradeVillageCommand();
            };

            // DevelopmentCardsCommands

            _commandDictionary[EnumCommandType.ShowDevelopmentCardsCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new ShowDevelopmentCardsCommand();
            };

            _commandDictionary[EnumCommandType.BuyDevelopmentCardsCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new BuyDevelopmentCardCommand();
            };

            _commandDictionary[EnumCommandType.DevelopmentCardsCanceledCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new DevelopmentCardsCanceledCommand();
            };

            _commandDictionary[EnumCommandType.CardSelectionAcceptedCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new CardSelectionAcceptedCommand();
            };

            _commandDictionary[EnumCommandType.DevelopmentCardClickedPlayedCommand] = json =>
            {
                var dto = Deserialize<DevelopmentCardClickedPlayedDto>(json);

                return new DevelopmentCardClickedPlayedCommand(dto.DevelopmentCardId.Value);
            };

            // PhasesCommands

            _commandDictionary[EnumCommandType.StartGameCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new StartGameCommand();
            };

            // ResourceCardsCommands

            _commandDictionary[EnumCommandType.ResourceCardSelectedCommand] = json =>
            {
                var dto = Deserialize<ResourceCardSelectedDto>(json);

                return new ResourceCardSelectedCommand(dto.IsSelected.Value, dto.Type.Value);
            };

            // RobberCommands

            _commandDictionary[EnumCommandType.VictimChosenCommand] = json =>
            {
                var dto = Deserialize<VictimChosenDto>(json);

                return new VictimChosenCommand(dto.VictimId.Value);
            };

            _commandDictionary[EnumCommandType.DiscardingAcceptedCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new DiscardingAcceptedCommand();
            };

            _commandDictionary[EnumCommandType.StolenCardSelectedCommand] = json =>
            {
                var dto = Deserialize<StolenCardSelectedDto>(json);

                return new StolenCardSelectedCommand(dto.Type.Value);
            };

            _commandDictionary[EnumCommandType.TryGetDiscardingVictimCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new TryGetDiscardingVictimCommand();
            };

            // RollAndTurnCommands

            _commandDictionary[EnumCommandType.RollDiceCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new RollDiceCommand();
            };

            _commandDictionary[EnumCommandType.EndTurnCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new EndTurnCommand();
            };

            //TradeCommands

            _commandDictionary[EnumCommandType.OfferTradeCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new OfferTradeCommand();
            };

            _commandDictionary[EnumCommandType.TradeOfferCanceledCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new TradeOfferCanceledCommand();
            };

            _commandDictionary[EnumCommandType.TradeRequestAcceptedCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new TradeRequestAcceptedCommand();
            };

            _commandDictionary[EnumCommandType.RefuseTradeRequestCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new RefuseTradeRequestCommand();
            };

            _commandDictionary[EnumCommandType.RequestTradeDataCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new RequestTradeDataCommand();
            };

            _commandDictionary[EnumCommandType.AcceptTradeRequestCommand] = json =>
            {
                Deserialize<EmptyDto>(json);

                return new AcceptTradeRequestCommand();
            };

            _commandDictionary[EnumCommandType.TradePartnerChosenCommand] = json =>
            {
                var dto = Deserialize<TradePartnerChosenDto>(json);

                return new TradePartnerChosenCommand(dto.PlayerId.Value);
            };
        }

        public ICommand Create(CommandRequestDto request)
        {
            if (!Enum.TryParse<EnumCommandType>(request.Type, out var type))
                throw new Exception($"Failed to parse CommandType: {request.Type}");

            if (!_commandDictionary.TryGetValue(type, out var command))
            {
                throw new BadRequestException($"Command not registered: {request.Type}");
            }

            return command(request.Data);
        }

        private T Deserialize<T>(JObject json) where T : IValidatableDto
        {
            DtoValidation.EnsureNoExtraFields<T>(json);

            var dto = json.ToObject<T>();

            if (dto == null)
                throw new BadRequestException($"Failed to deserialize {typeof(T).Name}");

            dto.Validate();

            return dto;
        }
    }
}
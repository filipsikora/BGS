using Catan.Backend.Data;
using Catan.Backend.Models;
using Catan.Shared.Data;

namespace Catan.Backend.Mappers
{
    public static class EnumMappers
    {
        public static EnumResourceType MapResourceTypeFromDto(EnumResourceTypeDto typeDto)
        {
            return typeDto switch
            {
                EnumResourceTypeDto.Clay => EnumResourceType.Clay,
                EnumResourceTypeDto.Stone => EnumResourceType.Stone,
                EnumResourceTypeDto.Wheat => EnumResourceType.Wheat,
                EnumResourceTypeDto.Wood => EnumResourceType.Wood,
                EnumResourceTypeDto.Wool => EnumResourceType.Wool,

                _ => throw new BadRequestException($"Unknown resource type: {typeDto}")
            };
        }

        public static EnumGamePhasesDto? MapGamePhaseToDto(EnumGamePhases? phase)
        {
            if (phase == null)
                return null;

            return phase switch
            {
                EnumGamePhases.BeforeRoll => EnumGamePhasesDto.BeforeRoll,
                EnumGamePhases.FirstRoundsBuilding => EnumGamePhasesDto.FirstRoundsBuilding,
                EnumGamePhases.BankTrade => EnumGamePhasesDto.BankTrade,
                EnumGamePhases.CardDiscarding => EnumGamePhasesDto.CardDiscarding,
                EnumGamePhases.CardStealing => EnumGamePhasesDto.CardStealing,
                EnumGamePhases.RobberPlacing => EnumGamePhasesDto.RobberPlacing,
                EnumGamePhases.DevelopmentCards => EnumGamePhasesDto.DevelopmentCards,
                EnumGamePhases.MonopolyCard => EnumGamePhasesDto.MonopolyCard,
                EnumGamePhases.NormalRound => EnumGamePhasesDto.NormalRound,
                EnumGamePhases.RoadBuilding => EnumGamePhasesDto.RoadBuilding,
                EnumGamePhases.TradeOffer => EnumGamePhasesDto.TradeOffer,
                EnumGamePhases.TradeRequest => EnumGamePhasesDto.TradeRequest,
                EnumGamePhases.YearOfPlentyCard => EnumGamePhasesDto.YearOfPlentyCard,

                _ => throw new Exception($"Unknown phase: {phase}")
            };
        }
    }
}

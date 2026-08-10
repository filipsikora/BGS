namespace Catan.Shared.Data
{
    public enum EnumDomainEventDto
    {
        VillagePlacedEventPrivateDto,
        VillagePlacedEventPublicDto,

        RoadPlacedEventPrivateDto,
        RoadPlacedEventPublicDto,

        TownPlacedEventPrivateDto,
        TownPlacedEventPublicDto,

        DevCardUsedEventDto,

        DevCardBoughtEventPrivateDto,
        DevCardBoughtEventPublicDto,

        VictoryCardUsedEventDto,
        KnightCardUsedEventDto,

        CardsStolenEventThiefDto,
        CardsStolenEventVictimDto,
        CardsStolenEventPublicDto,

        CardStolenEventThiefDto,
        CardStolenEventVictimDto,
        CardStolenEventPublicDto,

        CardsDiscardedEventPrivateDto,
        CardsDiscardedPublicEventDto,

        PlayerResourcesReceivedEventPrivateDto,
        PlayerResourcesReceivedEventPublicDto,

        RoadChampionChangedEventDto,
        KnightChampionChangedEventDto,

        RolledNumberChangedEventDto,
        PhaseChangedEventDto,
        PlayersToMoveChangedEventDto,
        GameWonEventDto,

        BankTradeDoneEventDto,

        TradeDoneEventSellerDto,
        TradeDoneEventBuyerDto,
        TradeDoneEventPublicDto,

        RobberPlacedEventDto
    }
}
using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.UseCases
{
    public sealed class BlockHexLogic : BaseUseCase
    {
        public BlockHexLogic(GameSession session) : base(session) { }

        public ResultBlockHex Handle(int hexId)
        {
            var validation = RulesRobber.CanBlock(hexId, Session);

            if (!validation.Success)
            {
                return ResultBlockHex.Fail(validation.Reason);
            }

            var hex = Session.GetHexById(hexId);

            Session.BlockHexMutation(hex);

            var potentialVictimsIds = Session.GetPossibleVictimsIds();
            var canSteal = potentialVictimsIds.Count > 0;

            var result = ResultBlockHex.Ok(hex.Id, canSteal, potentialVictimsIds, null);
            result.AddDomainEvent(new RobberPlacedEvent(hexId));

            return ApplyPhase(result);
        }
    }
}
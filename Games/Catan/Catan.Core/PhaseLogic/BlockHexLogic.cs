using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BlockHexLogic : BaseLogic
    {
        public BlockHexLogic(GameSession session) : base(session) { }

        public ResultBlockHex Handle(int hexId)
        {
            var hex = Session.GetHexById(hexId);
            var validation = RulesRobber.CanBlock(hex);

            if (!validation.Success)
            {
                return ResultBlockHex.Fail(validation.Reason);
            }

            Session.BlockHexMutation(hex);

            var potentialVictimsIds = Session.GetPossibleVictimsIds();
            var canSteal = potentialVictimsIds.Count > 0;

            var result = ResultBlockHex.Ok(hex.Id, canSteal, potentialVictimsIds, null);
            result.AddDomainEvent(new RobberPlacedEvent(hexId));

            return result;
        }
    }
}
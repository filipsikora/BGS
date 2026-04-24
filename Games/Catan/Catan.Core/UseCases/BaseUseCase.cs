using Catan.Core.Results;

namespace Catan.Core.UseCases
{
    public abstract class BaseUseCase
    {
        protected readonly GameSession Session;

        protected BaseUseCase(GameSession session)
        {
            Session = session;
        }

        protected TResult ApplyPhase<TResult>(TResult result) where TResult : ResultBase
        {
            if (result.NextPhase != null)
                Session.SetCorePhase(result.NextPhase.Value);

            return result;
        }
    }
}
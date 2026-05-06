using Xunit;
using Catan.Core;
using Catan.Application;
using Catan.Shared.Data;
using Catan.Core.Engine;
using Catan.Core.Helpers;
using Catan.Backend.GameManagement;
using Catan.Application.Controllers;
using System.Security.Cryptography.X509Certificates;

namespace Catan.Tests;

public class DevCards
{
    [Fact]
    public void BuyDevCardInInitialPhase_ShouldFail()
    {
        var game = TestGame.New().InInitialiRound();

        var result = game.Facade.UseBuyDevCard();

        Assert.False(result.Success);
    }
}
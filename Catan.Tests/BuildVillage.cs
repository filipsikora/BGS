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

public class BuildVillage
{
    [Fact]
    public void BuildNormalVillageInFirstRound_ShouldFail()
    {
        var game = TestGame.New();

        var result = game.Facade.UseBuildVillage(1);

        Assert.False(result.Success);
    }

    [Fact]
    public void BuildInitialVillageInFirstRound_ShouldPass()
    {
        var game = TestGame.New();

        var result = game.Facade.UseBuildInitialVillage(1);

        Assert.True(result.Success);
    }

    [Fact]
    public void BuildInitialVillageInNormalRound_ShouldFail()
    {
        var game = TestGame.New().InNormalRound();

        var result = game.Facade.UseBuildInitialVillage(1);

        Assert.True(result.Success);
        
    }
}
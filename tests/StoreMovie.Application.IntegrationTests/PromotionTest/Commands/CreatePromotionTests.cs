﻿using StoreMovie.Api.Application.Common.Exceptions;
using StoreMovie.Api.Application.PromotionApplication.Commands.CreatePromotion;
using StoreMovie.Api.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using StoreMovie.Application.IntegrationTests;

namespace StoreMovie.Api.Application.IntegrationTests.PromotionTest.Commands;

using static Testing;

public class CreatePromotionTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreatePromotionCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreatePromotionCommand
        {
            Active = true
        });

        var command = new CreatePromotionCommand
        {
            Active = true
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreatePromotionCommand
        {
            Active = true
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Promotion>(id);

        list.Should().NotBeNull();
        list!.Active.Should().Be(command.Active);
        list.CreatedByUserId.Should().Be(userId);
        list.DateCreated.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}

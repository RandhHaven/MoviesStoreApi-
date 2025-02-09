﻿namespace StoreMovie.Api.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreMovie.Api.Domain.Entities;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.Property(t => t.PromocionName)
            .HasMaxLength(200)
            .IsRequired();
    }
}

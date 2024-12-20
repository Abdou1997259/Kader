﻿using Kader_System.Domain.Constants.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kader_System.DataAccess.Configrurations.SalaryCalculatorDetailsStoredConfig
{
    public class SpCaclauateSalaryDetailedTransConfiguration : IEntityTypeConfiguration<SpCaclauateSalaryDetailedTrans>
    {
        public void Configure(EntityTypeBuilder<SpCaclauateSalaryDetailedTrans> builder)
        {
            builder.HasNoKey();

            builder.Property(x => x.JournalType)
                .HasConversion(
                    x => x.ToString(),
                    x => (JournalType)Enum.Parse(typeof(JournalType), x)
                );
        }
    }
}

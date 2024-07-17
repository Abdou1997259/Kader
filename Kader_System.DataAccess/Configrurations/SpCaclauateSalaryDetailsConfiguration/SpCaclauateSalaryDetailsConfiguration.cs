using Kader_System.Domain.Constants.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Configrurations.SpCaclauateSalaryDetailsConfiguration
{
    public class SpCaclauateSalaryDetailsConfiguration : IEntityTypeConfiguration<SpCaclauateSalaryDetails>
    {
        public void Configure(EntityTypeBuilder<SpCaclauateSalaryDetails> builder)
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

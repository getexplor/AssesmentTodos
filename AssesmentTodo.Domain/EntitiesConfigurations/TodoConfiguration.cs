using AssesmentTodo.Domain.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Domain
{
    public class TodoConfiguration : BaseEntityConfiguration<Todo>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable($"tr{nameof(Todo)}");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace tasksWebApi.Infra.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<tasksWebApi.Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<tasksWebApi.Domain.Entities.Task> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(x => x.Id);
        }
    }
}



using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Snappfood.Domain.Entities;

namespace Snappfood.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.Id);

        //seed users
        var user1 = new User(1,"reza");
        builder.HasData(user1);
        var user2 = new User(2, "mohamad");
        builder.HasData(user2);
        var user3 = new User(3, "jack");
        builder.HasData(user3);
    }
}
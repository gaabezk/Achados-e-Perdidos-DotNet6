using com.achadoseperdidos.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace com.achadoseperdidos.Api.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("tb_usuario");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(c => c.FullName)
            .HasColumnName("nome");

        builder.Property(c => c.Email)
            .HasColumnName("email");

        builder.Property(c => c.Password)
            .HasColumnName("senha");

        builder.Property(c => c.Phone)
            .HasColumnName("telefone");

        builder.Property(c => c.Role)
            .HasColumnName("role");

        builder.Property(c => c.CodeToResetPassword)
            .HasColumnName("codigo");

//        builder.HasMany(c => c.Compras)
//            .WithOne(p => p.Pessoa)
//            .HasForeignKey(p => p.PessoaId);
    }
}
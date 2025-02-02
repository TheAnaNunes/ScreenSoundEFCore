﻿using Microsoft.EntityFrameworkCore;
using ScreenSoundSQL.Modelos;

namespace ScreenSoundSQL.Banco;
public class ScreenSoundContext(DbContextOptions<ScreenSoundContext> options) : DbContext(options)
{
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }

    private const string _connectionString = "Data Source=DESKTOP-NV3P7TJ;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder
            .UseSqlServer(_connectionString)
            .UseLazyLoadingProxies();
}

namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Text;
    using Castle.Core.Internal;
    using Data;
    using Initializer;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);




            //Test your solutions here

            //Console.WriteLine(ExportAlbumsInfo(context, 9));

            //Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();

            var albumsInfo = context.Albums
                .Where(a => a.ProducerId.HasValue && a.ProducerId.Value == producerId)
                .ToArray() //this is because of bug in EF
                .OrderByDescending(a => a.Price)
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name.IsNullOrEmpty() ? "" : $"{a.Producer.Name}",
                    Songs = a.Songs
                        .OrderByDescending(s => s.Name)
                        .ThenBy(s => s.Writer.Name)
                        .Select(s => new
                    {
                            SongName = s.Name,
                            Price = s.Price.ToString("f2"),
                            Writer = s.Writer.Name
                    }).ToArray(),
                    AlbumPrice = a.Price.ToString("f2")
                }).ToArray();

            foreach (var album in albumsInfo)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");
                int count = 1;
                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{count}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.Price}");
                    sb.AppendLine($"---Writer: {song.Writer}");
                    
                    count++;

                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice}");


            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();

            var songs = context.Songs
                .AsEnumerable()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    SongName = s.Name,
                    Writer = s.Writer.Name,
                    Performers = s.SongPerformers
                        .Select(p => $"{p.Performer.FirstName} {p.Performer.LastName}")
                        //.Select(p =>  new
                        //{
                        //    p.Performer.FirstName,
                        //    p.Performer.LastName,
                        //})
                        .OrderBy(p => p)
                        .ToList(),
                    AlbumProducer =  s.Album.Name.IsNullOrEmpty()
                    ? ""
                    : s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c"),
                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ToList();

            int count = 1;

            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{count}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.Writer}");
                if (song.Performers.Any())
                {
                    foreach (var performer in song.Performers)
                    {
                        sb.AppendLine($"---Performer: {performer}");

                        //sb.AppendLine($"---Performer: {performer.FirstName} {performer.LastName}");

                    }

                 
                }
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration}");
                count++;
            }

            return sb.ToString().TrimEnd();

        }
    }
}

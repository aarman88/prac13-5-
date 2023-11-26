using System;
using System.Collections;
using System.Linq;

class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }

    public override string ToString()
    {
        return $"{Title} by {Artist}";
    }
}

class CD
{
    public int ID { get; }
    public string Title { get; set; }
    public ArrayList Songs { get; } = new ArrayList();

    public CD(int id)
    {
        ID = id;
    }

    public override string ToString()
    {
        return $"CD {ID}: {Title}";
    }
}

class MusicCatalog
{
    private Hashtable catalog = new Hashtable();
    private int nextCDID = 1;

    public void AddCD(string title)
    {
        CD newCD = new CD(nextCDID)
        {
            Title = title
        };
        catalog[nextCDID] = newCD;
        nextCDID++;
    }

    public void RemoveCD(int cdID)
    {
        catalog.Remove(cdID);
    }

    public void AddSong(int cdID, string title, string artist)
    {
        if (catalog.ContainsKey(cdID))
        {
            CD cd = (CD)catalog[cdID];
            cd.Songs.Add(new Song { Title = title, Artist = artist });
        }
        else
        {
            Console.WriteLine($"CD with ID {cdID} not found.");
        }
    }

    public void RemoveSong(int cdID, string title, string artist)
    {
        if (catalog.ContainsKey(cdID))
        {
            CD cd = (CD)catalog[cdID];
            Song songToRemove = cd.Songs.OfType<Song>().FirstOrDefault(song => song.Title == title && song.Artist == artist);
            if (songToRemove != null)
            {
                cd.Songs.Remove(songToRemove);
            }
            else
            {
                Console.WriteLine($"Song not found on CD {cdID}.");
            }
        }
        else
        {
            Console.WriteLine($"CD with ID {cdID} not found.");
        }
    }

    public void DisplayCatalog()
    {
        foreach (CD cd in catalog.Values)
        {
            Console.WriteLine(cd);
            foreach (Song song in cd.Songs.OfType<Song>())
            {
                Console.WriteLine($"  {song}");
            }
        }
    }

    public void SearchByArtist(string artist)
    {
        Console.WriteLine($"Search results for artist {artist}:");
        foreach (CD cd in catalog.Values)
        {
            foreach (Song song in cd.Songs.OfType<Song>().Where(s => s.Artist == artist))
            {
                Console.WriteLine($"  {cd} - {song}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddCD("Classic Hits");
        catalog.AddSong(1, "Bohemian Rhapsody", "Queen");
        catalog.AddSong(1, "Hotel California", "Eagles");

        catalog.AddCD("Pop Favorites");
        catalog.AddSong(2, "Shape of You", "Ed Sheeran");
        catalog.AddSong(2, "Dance Monkey", "Tones and I");

        Console.WriteLine("Initial Catalog:");
        catalog.DisplayCatalog();

        Console.WriteLine("\nSearch by Artist 'Queen':");
        catalog.SearchByArtist("Queen");

        Console.WriteLine("\nRemoving CD 1:");
        catalog.RemoveCD(1);
        catalog.DisplayCatalog();
    }
}

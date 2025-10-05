using System;
using System.Collections.Generic;

public class InvalidSongException : Exception
{
    public InvalidSongException(string message = "invalid song") : base(message)
    { }
}

public class InvalidArtistNameExceptioon : InvalidSongException
{
    public InvalidArtistNameExceptioon() : base("artist name should be between 3 and 20 symbols") { }
}

public class InvalidSongNameException : InvalidSongException
{
    public InvalidSongNameException() : base("song name should be between 3 and 30 symbols") { }
}

public class InvalidSongLengthException : InvalidSongException
{
    public InvalidSongLengthException() : base("ivalid song length") { }

    public InvalidSongLengthException(string message) : base(message)
    {
    }
}

public class InvalidSongMinutesException : InvalidSongLengthException
{
    public InvalidSongMinutesException() : base("song minutes should be between 0 and 14") { }
}

public class InvalidSongSecondsException : InvalidSongLengthException
{
    public InvalidSongSecondsException() : base("song seconds should be between 0 and 59") { }
}

public class Song
{
    private string _artistName;
    private string _songName;
    private int _minutes;
    private int _seconds;

    public Song(string artistName, string songName, string length)
    {
        ArtistName = artistName;
        SongName = songName;

        string[] parts = length.Split(':');
        if (!int.TryParse(parts[0], out int minutes) || !int.TryParse(parts[1], out int seconds))
        {
            throw new InvalidSongLengthException();
        }
        Minutes = minutes;
        Seconds = seconds;
    }

    public string ArtistName
    {
        get { return _artistName; }
        set
        {
            if (value.Length < 3 ||  value.Length > 20)
            {
                throw new InvalidArtistNameExceptioon();
            }
            _artistName = value;
        }
    }

    public string SongName
    {
        get { return _songName; }
        set
        {
            if (value.Length < 3 || value.Length > 30)
                throw new InvalidSongNameException();
            _songName = value;
        }
    }

    public int Minutes
    {
        get { return _minutes; }
        set
        {
            if (value < 0 || value > 14)
                throw new InvalidSongMinutesException();
            _minutes = value;
        }
    }

    public int Seconds
    {
        get { return _seconds; }
        set
        {
            if (value < 0 || value > 59)
                throw new InvalidSongSecondsException();
            _seconds = value;
        }
    }

    public int TotalSeconds() { return Minutes * 60 + Seconds; }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        List<Song> playlist = new List<Song>();
        int totalSeconds = 0;

        for (int i = 0; i < n; i++)
        {
            try
            {
                string[] parts = Console.ReadLine().Split(';');
                if (parts.Length != 3)
                {
                    throw new InvalidSongException();
                }

                Song song = new Song(parts[0], parts[1], parts[2]);
                playlist.Add(song);
                totalSeconds += song.TotalSeconds();
                Console.WriteLine("song added");
            }
            catch (InvalidSongException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine($"songs added: {playlist.Count}");
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        Console.WriteLine($"playlist length: {hours}h {minutes}m {seconds}s");
    }
}
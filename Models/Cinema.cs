using System;

public class Cinema
{
	public int ID { get; set; }
	public String Name { get; set; }
	public int AmountDefault { get; set; }
    public ICollection<FilmSchedule> FilmSchedules { get; set; }

    public Cinema()
    {
        FilmSchedules = new HashSet<FilmSchedule>();
    }
}

using System;

public class FilmSchedule
{
	public int ID { get; set; }
	public int FilmID { get; set; }
	public int CinemaID { get; set; }
	public int AmountEmpty { get; set; }
	public DateTime PremiereTime { get; set; }
	public Film Film { get; set; }
	public Cinema Cinema { get; set; }
    public ICollection<Order> Orders { get; set; }

    public FilmSchedule()
    {
        Orders = new HashSet<Order>();
    }
}

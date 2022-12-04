using Midterm_Project.Models;
using System;

public class Order
{
	public int ID { get; set; }
	public String PhoneNumber { get; set; }
	public int FilmScheduleID { get; set; }
    public int AmountTicket { get; set; }
	public FilmSchedule FilmSchedule { get; set; }
}

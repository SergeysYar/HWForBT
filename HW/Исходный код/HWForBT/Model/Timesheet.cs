namespace HWForBT.Model
{
    public class Timesheet
    {
        public int Id { get; set; }
        public int Employee { get; set; }
        public int Reason { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public bool Discounted { get; set; }
        public string Description { get; set; }
    }
}

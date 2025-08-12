using System;

namespace WebCalendar.Model
{
    public class Appointment
    {
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Text { get; set; }
    }
}

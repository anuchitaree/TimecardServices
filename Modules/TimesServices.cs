namespace TimecardServices.Modules
{
    public static class TimesServices
    {

        public static string FindDayOrNight(DateTime startnow)
        {
            /*
             *  date :2022-11-12 07:29:59 => N
                * date :2022-11-12 07:30:00 => D
                * date :2022-11-12 07:30:01 => D
                * date :2022-11-12 08:30:59 => D
                * date :2022-11-12 19:29:59 => D
                * date :2022-11-12 19:30:00 => N
                * date :2022-11-12 19:30:01 => N
                * date :2022-11-12 20:30:59 => N
                * date :2022-11-13 03:30:59 => N
                * date :2022-11-13 07:29:59 => N
                * date :2022-11-13 07:30:00 => D
             */
            int yy = startnow.Year;
            int mm = startnow.Month;
            int dd = startnow.Day;
            DateTime daystart = new(yy, mm, dd, 0, 0, 0);

            DateTime dayshift = new(yy, mm, dd, 07, 30, 00);
            DateTime nightshift = new(yy, mm, dd, 19, 30, 00);

            int t1 = Convert.ToInt32((dayshift - daystart).TotalSeconds);
            int t2 = Convert.ToInt32((nightshift - daystart).TotalSeconds);

            int tnow = Convert.ToInt32((startnow - daystart).TotalSeconds);
            return (tnow >= t1 && tnow < t2) ? "D" : "N";
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace The_Mark
{
    class TimeManager
    {
        public int Year;
        public int Hour;
        public int Minute;
        public int Day;
        public int CurrentMonth;

        string hourMinutesToString;

        //const
        int daysPerMonth = 30;
        List<string> calendarMonths = new List<string>();


        public TimeManager()
        {
            Year = 0;
            Hour = 0;
            Minute = 0;
            Day = 0;
            CurrentMonth = 0;

            hourMinutesToString = hourminuteformat();

            //add calendar months
            calendarMonths.Add("Flame");
            calendarMonths.Add("Frost");
            calendarMonths.Add("Storm");
            calendarMonths.Add("Earth");
            calendarMonths.Add("Spirit");
            calendarMonths.Add("Dew");
            calendarMonths.Add("Light");
        }

        String hourminuteformat()
        {
            string hourvalue = Hour.ToString();
            string minutevalue = Minute.ToString();

            if (minutevalue.Length==1)
            {
                minutevalue = "0" + minutevalue;
            }

            string ampm = "AM";
            if (Hour >=12)
            {
                ampm = "PM";
            }

            if (Hour > 12)
            {
                hourvalue = (Hour - 12).ToString();
            }

            if (hourvalue.Length == 1)
            {
                hourvalue = "0" + hourvalue;
            }


            return hourvalue + ":" + minutevalue + " " + ampm;
        }

        public void Update()
        {

        }

        public void timeTick(int minutes)
        {
            int minutesleft = minutes;
            while (minutesleft>0)
            {
                Minute += 1;
                minutesleft -= 1;

                if (Minute==60)
                {
                    Minute = 0;
                    Hour += 1;
                    if (Hour==24)
                    {
                        Hour = 0;
                        Day += 1;
                    }
                }

                if (Day>daysPerMonth)
                {
                    Day = 1;
                    CurrentMonth += 1;
                    if (CurrentMonth==calendarMonths.Count)
                    {
                        Year += 1;
                        CurrentMonth = 0;
                    }
                }
            }

            hourMinutesToString = hourminuteformat();
        }

        public void Draw(SpriteBatch spriteBatch,SpriteFont spriteFont)
        {
            spriteBatch.DrawString(spriteFont, hourMinutesToString, new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(spriteFont, Day + " of " + calendarMonths[CurrentMonth], new Vector2(100, 150), Color.White);
        }

    }
}

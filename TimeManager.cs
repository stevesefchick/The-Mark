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

        float blue=0;
        float red=0;
        float green=0;

        float actualblue;
        float actualred;
        float actualgreen;
        float colormoveamount = 0.002f;


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

        public Vector3 returnColorInfluencesBasedOnTime()
        {
            //deep night
            if ((Hour>=1 && Hour <5))
            {
                red = -0.5f;
                green = -0.3f;
                blue = 0.3f;
            }
            //early morning
            else if (Hour>=5 && Hour < 6)
            {
                red = -0.2f;
                green = -0.1f;
                blue = 0.5f;
            }
            //sunrise
            else if (Hour >= 6 && Hour < 7)
            {
                red = 0.2f;
                green = -0.1f;
                blue = -0.1f;
            }
            //midday
            else if (Hour >= 7 && Hour < 19)
            {
                red =0;
                green = 0;
                blue = 0;
            }
            //late day
            else if (Hour >= 19 && Hour < 20)
            {
                red = 0.1f;
                green = 0;
                blue = 0.1f;
            }
            //sunset
            else if (Hour >= 20 && Hour < 21)
            {
                red = 0.2f;
                green = -0.1f;
                blue = -0.1f;
            }
            //night
            if ((Hour >= 0 && Hour < 1) || (Hour >= 21 && Hour < 24))
            {
                red = -0.4f;
                green = -0.2f;
                blue = 0.3f;
            }


            if (red > actualred)
            {
                actualred += colormoveamount;
            }
            else if (red < actualred)
            {
                actualred -= colormoveamount;
            }

            if (blue > actualblue)
            {
                actualblue += colormoveamount;
            }
            else if (blue < actualblue)
            {
                actualblue -= colormoveamount;
            }

            if (green > actualgreen)
            {
                actualgreen += colormoveamount;
            }
            else if (green < actualgreen)
            {
                actualgreen -= colormoveamount;
            }


            return new Vector3(actualred,actualgreen,actualblue);
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

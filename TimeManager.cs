﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace The_Mark
{
    class TimeManager
    {
        //time values
        public int Year;
        public int Hour;
        public int Minute;
        public int Day;
        public int CurrentMonth;
        //time to detriment
        int minutesToDetriment;
        int detrimentDelay = 0;
        const int detrimentDelayMax = 5;

        //strings
        string hourMinutesToString;
        string dayString;

        //the mark time values
        public int daysRemaining;
        public int hoursRemaining;
        public int minutesRemaining;

        //const
        const int daysPerMonth = 30;
        List<string> calendarMonths = new List<string>();

        //color values/adjustments
        float blue=0;
        float red=0;
        float green=0;
        float actualblue;
        float actualred;
        float actualgreen;
        const float colormoveamount = 0.001f;


        public TimeManager()
        {
            Year = 0;
            Hour = 7;
            Minute = 0;
            Day = 1;
            CurrentMonth = 0;

            hourMinutesToString = Hourminuteformat();
            dayString = Daystringformat();

            //add calendar months
            calendarMonths.Add("Flame");
            calendarMonths.Add("Frost");
            calendarMonths.Add("Storm");
            calendarMonths.Add("Earth");
            calendarMonths.Add("Spirit");
            calendarMonths.Add("Dew");
            calendarMonths.Add("Light");
        }

        String Hourminuteformat()
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

        public void StartTimer()
        {
            daysRemaining = 14;
            hoursRemaining = 0;
            minutesRemaining = 0;

        }

        String Daystringformat()
        {
            String ending = "";

            if (Day == 1 || Day == 21)
            {
                ending = "st";
            }
            else if (Day==2 || Day == 22)
            {
                ending = "nd";
            }
            else if (Day==3 || Day == 23)
            {
                ending = "rd";
            }    
            else if (Day==4 || Day==5 || Day==6 || Day==7 || Day==8 || Day==9 || Day==10 || Day==11 || Day==12 || Day==13 || Day==14 || Day==15 || Day==16||
                Day==17 || Day==18 || Day==19 || Day==20 || Day==24 || Day==25 || Day==26 || Day==27 || Day==28 || Day==29 || Day==30)
            {
                ending = "th";
            }


            return Day + ending;
        }

        public void Update()
        {
            if (minutesToDetriment>0 && detrimentDelay<=0)
            {
                    #region time calculation
                    Minute += 1;

                    if (Minute == 60)
                    {
                        Minute = 0;
                        Hour += 1;
                        if (Hour == 24)
                        {
                            Hour = 0;
                            Day += 1;
                        }
                    }

                    if (Day > daysPerMonth)
                    {
                        Day = 1;
                        CurrentMonth += 1;
                        if (CurrentMonth == calendarMonths.Count)
                        {
                            Year += 1;
                            CurrentMonth = 0;
                        }
                    }
                    #endregion

                    #region countdown time calculation
                    if (minutesRemaining > 0)
                    {
                        minutesRemaining -= 1;
                    }
                    else
                    {
                        minutesRemaining = 59;
                        if (hoursRemaining > 0)
                        {
                            hoursRemaining -= 1;
                        }
                        else
                        {
                            hoursRemaining = 23;
                            if (daysRemaining > 0)
                            {
                                daysRemaining -= 1;
                            }
                        }

                    }


                    #endregion
                

                hourMinutesToString = Hourminuteformat();
                dayString = Daystringformat();


                minutesToDetriment -= 1;
                detrimentDelay = detrimentDelayMax;
            }
            else if (minutesToDetriment>0 && detrimentDelay>0)
            {
                if (minutesToDetriment >= 20)
                {
                    detrimentDelay -= 5;
                }
                else if (minutesToDetriment >= 10)
                {
                    detrimentDelay -= 3;
                }
                else
                {
                    detrimentDelay -= 1;
                }
            }
        }

        public Vector3 returnColorInfluencesBasedOnTime()
        {
            //deep night
            if ((Hour>=0 && Hour <5))
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
            else if (Hour >= 7 && Hour < 12)
            {
                red =0;
                green = 0;
                blue = 0;
            }
            //late day
            else if (Hour >= 12 && Hour < 17)
            {
                red = 0.1f;
                green = 0;
                blue = 0.1f;
            }
            //sunset
            else if (Hour >= 17 && Hour < 18)
            {
                red = 0.2f;
                green = -0.1f;
                blue = -0.1f;
            }
            //night
            if (Hour >= 18 && Hour < 24)
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
            minutesToDetriment += minutes;

        }

        public Boolean OKToSleepTick()
        {
            if (minutesToDetriment>20)
            {
                return false;
            }
            else
            {
                return true;
            }    
        }



        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 baseposition)
        {
            spriteBatch.DrawString(spriteFont, hourMinutesToString, new Vector2(baseposition.X + 10, baseposition.Y + 30), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(spriteFont, hourMinutesToString, new Vector2(baseposition.X + 11, baseposition.Y + 31), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(spriteFont, hourMinutesToString, new Vector2(baseposition.X + 12, baseposition.Y + 32), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);

            spriteBatch.DrawString(spriteFont, dayString + " day of " + calendarMonths[CurrentMonth], new Vector2(baseposition.X + 200, baseposition.Y + 30), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(spriteFont, dayString + " day of " + calendarMonths[CurrentMonth], new Vector2(baseposition.X + 201, baseposition.Y + 31), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(spriteFont, dayString + " day of " + calendarMonths[CurrentMonth], new Vector2(baseposition.X + 202, baseposition.Y + 32), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);

            DrawRemainingTime(spriteBatch, spriteFont,baseposition);
        }

        void DrawRemainingTime(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 baseposition)
        {
            spriteBatch.DrawString(spriteFont, daysRemaining + " days " + hoursRemaining + " hours " + minutesRemaining + " minutes remain", baseposition, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(spriteFont, daysRemaining + " days " + hoursRemaining + " hours " + minutesRemaining + " minutes remain", new Vector2(baseposition.X+1,baseposition.Y+1), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(spriteFont, daysRemaining + " days " + hoursRemaining + " hours " + minutesRemaining + " minutes remain", new Vector2(baseposition.X + 2, baseposition.Y + 2), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
        }

    }
}

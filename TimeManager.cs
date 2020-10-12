using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace The_Mark
{
    class TimeManager
    {
        public int Hour;
        public int Minute;
        string hourMinutesToString;

        public TimeManager()
        {
            Hour = 0;
            Minute = 0;
            hourMinutesToString = hourminuteformat();
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
                    }
                }
            }

            hourMinutesToString = hourminuteformat();
        }

        public void Draw(SpriteBatch spriteBatch,SpriteFont spriteFont)
        {
            spriteBatch.DrawString(spriteFont, hourMinutesToString, new Vector2(100, 100), Color.White);
        }

    }
}

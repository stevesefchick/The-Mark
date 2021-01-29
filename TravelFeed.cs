using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class TravelFeed
    {
        //lists
        List<String> feedItems = new List<String>();
        List<int> feedItemDecayTimer = new List<int>();

        //consts
        const int totalDecayTime = 300;

        public TravelFeed()
        {


        }

        public void AddTravelItem(String text)
        {
            feedItems.Add(text);
            feedItemDecayTimer.Add(totalDecayTime);
        }

        void RemoveTravelItem(int arrayrecord)
        {
            feedItems.RemoveAt(arrayrecord);
            feedItemDecayTimer.RemoveAt(arrayrecord);
        }

        public void Update()
        {
            for (int i =0;i < feedItems.Count;++i)
            {
                feedItemDecayTimer[i] -= 1;
                if (feedItemDecayTimer[i]==0)
                {
                    RemoveTravelItem(i);
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 baseLocation,SpriteFont font)
        {
            for (int i =0; i < feedItems.Count; ++i)
            {
                spriteBatch.DrawString(font, feedItems[i], new Vector2(baseLocation.X + 25, baseLocation.Y + 180 + (i * 25)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(font, feedItems[i], new Vector2(baseLocation.X + 26, baseLocation.Y + 181 + (i * 25)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(font, feedItems[i], new Vector2(baseLocation.X + 27, baseLocation.Y + 182 + (i * 25)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            }

        }
    }
}

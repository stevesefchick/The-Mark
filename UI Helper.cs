using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UI_Helper
    {
        //enums
        public enum UIWindowCreationTypes { CharacterStatWindow, CharacterEquipmentWindow, CharacterSkillsWindow }


        //fonts
        SpriteFont titleFont;
        SpriteFont mainFont;

        Vector2 backbuffersize;

        //Sprites
        Texture2D uiWindowSprites;
        Texture2D healtbarSprites;
        Texture2D equipmentHandsSpriteSheet;
        Texture2D equipmentHeadSpriteSheet;
        Texture2D equipmentBodySpriteSheet;
        Texture2D equipmentJewelrySpriteSheet;
        Texture2D equipmentTrinketSpriteSheet;
        //hover cards
        List<UIHoverCard> hoverCards = new List<UIHoverCard>();
        //ui windows
        List<UIWindow> uiWindows = new List<UIWindow>();
        //character bubbles
        List<UICharacterBubble> uiCharacterBubbles = new List<UICharacterBubble>();

        public UI_Helper(GameMain thegame,SpriteFont bigfont, SpriteFont babyfont)
        {
            titleFont = bigfont;
            mainFont = babyfont;
            backbuffersize = thegame.backbufferJamz;

            LoadAllTextures(thegame);
            //the mark
            uiCharacterBubbles.Add(new UICharacterBubble(new Vector2(returnLocationBasedonBackBuffer(0.05f,true,0,0), backbuffersize.Y - 100 -  returnLocationBasedonBackBuffer(0.15f,false,0,0)),true));
            //party members
            for (int i =0; i < 5;++i)
            {
                uiCharacterBubbles.Add(new UICharacterBubble(new Vector2(returnLocationBasedonBackBuffer(0.05f, true,0,0) + 100 +  (i * 100), backbuffersize.Y - 100 - returnLocationBasedonBackBuffer(0.15f, false,0,0)),false));

            }

            //createHoverCard(new Vector2(200,200), "Horse Crimes", "you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine");
            //createUIWindow(new Rectangle(600, 200, 200, 300));
        }

        public void assignMarkToPartyWindow(String id)
        {
            uiCharacterBubbles[0].associatedCharacter = id;
        }

        int returnLocationBasedonBackBuffer(float percentage,Boolean isX, int XOffset, int YOffset)
        {
            if (isX==true)
            {
                return (int)((percentage * backbuffersize.X) + XOffset);
            }
            else
            {
                return (int)((percentage * backbuffersize.Y) + YOffset);
            }
        }

        public void createHoverCard(Vector2 position, String title, String body)
        {
            hoverCards.Add(new UIHoverCard(position, title, body,mainFont,titleFont));
        }
        public void createHoverCard(UIHoverCard existingcard)
        {
            hoverCards.Add(existingcard);
        }

        public void createUIWindow(Rectangle position)
        {
            uiWindows.Add(new UIWindow(position));
        }

        //Create Window for player stats
        public void createUIWindow(UIWindowCreationTypes creationType, Boolean isMark,int associatedChar, PlayerHandler playerhandler)
        {
            if (creationType == UIWindowCreationTypes.CharacterStatWindow)
            {
                UIWindow newwindow = new UIWindow(new Rectangle(returnLocationBasedonBackBuffer(0.5f,true,-250,0), returnLocationBasedonBackBuffer(0.25f,false,0,-100), 500, 400));
                

                //character name / title
                String charname;
                if (isMark == true)
                {
                    charname = playerhandler.theMark.personFirstName + " " + playerhandler.theMark.personLastName;
                    newwindow.isTheMark = true;
                }
                else
                {
                    charname = playerhandler.partyMembers[associatedChar].personFirstName + " " + playerhandler.partyMembers[associatedChar].personLastName;
                    newwindow.partymember = associatedChar;
                }
                newwindow.AssignTitle(charname, titleFont);

                //the mark flag
                if (isMark == true)
                {
                    newwindow.AssignTextBody("The Mark", new Vector2(0, 35), Color.Gold, UIWindow.UIWindowAlignmentType.Center,mainFont);
                }

                //health stats
                String health;
                String stamina;
                String stress;
                //equipment stats
                String attack;
                String defense;
                String ability;
                if (isMark==true)
                {
                    health = playerhandler.theMark.returnHealthString();
                    stamina = playerhandler.theMark.returnStaminaString();
                    stress = playerhandler.theMark.returnStressString();
                    attack = playerhandler.theMark.returnAttackString();
                    defense = playerhandler.theMark.returnDefenseString();
                    ability = playerhandler.theMark.returnAbilityString();
                }
                else
                {
                    health = playerhandler.partyMembers[associatedChar].returnHealthString();
                    stamina = playerhandler.partyMembers[associatedChar].returnStaminaString();
                    stress = playerhandler.partyMembers[associatedChar].returnStressString();
                    attack = playerhandler.partyMembers[associatedChar].returnAttackString();
                    defense = playerhandler.partyMembers[associatedChar].returnDefenseString();
                    ability = playerhandler.partyMembers[associatedChar].returnAbilityString();
                }
                //health
                newwindow.AssignTextBody("Health", new Vector2(0, 75), Color.White, UIWindow.UIWindowAlignmentType.Left, mainFont);
                newwindow.AssignTextBody(health, new Vector2(100, 75), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X+0, newwindow.uiWindowPosition.Y + 75, 175, 25), new UIHoverCard(new Vector2(50, 75), "Health", "Health is a character's overall well being, and is reduced by painful activities or by taking damage in combat. If this value reaches rock bottom, the character will die.", mainFont, titleFont));
                //stamina
                newwindow.AssignTextBody("Stamina", new Vector2(0, 100), Color.White, UIWindow.UIWindowAlignmentType.Left, mainFont);
                newwindow.AssignTextBody(stamina, new Vector2(100, 100), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 0, newwindow.uiWindowPosition.Y + 100, 175, 25), new UIHoverCard(new Vector2(50, 75), "Stamina", "Stamina is a character's physical fatigue. Sleeping recovers Stamina, while this is depleted through physical activities. If this falls low enough, a character's Health will be affected.", mainFont, titleFont));
                //stress
                newwindow.AssignTextBody("Stress", new Vector2(0, 125), Color.White, UIWindow.UIWindowAlignmentType.Left, mainFont);
                newwindow.AssignTextBody(stress, new Vector2(100, 125), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 0, newwindow.uiWindowPosition.Y + 125, 175, 25), new UIHoverCard(new Vector2(50, 75), "Stress", "Stress is a character's mental fatigue. Low Stress is considered good, while high Stress will cause negative effects to themselves and the party.", mainFont, titleFont));


                //equipment stats
                //attack
                newwindow.AssignTextBody("Attack", new Vector2(250, 75), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(attack, new Vector2(400, 75), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 250, newwindow.uiWindowPosition.Y + 75, 175, 25), new UIHoverCard(new Vector2(50, 75), "Attack", "Attack is a character's overall physical power. This allows them to deal more physical damage during combat.", mainFont, titleFont));
                //defense
                newwindow.AssignTextBody("Defense", new Vector2(250, 100), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(defense, new Vector2(400, 100), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 250, newwindow.uiWindowPosition.Y + 100, 175, 25), new UIHoverCard(new Vector2(50, 75), "Defense", "Defense is a character's overall resistance to damage. This reduces the amount of Health lost from being attacked in combat.", mainFont, titleFont));
                //ability
                newwindow.AssignTextBody("Ability", new Vector2(250, 125), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(ability, new Vector2(400, 125), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 250, newwindow.uiWindowPosition.Y + 125, 175, 25), new UIHoverCard(new Vector2(50, 75), "Ability", "Ability is a character's prowess in specialized abilities, used during combat.", mainFont, titleFont));



                //personal stats
                //endurance
                newwindow.AssignTextBody("Endurance", new Vector2(250, 175), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody("Good", new Vector2(400, 175), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                //strength
                newwindow.AssignTextBody("Strength", new Vector2(250, 200), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody("Good", new Vector2(400, 200), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                //dexterity
                newwindow.AssignTextBody("Dexterity", new Vector2(250, 225), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody("Good", new Vector2(400, 225), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                //wit
                newwindow.AssignTextBody("Wit", new Vector2(250, 250), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody("Good", new Vector2(400, 250), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                //wisdom
                newwindow.AssignTextBody("Wisdom", new Vector2(250, 275), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody("Good", new Vector2(400, 275), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                //charisma
                newwindow.AssignTextBody("Charisma", new Vector2(250, 300), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody("Good", new Vector2(400, 300), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);




                //tabs
                newwindow.CreateTab(0, "Health & Stats",true, UIWindowCreationTypes.CharacterStatWindow);
                newwindow.CreateTab(180, "Equipment",false, UIWindowCreationTypes.CharacterEquipmentWindow);
                newwindow.CreateTab(360, "Skills & Traits",false, UIWindowCreationTypes.CharacterEquipmentWindow);



                newwindow.switchForegroundBackground(true);

                uiWindows.Add(newwindow);
            }
            else if (creationType == UIWindowCreationTypes.CharacterEquipmentWindow)
            {
                UIWindow newwindow = new UIWindow(new Rectangle(returnLocationBasedonBackBuffer(0.5f, true, -250, 0), returnLocationBasedonBackBuffer(0.25f, false, 0, -100), 500, 400));

                //character name / title
                String charname;
                if (isMark == true)
                {
                    charname = playerhandler.theMark.personFirstName + " " + playerhandler.theMark.personLastName;
                    newwindow.isTheMark = true;
                }
                else
                {
                    charname = playerhandler.partyMembers[associatedChar].personFirstName + " " + playerhandler.partyMembers[associatedChar].personLastName;
                    newwindow.partymember = associatedChar;
                }
                newwindow.AssignTitle(charname, titleFont);

                //the mark flag
                if (isMark == true)
                {
                    newwindow.AssignTextBody("The Mark", new Vector2(0, 35), Color.Gold, UIWindow.UIWindowAlignmentType.Center, mainFont);
                }


                //equipment stats
                String attack;
                String defense;
                String ability;
                String hands;
                Vector2 handsheet;
                String body;
                Vector2 bodysheet;
                String head;
                Vector2 headsheet;
                String jewelry;
                Vector2 jewelrysheet;
                String trinket;
                Vector2 trinketsheet;

                if (isMark == true)
                {
                    attack = playerhandler.theMark.returnAttackString();
                    defense = playerhandler.theMark.returnDefenseString();
                    ability = playerhandler.theMark.returnAbilityString();
                    hands = playerhandler.theMark.handsEquipment.itemName;
                    handsheet = playerhandler.theMark.handsEquipment.itemSpriteSheetXY;
                    head = playerhandler.theMark.headEquipment.itemName;
                    headsheet = playerhandler.theMark.headEquipment.itemSpriteSheetXY;
                    body = playerhandler.theMark.bodyEquipment.itemName;
                    bodysheet = playerhandler.theMark.bodyEquipment.itemSpriteSheetXY;
                    jewelry = playerhandler.theMark.jewelryEquipment.itemName;
                    jewelrysheet = playerhandler.theMark.jewelryEquipment.itemSpriteSheetXY;
                    trinket = playerhandler.theMark.trinketEquipment.itemName;
                    trinketsheet = playerhandler.theMark.trinketEquipment.itemSpriteSheetXY;

                }
                else
                {
                    attack = playerhandler.partyMembers[associatedChar].returnAttackString();
                    defense = playerhandler.partyMembers[associatedChar].returnDefenseString();
                    ability = playerhandler.partyMembers[associatedChar].returnAbilityString();
                    hands = playerhandler.partyMembers[associatedChar].handsEquipment.itemName;
                    handsheet = playerhandler.partyMembers[associatedChar].handsEquipment.itemSpriteSheetXY;
                    head = playerhandler.partyMembers[associatedChar].headEquipment.itemName;
                    headsheet = playerhandler.partyMembers[associatedChar].headEquipment.itemSpriteSheetXY;
                    body = playerhandler.partyMembers[associatedChar].bodyEquipment.itemName;
                    bodysheet = playerhandler.partyMembers[associatedChar].bodyEquipment.itemSpriteSheetXY;
                    jewelry = playerhandler.partyMembers[associatedChar].jewelryEquipment.itemName;
                    jewelrysheet = playerhandler.partyMembers[associatedChar].jewelryEquipment.itemSpriteSheetXY;
                    trinket = playerhandler.partyMembers[associatedChar].trinketEquipment.itemName;
                    trinketsheet = playerhandler.partyMembers[associatedChar].trinketEquipment.itemSpriteSheetXY;

                }
                //attack
                newwindow.AssignTextBody("Attack", new Vector2(250, 75), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(attack, new Vector2(400, 75), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 250, newwindow.uiWindowPosition.Y + 75, 175, 25), new UIHoverCard(new Vector2(50, 75), "Attack", "Attack is a character's overall physical power. This allows them to deal more physical damage during combat.", mainFont, titleFont));
                //defense
                newwindow.AssignTextBody("Defense", new Vector2(250, 100), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(defense, new Vector2(400, 100), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 250, newwindow.uiWindowPosition.Y + 100, 175, 25), new UIHoverCard(new Vector2(50, 75), "Defense", "Defense is a character's overall resistance to damage. This reduces the amount of Health lost from being attacked in combat.", mainFont, titleFont));
                //ability
                newwindow.AssignTextBody("Ability", new Vector2(250, 125), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(ability, new Vector2(400, 125), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.hoverCardCollision.Add(new Rectangle(newwindow.uiWindowPosition.X + 250, newwindow.uiWindowPosition.Y + 125, 175, 25), new UIHoverCard(new Vector2(50, 75), "Ability", "Ability is a character's prowess in specialized abilities, used during combat.", mainFont, titleFont));

                //equipment
                newwindow.AssignTextBody("Hands", new Vector2(0, 75), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(hands, new Vector2(35, 100), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.CreateEquipmentSprite(new Vector2(0, 95), handsheet, UIWindow.EquipmentSpriteSheet.Hands);

                newwindow.AssignTextBody("Head", new Vector2(0, 125), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(head, new Vector2(35, 150), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.CreateEquipmentSprite(new Vector2(0, 145), headsheet, UIWindow.EquipmentSpriteSheet.Head);

                newwindow.AssignTextBody("Body", new Vector2(0, 175), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(body, new Vector2(35, 200), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.CreateEquipmentSprite(new Vector2(0, 195), bodysheet, UIWindow.EquipmentSpriteSheet.Body);

                newwindow.AssignTextBody("Jewelry", new Vector2(0, 225), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(jewelry, new Vector2(35, 250), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.CreateEquipmentSprite(new Vector2(0, 245), jewelrysheet, UIWindow.EquipmentSpriteSheet.Jewelry);

                newwindow.AssignTextBody("Trinket", new Vector2(0, 275), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.AssignTextBody(trinket, new Vector2(35, 300), Color.White, UIWindow.UIWindowAlignmentType.Normal, mainFont);
                newwindow.CreateEquipmentSprite(new Vector2(0, 295), trinketsheet, UIWindow.EquipmentSpriteSheet.Trinket);

                //tabs
                newwindow.CreateTab(0, "Health & Stats", false, UIWindowCreationTypes.CharacterStatWindow);
                newwindow.CreateTab(180, "Equipment", true, UIWindowCreationTypes.CharacterEquipmentWindow);
                newwindow.CreateTab(360, "Skills & Traits", false, UIWindowCreationTypes.CharacterEquipmentWindow);

                newwindow.switchForegroundBackground(true);
                uiWindows.Add(newwindow);

            }
            else if (creationType == UIWindowCreationTypes.CharacterSkillsWindow)
            {
                UIWindow newwindow = new UIWindow(new Rectangle(returnLocationBasedonBackBuffer(0.5f, true, -250, 0), returnLocationBasedonBackBuffer(0.25f, false, 0, -100), 500, 400));

                //character name / title
                String charname;
                if (isMark == true)
                {
                    charname = playerhandler.theMark.personFirstName + " " + playerhandler.theMark.personLastName;
                    newwindow.isTheMark = true;
                }
                else
                {
                    charname = playerhandler.partyMembers[associatedChar].personFirstName + " " + playerhandler.partyMembers[associatedChar].personLastName;
                    newwindow.partymember = associatedChar;
                }
                newwindow.AssignTitle(charname, titleFont);

                //the mark flag
                if (isMark == true)
                {
                    newwindow.AssignTextBody("The Mark", new Vector2(0, 35), Color.Gold, UIWindow.UIWindowAlignmentType.Center, mainFont);
                }



                //tabs
                newwindow.CreateTab(0, "Health & Stats", false, UIWindowCreationTypes.CharacterStatWindow);
                newwindow.CreateTab(180, "Equipment", false, UIWindowCreationTypes.CharacterEquipmentWindow);
                newwindow.CreateTab(360, "Skills & Traits", true, UIWindowCreationTypes.CharacterEquipmentWindow);


                newwindow.switchForegroundBackground(true);
                uiWindows.Add(newwindow);
            }

        }

        void LoadAllTextures(GameMain thegame)
        {
            uiWindowSprites = thegame.Content.Load<Texture2D>("Sprites/UI/uiWindow");
            healtbarSprites = thegame.Content.Load<Texture2D>("Sprites/UI/uiHealthBars");
            equipmentHandsSpriteSheet = thegame.Content.Load<Texture2D>("Sprites/UI/equipmentHandsIcons");
            equipmentHeadSpriteSheet = thegame.Content.Load<Texture2D>("Sprites/UI/equipmentHeadIcons");
            equipmentBodySpriteSheet = thegame.Content.Load<Texture2D>("Sprites/UI/equipmentBodyIcons");
            equipmentJewelrySpriteSheet = thegame.Content.Load<Texture2D>("Sprites/UI/equipmentJewelryIcons");
            equipmentTrinketSpriteSheet = thegame.Content.Load<Texture2D>("Sprites/UI/equipmentTrinketIcons");

        }

        //normal
        //calculates position based on position and camera offset
        public Rectangle getUIPosition(Rectangle position, Vector2 offsetposition)
        {
            Rectangle posish = new Rectangle((int)(position.X + offsetposition.X), (int)(position.Y + offsetposition.Y), position.Width, position.Height);

            return posish;
        }

        //for tabs
        //gets base + tab + offset position
        public Rectangle getUIPosition(Rectangle tabposition, Rectangle baseposition, Vector2 offsetposition)
        {
            Rectangle posish = new Rectangle((int)(tabposition.X + baseposition.X + offsetposition.X), (int)(tabposition.Y + baseposition.Y + offsetposition.Y), tabposition.Width, tabposition.Height);

            return posish;
        }

        void ClickTabs(int window, int partymember, Boolean isMark, String nameOfTab,PlayerHandler playerhandler)
        {
            if (nameOfTab=="Equipment")
            {
                createUIWindow(UIWindowCreationTypes.CharacterEquipmentWindow, isMark, partymember, playerhandler);
                uiWindows.RemoveAt(window);
            }
            else if (nameOfTab == "Health & Stats")
            {
                createUIWindow(UIWindowCreationTypes.CharacterStatWindow, isMark, partymember, playerhandler);
                uiWindows.RemoveAt(window);
            }
            else if (nameOfTab == "Skills & Traits")
            {
                createUIWindow(UIWindowCreationTypes.CharacterSkillsWindow, isMark, partymember, playerhandler);
                uiWindows.RemoveAt(window);
            }
        }

        public void Update(MouseHandler mouse, PlayerHandler party,Vector2 offset)
        {
            if (mouse.isLeftClickDown == true)
            {
                //Hovercards
                hoverCards.Clear();

                //UI Windows
                for (int i = 0; i < uiWindows.Count;++i)
                {
                    foreach (KeyValuePair<Rectangle, UIHoverCard> r in uiWindows[i].hoverCardCollision)
                    {
                        if (mouse.leftMouseClickPosition.Intersects(getUIPosition(r.Key, offset)))
                        {
                            createHoverCard(r.Value);
                        }
                    }

                    for (int t = 0; t < uiWindows[i].uiWindowTabs.Count; ++t)
                    {
                            if (mouse.leftMouseClickPosition.Intersects(getUIPosition(uiWindows[i].uiWindowTabs[t], uiWindows[i].uiWindowPosition, offset)))
                            {
                                ClickTabs(i, uiWindows[i].partymember, uiWindows[i].isTheMark, uiWindows[i].uiWindowsTabsText[t], party);
                                break;
                            }
                    }

                    if (mouse.leftMouseClickPosition.Intersects(uiWindows[i].publicxButtonPosition) == true && hoverCards.Count==0)
                    {
                        uiWindows.RemoveAt(i);
                    }

                }

                //Character Bubbles
                for (int i = 0; i < uiCharacterBubbles.Count;++i)
                {
                    if (mouse.leftMouseClickPosition.Intersects(getUIPosition(uiCharacterBubbles[i].bubblePosition, offset)) == true &&
                        uiCharacterBubbles[i].isActive == true &&
                        uiWindows.Count==0)
                    {
                        if (i == 0)
                        {
                            createUIWindow(UIWindowCreationTypes.CharacterStatWindow, true, 0,party);
                        }
                        else
                        {
                            createUIWindow(UIWindowCreationTypes.CharacterStatWindow, false, i,party);
                        }
                    }

                    if (i==0)
                    {
                        uiCharacterBubbles[i].HealthScale = party.returnHealthValuesForMark().Item3;
                        uiCharacterBubbles[i].StaminaScale = party.returnStaminaValuesForMark().Item3;
                        uiCharacterBubbles[i].StressScale = party.returnStressValuesForMark().Item3;
                    }
                    else if (uiCharacterBubbles[i].isActive==true)
                    {
                        uiCharacterBubbles[i].HealthScale = party.returnHealthValuesForPartyMember(i).Item3;
                        uiCharacterBubbles[i].StaminaScale = party.returnStaminaValuesForPartyMember(i).Item3;
                        uiCharacterBubbles[i].StressScale = party.returnStressValuesForPartyMember(i).Item3;
                    }
                }

            }

            


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            //Hovercards
            for (int i =0; i < hoverCards.Count;++i)
            {
                hoverCards[i].Draw(spriteBatch, uiWindowSprites, getUIPosition(hoverCards[i].hovercardPosition, offset),titleFont,mainFont);

            }
            //UI Windows
            for (int i = 0; i < uiWindows.Count;++i)
            {
                uiWindows[i].Draw(spriteBatch, uiWindowSprites, getUIPosition(uiWindows[i].uiWindowPosition, offset),titleFont,mainFont,
                    equipmentHandsSpriteSheet,equipmentBodySpriteSheet,equipmentHeadSpriteSheet,equipmentJewelrySpriteSheet,equipmentTrinketSpriteSheet);

            }
            //character bubbles
            for (int i = 0; i < uiCharacterBubbles.Count;++i)
            {
                uiCharacterBubbles[i].Draw(spriteBatch, uiWindowSprites, healtbarSprites, getUIPosition(uiCharacterBubbles[i].bubblePosition, offset));
            }

        }

    }
}

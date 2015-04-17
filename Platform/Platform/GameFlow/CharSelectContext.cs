﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.UserInterface;
using Platform.Characters;
using Platform.Mobs;
using Platform.World;

namespace Platform.GameFlow
{
    class CharSelectContext: GameContext
    {
        private enum CharType { Spring, Summer, Autumn, Winter, None, Test }

        int level = 1;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        UIComponent gui;

        UILabel modeTitle;

        UIComponent charSplash;
        UILabel charName;
        UILabel charDesc;

        UIButton playButton;

        private CharType selected;

        public CharSelectContext()
        {
            selected = CharType.None;

            gui = new UIComponent();
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.Gray;

            UIButton backButton = new UIButton(new Rectangle(0, 0, 100, 50), delegate()
            {
                Game1.CurrentGame.GameMode = new MainMenuContext(); 
            }, "Back");
            gui.Add(backButton);

            modeTitle = new UILabel();
            modeTitle.bounds = new Rectangle(100, 0, Game1.CurrentGame.Window.ClientBounds.Width - 100, 50);
            modeTitle.text = "Character Select";
            modeTitle.hAlign = HorizontalTextAlignment.Center;
            modeTitle.vAlign = VerticalTextAlignment.Center;
            gui.Add(modeTitle);

            charSplash = new UIComponent();
            charSplash.bounds = new Rectangle(
                Game1.CurrentGame.Window.ClientBounds.Width - 600,
                Game1.CurrentGame.Window.ClientBounds.Height - 650,
                600, 650);
            charSplash.color = Color.LightGray;
            charSplash.depth = 0;
            gui.Add(charSplash);
            
            charDesc = new UILabel();
            charDesc.bounds = new Rectangle(10, Game1.CurrentGame.Window.ClientBounds.Height - 260, Game1.CurrentGame.Window.ClientBounds.Width - 20, 250); 
            gui.Add(charDesc);

            charName = new UILabel();
            charName.bounds = new Rectangle(Game1.CurrentGame.Window.ClientBounds.Width - 260, charDesc.bounds.Y - 90, 250, 80);
            charName.hAlign = HorizontalTextAlignment.Right;
            charName.vAlign = VerticalTextAlignment.Center;
            charName.Border = UIBorder.Scroll;
            charName.borderSize = 20;
            gui.Add(charName);
            UIButton testSelect = new UIButton(new Rectangle(370, 260, 150, 150), delegate() { selected = CharType.Test; charName.text = "testPlayer"; charDesc.text = "testDesc"; }, "test");
            gui.Add(testSelect);

            UIButton springSelect = new UIButton(new Rectangle(50, 100, 150, 150), delegate() {
                selected = CharType.Spring;
                charName.text = "Anoixi";
                charDesc.text = "A member of royalty for the kingdom of the Geogon, or Earthborn, who have a special affinity\nwith plants and the natural world. However, humans have indirectly wiped out her race as a\nresult of industrialization.";
                //TODO: set splash for selected char
            }, "Spring");
            gui.Add(springSelect);

            UIButton summerSelect = new UIButton(new Rectangle(210, 100, 150, 150), delegate() {
                selected = CharType.Summer;
                charName.text = "Kalokairi";
                charDesc.text = "Formerly a priest to Pelor, the Sun god. He died a heroic death protecting a city from invasion\nby demons.";
                //TODO: set splash for selected char

            }, "Summer");
            gui.Add(summerSelect);

            UIButton autumnSelect = new UIButton(new Rectangle(210, 260, 150, 150), delegate() {
                selected = CharType.Autumn;
                charName.text = "Akichira";
                charDesc.text = "A poor, kindhearted girl, the Titan of Autumn took pity on her for her kindness when she starved\nto death, and made her his envoy.";
                //TODO: set splash for selected char
            }, "Autumn");
            gui.Add(autumnSelect);

            UIButton winterSelect = new UIButton(new Rectangle(50, 260, 150, 150), delegate() {
                selected = CharType.Winter;
                charName.text = "Zima";
                charDesc.text = "During an enemy raid of his city, Zimo\'s wife sacrificed herself to save him. Stricken with\ngrief, Zimo hung himself. By the time the previous Envoy of Winter found him, Zimo was already\npartially gone.";
                //TODO: set splash for selected char
            }, "Winter");
            gui.Add(winterSelect);

            playButton = new UIButton(new Rectangle(370, 100, 150, 60), delegate() {
                CombatContext nextSlide = new CombatContext();
                Player ploy = new TestPlayer();
                Game1.CurrentGame.GameMode = nextSlide;
                switch (selected){
                    case CharType.Spring: ploy = new SpringCharacter(); break;
                    case CharType.Summer: ploy = new SummerCharacter(); break;
                    case CharType.Autumn: ploy = new AutumnCharacter(); break;
                    case CharType.Winter: ploy = new WinterCharacter(); break;
                    case CharType.Test: ploy = new TestPlayer(); break;
                    default: Game1.CurrentGame.Player = new TestPlayer(); break;
                }
                
                Game1.CurrentGame.Player = ploy;
                
                nextSlide.CombatWorld = Map.LoadMap2(@"Content/maps/Level"+level+".txt");
                nextSlide.CombatWorld.Camera.PointOnScreen = new Point(Game1.CurrentGame.Window.ClientBounds.Width / 2, Game1.CurrentGame.Window.ClientBounds.Height / 2);

            }, "Start Game");
            playButton.visible = false;
            gui.Add(playButton);


        }

        public override void Update(GameTime gameTime)
        {
            playButton.visible = selected != CharType.None;
            gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }

    }
}

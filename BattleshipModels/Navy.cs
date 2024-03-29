﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipModels
{
    public enum Guess
    {
        Unknown,        // Unknown is first so it is the default value
        Hit,            // A ship has been hit
        Miss,           // A miss
        Ship,           // A ship segment that has not been hit
        DestroyedShip,  // A ship segment that is a part of a ship that has been destroyed
        Water,          // A space that is not a ship and has not been guessed
        PatrolBoat1H,   // The first position of a horizontal PatrolBoat
        PatrolBoat2H,
        PatrolBoat1V,   // The first position of a vertical PatrolBoat
        PatrolBoat2V,
        Submarine1H,
        Submarine2H,
        Submarine3H,
        Submarine1V,
        Submarine2V,
        Submarine3V,
        Destoyer1H,
        Destoyer2H,
        Destoyer3H,
        Destoyer1V,
        Destoyer2V,
        Destoyer3V,
        Battleship1H,
        Battleship2H,
        Battleship3H,
        Battleship4H,
        Battleship1V,
        Battleship2V,
        Battleship3V,
        Battleship4V,
        Carrier1H,
        Carrier2H,
        Carrier3H,
        Carrier4H,
        Carrier5H,
        Carrier1V,
        Carrier2V,
        Carrier3V,
        Carrier4V,
        Carrier5V
    }

    /// <summary>
    /// Contains all of the information 1 user should know
    /// </summary>
    public class Navy
    {
        public int OceanSize { get; set; }          // The size of the board
        public List<Ship> Ships { get; set; }       // The list of Ships in the navy
        public Guess[][][] Ocean { get; set; }        // Guess array of every value in the player's ocean
        public Guess[][][] EnemyOcean { get; set; }   // Guess array of every value in the player's enemy's ocean
        public bool DestroyedNavy { get; set; }     // True if all ships in the navy are destroyed
        public int Levels { get; set; }             // Number of Z-levels on the board

        /// <summary>
        /// Creates a list of 5 ships and creates an ocean of sizeXsize
        /// </summary>
        /// <param name="p_oceanSize">The size of the ocean</param>
        public Navy(int p_oceanSize)
        {
            Levels = 2;
            OceanSize = p_oceanSize;
            Ships = new List<Ship>()
            {
                new Ship(5),
                new Ship(4),
                new Ship(3),
                new Ship(3),
                new Ship(2)
            };
            Ocean = new Guess[OceanSize][][];
            for (int i = 0; i < OceanSize; i++)
            {
                Ocean[i] = new Guess[OceanSize][];
                for (int z = 0; z < OceanSize; z++)
                {
                    Ocean[i][z] = new Guess[Levels];
                }
            }
            for (int y = 0; y < OceanSize; y++)
            {
                for (int x = 0; x < OceanSize; x++)
                {
                    for (int z = 0; z < Levels; z++)
                    {
                        Ocean[x][y][z] = Guess.Water;
                    }
                }
            }
            EnemyOcean = new Guess[OceanSize][][];
            for (int i = 0; i < OceanSize; i++)
            {
                EnemyOcean[i] = new Guess[OceanSize][];
                for (int z = 0; z < OceanSize; z++)
                {
                    EnemyOcean[i][z] = new Guess[Levels];
                }
            }
            DestroyedNavy = false;
        }

        /// <summary>
        /// Checks to see if a ship can be placed on the board,
        /// and places the ship if it can fit on the board.
        /// </summary>
        /// <param name="p_ship">The Ship to be places</param>
        /// <param name="p_position">The position the head of the ship is being placed at</param>
        /// <param name="p_orientation">The orientation of the ship</param>
        /// <returns>True if the ship was placed on the board</returns>
        public bool PlaceShip(Ship p_ship, Position p_position, Orientation p_orientation)
        {
            if (CanShipFit(p_position, p_orientation, p_ship.Size))
            {
                p_ship.PositionHead = p_position;
                p_ship.Orientation = p_orientation;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if any ships are overlapping 
        /// </summary>
        /// <returns>True if no ships are overlapping</returns>
        public bool CanBeDeployed()
        {
            for (int i = 0; i < Ships.Count; i++)
            {
                foreach (Position position in Ships[i].Positions)
                {
                    for (int x = i + 1; x < Ships.Count; x++)
                    {
                        foreach (Position other in Ships[x].Positions)
                        {
                            if (position == other)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Finalizes the players board and deploys the ships.
        /// Checks to make sure no ships are overlapping and returns false if ships
        /// are overlapping.
        /// </summary>
        /// <returns>True if the ships were managed to be deployed</returns>
        public bool DeployShips()
        {
            foreach (Ship ship in Ships)
            {
                ship.DeployShip();
            }
            if (CanBeDeployed())
            {
                PlaceShipSprites();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PlaceShipSprites()
        {
            // Placing the Carrier
            Ship ship = Ships[0];
            if (ship.Orientation == Orientation.Horizontal)
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Carrier1H;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Carrier2H;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Carrier3H;
                Ocean[ship.Positions[3].XCoordinate][ship.Positions[3].YCoordinate][ship.Positions[3].ZCoordinate] = Guess.Carrier4H;
                Ocean[ship.Positions[4].XCoordinate][ship.Positions[4].YCoordinate][ship.Positions[4].ZCoordinate] = Guess.Carrier5H;
            }
            else
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Carrier1V;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Carrier2V;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Carrier3V;
                Ocean[ship.Positions[3].XCoordinate][ship.Positions[3].YCoordinate][ship.Positions[3].ZCoordinate] = Guess.Carrier4V;
                Ocean[ship.Positions[4].XCoordinate][ship.Positions[4].YCoordinate][ship.Positions[4].ZCoordinate] = Guess.Carrier5V;
            }

            // Placing the Battleship
            ship = Ships[1];
            if (ship.Orientation == Orientation.Horizontal)
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Battleship1H;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Battleship2H;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Battleship3H;
                Ocean[ship.Positions[3].XCoordinate][ship.Positions[3].YCoordinate][ship.Positions[3].ZCoordinate] = Guess.Battleship4H;
            }
            else
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Battleship1V;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Battleship2V;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Battleship3V;
                Ocean[ship.Positions[3].XCoordinate][ship.Positions[3].YCoordinate][ship.Positions[3].ZCoordinate] = Guess.Battleship4V;
            }

            // Placing the Submarine
            ship = Ships[2];
            if (ship.Orientation == Orientation.Horizontal)
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Submarine1H;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Submarine2H;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Submarine3H;
            }
            else
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Submarine1V;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Submarine2V;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Submarine3V;
            }

            // Placing the Destroyer
            ship = Ships[3];
            if (ship.Orientation == Orientation.Horizontal)
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Destoyer1H;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Destoyer2H;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Destoyer3H;
            }
            else
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.Destoyer1V;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.Destoyer2V;
                Ocean[ship.Positions[2].XCoordinate][ship.Positions[2].YCoordinate][ship.Positions[2].ZCoordinate] = Guess.Destoyer3V;
            }

            // Placing the PatrolBoat
            ship = Ships[4];
            if (ship.Orientation == Orientation.Horizontal)
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.PatrolBoat1H;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.PatrolBoat2H;
            }
            else
            {
                Ocean[ship.Positions[0].XCoordinate][ship.Positions[0].YCoordinate][ship.Positions[0].ZCoordinate] = Guess.PatrolBoat1V;
                Ocean[ship.Positions[1].XCoordinate][ship.Positions[1].YCoordinate][ship.Positions[1].ZCoordinate] = Guess.PatrolBoat2V;
            }
        }

        /// <summary>
        /// Determines whether an attack at a position was a Hit or a Miss
        /// </summary>
        /// <param name="position">The position of the attack</param>
        /// <returns>The Guess value of the square that was attacked</returns>
        public Guess IncomingAttack(Position position)
        {
            Guess attacked = Ocean[position.XCoordinate][position.YCoordinate][position.ZCoordinate];
            Guess returnValue = attacked;
            if (attacked == Guess.Water)
            {
                returnValue = Guess.Miss;
            }
            else if (attacked == Guess.Ship || attacked >= Guess.PatrolBoat1H)
            {
                if (ShipWasHit(position))
                {
                    returnValue = Guess.DestroyedShip;
                }
                else
                {
                    returnValue = Guess.Hit;
                }
            }
            Ocean[position.XCoordinate][position.YCoordinate][position.ZCoordinate] = returnValue;
            return returnValue;
        }

        /// <summary>
        /// Updates the Enemy board to reflect an attack
        /// </summary>
        /// <param name="p_position">The position of the attack</param>
        /// <param name="p_attack">The result of the attack</param>
        public void OutgoingAttack(Position p_position, Guess p_attack)
        {
            EnemyOcean[p_position.XCoordinate][p_position.YCoordinate][p_position.ZCoordinate] = p_attack;
        }

        /// <summary>
        /// Damages a ship that was hit at Position and checks to see if the ship was destroyed.
        /// Also changes the Guess values of the ship on the Ocean to Guess.DestroyedShip
        /// </summary>
        /// <param name="position">The position of the hit ship segment</param>
        /// <returns>True if the ship was destroyed</returns>
        public bool ShipWasHit(Position position)
        {
            bool shipStatus = false;
            foreach (Ship ship in Ships)
            {
                if (ship.HitShip(position))
                {
                    shipStatus = ship.ShipDestroyed();
                    if (shipStatus)
                    {
                        foreach (Position pos in ship.Positions)
                        {
                            Ocean[pos.XCoordinate][pos.YCoordinate][pos.ZCoordinate] = Guess.DestroyedShip;
                        }
                        NavyDestroyed();
                    }
                    return shipStatus;
                }
            }
            return shipStatus;
        }

        /// <summary>
        /// Determines whether every ship has been destroyed
        /// </summary>
        /// <returns>True if every ship in the navy has been destroyed</returns>
        public bool NavyDestroyed()
        {
            foreach (Ship ship in Ships)
            {
                if (!ship.Destroyed)
                {
                    return false;
                }
            }
            DestroyedNavy = true;
            return true;
        }

        /// <summary>
        /// Checks to see if a ship being placed can fit on the board
        /// </summary>
        /// <param name="p_startingPosition">The Position of the first segment of the ship</param>
        /// <param name="p_orientation">The Orientation of the ship</param>
        /// <param name="p_shipSize">The Size of the ship</param>
        /// <returns>True if the ship can fit on the board.</returns>
        public bool CanShipFit(Position p_startingPosition, Orientation p_orientation, int p_shipSize)
        {
            bool fit = false;
            if (p_startingPosition.ZCoordinate >= Levels || p_startingPosition.ZCoordinate < 0)
            {
                fit = false;
            }
            else if (p_startingPosition.XCoordinate >= OceanSize || p_startingPosition.YCoordinate >= OceanSize)
            {
                fit = false;
            }
            else if (p_orientation == Orientation.Horizontal && p_startingPosition.XCoordinate < OceanSize - p_shipSize)
            {
                fit = true;
            }
            else if (p_orientation == Orientation.Vertical && p_startingPosition.YCoordinate < OceanSize - p_shipSize)
            {
                fit = true;
            }
            return fit;
        }
    }
}

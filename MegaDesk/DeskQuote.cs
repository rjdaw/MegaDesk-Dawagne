﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk
{
    public enum Delivery
    {
        [Description("Rush 3 Days")]
        Rush3Days,

        [Description("Rush 5 Days")]
        Rush5Days,

        [Description("Rush 7 Days")]
        Rush7Days,

        [Description("No Rush")]
        Normal14Days
    }
    public class DeskQuote
    {
        //This is a two dimensional array (A row and a column)
        private int[,] _rushOrderPrices;

        //constant prices 
        private const decimal BASE_DESK_PRICE = 200.00M;
        private const decimal SURFACE_AREA_COST = 1.00M;
        private const decimal DRAWER_COST = 50.00M;
        private const decimal OAK_COST = 200.00M;
        private const decimal LAMINATE_COST = 100.00M;
        private const decimal PINE_COST = 50.00M;
        private const decimal ROSEWOOD_COST = 300.00M;
        private const decimal VENEER_COST = 125.00M;

        //Properties 
        public Desk Desk { get; set; }

        public string CustomerName { get; set; }


        public DateTime QuoteDate { get; set; }


        public Delivery DeliveryType { get; set; }

        public decimal QuotePrice { get; set; }

        public decimal GetQuotePrice() //This is a method that accessed all the information above
        {
            getRushOrderprices();

            //Returns the value of the quote price 
            decimal quotePrice = BASE_DESK_PRICE;


            //Returns the surface area 
            decimal surfaceArea = this.Desk.Depth * this.Desk.Width;

            //This gives the surface Price 
            decimal surfacePrice = 0.00M;

            if (surfaceArea > 1000)
            {
                surfacePrice = (surfaceArea - 1000) * SURFACE_AREA_COST;
            }

            //drawers price
            decimal drawerPrice = this.Desk.NumberOfDrawers * DRAWER_COST;

            //Cost for the surface Materials 
            decimal surfaceMaterialPrice = 0.00M;

            switch (this.Desk.SurfaceMaterial)
            {
                case DesktopMaterial.Laminate:
                    surfaceMaterialPrice = LAMINATE_COST;
                    break;

                case DesktopMaterial.Oak:
                    surfaceMaterialPrice = OAK_COST;
                    break;

                case DesktopMaterial.Pine:
                    surfaceMaterialPrice = PINE_COST;
                    break;

                case DesktopMaterial.Rosewood:
                    surfaceMaterialPrice = ROSEWOOD_COST;
                    break;

                case DesktopMaterial.Veneer:
                    surfaceMaterialPrice = VENEER_COST;
                    break;
            }

            //shipping cost using switch 
            decimal shippingPrice = 0.00M; 

            switch (this.DeliveryType)
            {
                case Delivery.Rush3Days:
                    if (surfaceArea < 1000)
                    {
                        shippingPrice = _rushOrderPrices[0, 0];
                    }
                    else if (surfaceArea <= 2000)
                    {
                        shippingPrice = _rushOrderPrices[0, 1];
                    }
                    else
                    {
                        shippingPrice = _rushOrderPrices[0, 2];
                    }
                    break;

                case Delivery.Rush5Days:
                    if (surfaceArea < 1000)
                    {
                        shippingPrice = _rushOrderPrices[0, 3];
                    }
                    else if (surfaceArea <2000)
                    {
                        shippingPrice = _rushOrderPrices[0, 4];
                    }
                    else
                    {
                        shippingPrice = _rushOrderPrices[0, 5];
                    }
                    break;

                case Delivery.Rush7Days:
                    if (surfaceArea < 1000)
                    {
                        shippingPrice = _rushOrderPrices[0, 6];
                    }
                    else if (surfaceArea <= 2000)
                    {
                        shippingPrice = _rushOrderPrices[0, 7];
                    }
                    else
                    {
                        shippingPrice = _rushOrderPrices[0, 8];
                    }
                    break; 

            }

            quotePrice = quotePrice + surfacePrice + drawerPrice + surfaceMaterialPrice + shippingPrice;

            return quotePrice;
        }


        private void getRushOrderprices()
            {

            _rushOrderPrices = new int[3, 3]; //This creates 3 columns and 3 rows 

            var pricesFile = @"rushOrderPrices.txt";

            try
            {
                string[] prices = File.ReadAllLines(pricesFile);
                int i = 0, j = 0;

                foreach (string price in prices) //This is the for loop to read through the files 
                {
                    _rushOrderPrices[i, j] = int.Parse(price); //This turns the number back into an integer

                    if (j == 2)
                    {
                        i++;
                        j = 0; //This makes the next line blank so it can read the new value

                    }
                    else
                    {
                        j++;
                    }
                }
            }
            catch (Exception)
            {
                throw; //This comes up if there is an error
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FurnitureManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public  FurnitureEntities sharedInstance
        {
            get
            {
                return new FurnitureEntities();
            }
        }
    }

    public static class Context
    {


        public static FurnitureEntities sharedInstance
        {
            get
            {
                return new FurnitureEntities();
            }
        }

    }
}

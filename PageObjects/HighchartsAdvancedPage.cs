﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TA_Lab.Additional;
using System.Collections;
using System.Globalization;

namespace TA_Lab.PageObjects
{
    class HighchartsAdvancedPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public HighchartsAdvancedPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [FindsBy(How = How.CssSelector, Using = "path[aria-label*='Google search']")]
        IList<IWebElement> GoogleSearchGraph;

        [FindsBy(How = How.CssSelector, Using = "path[aria-label*='Revenue']")]
        IList<IWebElement> RevenueGraph;

        [FindsBy(How = How.CssSelector, Using = "path[aria-label*='Highsoft employees']")]
        IList<IWebElement> EmployeesGraph;

        //reading column from csv file
        private string[] ReadCSV(string path, int k)
        {
            StreamReader reader = new StreamReader(File.OpenRead(path));
            List<string> res = new List<string>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    res.Add(values[k]);
                }
            }

            return res.ToArray();
        }
        
        //k stands for the number of required fields to separate to
        private string[][] GetValuesFromGraph(IList<IWebElement> graph, int k)
        {
            string[] separator = { ". ", ", " };
            string[][] res = new string[graph.Count][];
            int j = 0;
            for (int i = 0; i < graph.Count; i++)
            {
                res[i] = (graph[i].GetAttribute("aria-label").Split(separator, k, StringSplitOptions.RemoveEmptyEntries));

            }
            return res;
        }

        public bool CheckChartGreen()
        {
            int n = 4;
            string[] csv1 = ReadCSV(Helper.GetPathCSV("Green"), 0);
            string[] csv2 = ReadCSV(Helper.GetPathCSV("Green"), 1);
            string[][] values = GetValuesFromGraph(EmployeesGraph, n);

            for (int i = 0; i < values.Length; i++)
            {
                if ((values[i][2] == "joined") | (values[i][2] == "left"))
                {
                    string a = values[i][1] + ". " + values[i][2];
                    if (csv1[i + 1] != a)
                        return false;

                    string[] tokens = values[i][3].Split(' ');
                    if (csv2[i + 1] != tokens[0])
                        return false;
                }
                else
                {
                    if (csv1[i + 1] != values[i][1])
                        return false;

                    string[] tokens = values[i][2].Split(' ');
                    if (csv2[i + 1] != tokens[0])
                        return false;
                } 
            }

            return true;
        }
    }
}

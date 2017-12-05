using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1
{
    public class Project
    {
        //TODO Find out what this class has to do.
        //Currently I want it to be displayable in MainPage
        //Could make it more OOP by adding a labelmaker
        private string name;
        /// <summary>
        /// The name of the project
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        public static Project Empty = new Project("");
        private List<String> activityList;
        /// <summary>
        /// The list that contains all activities of this project as a name
        /// </summary>
        public List<string> ActivityList
        {
            get
            {
                return activityList;
            }
        }
        public Project(string name, List<string> names)
        {
            this.name = name;
            participants = names;
        }
        public Label MakeLabel()
        {
            Label output = new Label();
            output.Text = name;
            return output;
        }
        
    }
}

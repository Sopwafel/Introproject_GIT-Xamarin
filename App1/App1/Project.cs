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
        /// <summary>
        /// Creates a new project with specified name and no activities.
        /// </summary>
        /// <param name="name"></param>
        public Project(string name)
        {
            this.name = name;
            activityList = new List<String>();
        }
        /// <summary>
        /// Creates a new project with specified name and list of activities.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="activities"></param>
        public Project(string name, List<string> activities) : this(name)
        {
            this.activityList = activities;
        }
        /// <summary>
        /// Copies the data of this instance and returns it as a new Project
        /// </summary>
        /// <returns></returns>
        public Project Copy()
        {
            List<string> copy = new List<string>();
            ActivityList.ForEach((string s) => { copy.Add(s); });
            return new Project(this.Name, copy);
        }
        public override string ToString()
        {
            return Name;
        }
        public Label MakeLabel()
        {
            Label output = new Label();
            output.Text = name;
            return output;
        }
        
    }
}

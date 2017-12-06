//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace App1.database
//{
//    /// <summary>
//    /// A static class that can read and write to local Project and Activity storage.
//    /// This class is currently a stub.
//    /// </summary>
//    public static class ProjectReader
//    {
//        //these values are for testing, and should be removed when actual work on this class begins.
//        private static IList<Project> localProjects = new List<Project>(); // all local projects on this machine

//        private static IList<Project> localNewProjects = new List<Project>(); // only new projects on this machine, used to write to database
//        public static IList<Project> GetProjects()
//        {
//            return localProjects;
//        }

//        public static void AddProject(Project project)
//        {
//            localProjects.Add(project);
//            localNewProjects.Add(project);
//            return;
//        }

//        public static void AddActivity(Project project, String activity)
//        {
//            project.ActivityList.Add(activity);
//        }

//        public static Project ProjectFromName(String name)
//        {
//            foreach(Project p in localProjects)
//            {
//                if (name.Equals(p.Name))
//                {
//                    return p;
//                }
//            }
//            return null;
//        }

//        public static void ReadFromDatabase()
//        {
//            //TODO
//        }

//        public static void WriteToDatabase()
//        {
//            //TODO



//            localNewProjects.Clear();
//            return;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SRSDEMO.UI.WinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {

            //  Create CourseCatalog, ScheduleOfClasses, and Faculty objects.
            //  The order of creation is important because a ScheduleOfClasses
            //  needs a CourseCatalog object to properly initialize and a 
            //  Faculty object needs a ScheduleOfClasses object.

            //  Create a CourseCatalog object and read data from input files. 

            CourseCatalog catalog = new CourseCatalog("CourseCatalog.dat", "Prerequisites.dat");
            catalog.ReadCourseCatalogData();
            catalog.ReadPrerequisitesData();

            //  Create a ScheduleOfClasses object and read data from input file.

            ScheduleOfClasses schedule = new ScheduleOfClasses("SoC_SP2009.dat", "SP2009");
            schedule.ReadScheduleData(catalog);

            //  Create a Faculty object and read data from input files.

            Faculty faculty = new Faculty("Faculty.dat", "TeachingAssignments.dat");
            faculty.ReadFacultyData();
            faculty.ReadAssignmentData(schedule);

            // Create and display an instance of the main GUI window

            Application.Run(new MainForm(schedule));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSDEMO.UI
{
    class SRS
    {
       //课程列表
        //课程列表
        public static ScheduleOfClasses scheduleOfClasses =
                         new ScheduleOfClasses("SP2009");
        public static ScheduleOfClasses scheduleOfClasses1 =
                         new ScheduleOfClasses("SM2009");
        //教授、学生、课程列表
        public static List<Professor> faculty;    // Generic List of Professors
        public static List<Student> studentBody;  // Generic List of Students
        public static List<Course> courseCatalog; // Generic List of Courses
        static void Main(string[] args)
        {
            Professor p1, p2, p3;
            Student s1, s2, s3;
            Course c1, c2, c3, c4, c5;
            Section sec1, sec2, sec3, sec4, sec5, sec6, sec7;

            // 创建多个对象（可能调用不同的构造函数）
            // 通常从数据库或文件中读取数据加载对象到内存中

            // -----------
            // Professors.
            // -----------

            p1 = new Professor("Jacquie Barker", "123-45-6789",
                               "Adjunct Professor", "Information Technology");
            p2 = new Professor("John Carson", "567-81-2345",
                               "Full Professor", "Chemistry");
            p3 = new Professor("Jackie Chan", "987-65-4321",
                               "Full Professor", "Physical Education");

            // Add these to the appropriate List.

            faculty = new List<Professor>();//列表需要先实例化再使用
            faculty.Add(p1);
            faculty.Add(p2);
            faculty.Add(p3);

            // ---------
            // Students.
            // ---------

            s1 = new Student("Zachary Palmer", "111-11-1111", "Math", "M.S.");

            s2 = new Student("Gerson Lopez", "222-22-2222",
                             "Information Technology", "Ph. D.");

            s3 = new Student("Mannat Durgapal", "333-33-3333", "Physics", "B.S.");

            // Add these to the appropriate List.

            studentBody = new List<Student>();
            studentBody.Add(s1);
            studentBody.Add(s2);
            studentBody.Add(s3);

            // --------
            // Courses.
            // --------

            c1 = new Course("CMP101", "Beginning Computer Technology", 3.0);
            c2 = new Course("OBJ101", "Object Methods for Software Development", 3.0);
            c3 = new Course("CMP283", "Higher Level Languages (C#)", 3.0);
            c4 = new Course("CMP999", "Living Brain Computers", 3.0);
            c5 = new Course("ART101", "Beginning Basketweaving", 3.0);

            // Add these to the appropriate List.

            courseCatalog = new List<Course>();
            courseCatalog.Add(c1);
            courseCatalog.Add(c2);
            courseCatalog.Add(c3);
            courseCatalog.Add(c4);
            courseCatalog.Add(c5);

            // 建立课程间的先修关系 (c1 => c2 => c3 => c4).

            c2.AddPrerequisite(c1);
            c3.AddPrerequisite(c2);
            c4.AddPrerequisite(c3);

            // ---------
            // Sections.
            // ---------

            // 通过调用Course对象的ScheduleSection方法生成一个Section对象，相当于给某门课建立一个排课
            //Schedule sections of each Course by calling the
            // scheduleSection method of Course (which internally
            // invokes the Section constructor). 

            sec1 = c1.ScheduleSection("M", "8:10 - 10:00 PM", "GOVT101", 30);
            sec2 = c1.ScheduleSection("W", "6:10 - 8:00 PM", "GOVT202", 30);
            sec3 = c2.ScheduleSection("Th", "4:10 - 6:00 PM", "GOVT105", 25);
            sec4 = c2.ScheduleSection("Tu", "6:10 - 8:00 PM", "SCI330", 25);
            sec5 = c3.ScheduleSection("M", "6:10 - 8:00 PM", "GOVT101", 20);
            sec6 = c4.ScheduleSection("Th", "4:10 - 6:00 PM", "SCI241", 15);
            sec7 = c5.ScheduleSection("F", "4:10 - 6:00 PM", "ARTS25", 40);

            //将Section加入到选课列表中
            // Add these to the Schedule of Classes.

            scheduleOfClasses.AddSection(sec1);
            scheduleOfClasses.AddSection(sec2);
            scheduleOfClasses.AddSection(sec3);
            scheduleOfClasses.AddSection(sec4);
            scheduleOfClasses.AddSection(sec5);
            scheduleOfClasses.AddSection(sec6);
            scheduleOfClasses.AddSection(sec7);

            //设定每门课的教师
            // Recruit a professor to teach each of the sections.

            p3.AgreeToTeach(sec1);
            p2.AgreeToTeach(sec2);
            p1.AgreeToTeach(sec3);
            p3.AgreeToTeach(sec4);
            p1.AgreeToTeach(sec5);
            p2.AgreeToTeach(sec6);
            p3.AgreeToTeach(sec7);

            Console.WriteLine("Student registration has begun!");
            Console.WriteLine("");

            //模拟学生选课
            // Students drop/add courses.

            Console.WriteLine("Student " + s1.Name +
                              " is attempting to enroll in " +
                              sec1.ToString());

            EnrollFlags status = sec1.Enroll(s1);

            // Note the use of a special method to interpret
            // and display the outcome of this enrollment request.
            // (We could have included the code in-line here, but
            // since (a) it is rather complex and (b) it will need
            // to be repeated for all subsequent enrollment requests
            // below, it made sense to turn it into a reusable method
            // instead.)

            //这个方法显示这个学生本次选课是否成功
            ReportStatus(status);

            //让该学生（s1）再次选这门课程（已选过），观察选课结果（选课应该不能成功）
            // Try enrolling the same Student in a different Section
            // of the SAME Course!

            Console.WriteLine("Student " + s1.Name +
                              " is attempting to enroll in " +
                              sec2.ToString());  //sec1和sec2是同一Course的两个Section
            status = sec2.Enroll(s1);
            ReportStatus(status);

            Console.WriteLine("Student " + s2.Name +
                              " is attempting to enroll in " +
                              sec2.ToString());
            status = sec2.Enroll(s2);
            ReportStatus(status);

            Console.WriteLine("Student " + s2.Name +
                              " is attempting to enroll in " +
                              sec3.ToString());
            status = sec3.Enroll(s2);
            ReportStatus(status);

            Console.WriteLine("Student " + s2.Name +
                           " is attempting to enroll in " +
                           sec7.ToString());
            status = sec7.Enroll(s2);
            ReportStatus(status);

            Console.WriteLine("Student " + s3.Name +
                           " is attempting to enroll in " +
                           sec1.ToString());
            status = sec1.Enroll(s3);
            ReportStatus(status);

            Console.WriteLine("Student " + s3.Name +
                           " is attempting to enroll in " +
                           sec5.ToString());
            status = sec5.Enroll(s3);
            ReportStatus(status);

            // Skip a line.
            Console.WriteLine("");

            // When the dust settles, here's what folks wound up
            // being registered for:
            //
            // sec1:  s1, s3
            // sec2:  s2
            // sec7:  s2


            //此处模拟学期结束教授给学生打分
            // Semester is finished (boy, that was quick!).  Professors
            // assign grades.

            sec1.PostGrade(s1, "C+");
            sec1.PostGrade(s3, "A");
            sec2.PostGrade(s2, "B+");
            sec7.PostGrade(s2, "A-");

            
            //使用display方法显示各个对象最终状态
            // Let's see if everything got set up properly
            // by calling various display methods!

            Console.WriteLine("====================");
            Console.WriteLine("Schedule of Classes:");
            Console.WriteLine("====================");
            Console.WriteLine("");
            scheduleOfClasses.Display();

            Console.WriteLine("======================");
            Console.WriteLine("Professor Information:");
            Console.WriteLine("======================");
            Console.WriteLine("");
            p1.Display();
            Console.WriteLine("");
            p2.Display();
            Console.WriteLine("");
            p3.Display();
            Console.WriteLine("");

            Console.WriteLine("====================");
            Console.WriteLine("Student Information:");
            Console.WriteLine("====================");
            Console.WriteLine("");
            s1.Display();
            Console.WriteLine("");
            s2.Display();
            Console.WriteLine("");
            s3.Display();

            //练习14.2
            Section sec2_1, sec2_2, sec2_3, sec2_4, sec2_5;
            sec2_1 = c1.ScheduleSection("M", "8:10 - 10:00 PM", "GOVT101", 30);
            sec2_1 = c1.ScheduleSection("W", "6:10 - 8:00 PM", "GOVT202", 30);
            scheduleOfClasses1.AddSection(sec1);
            scheduleOfClasses1.AddSection(sec2);
            //题2：让s1选sec2
            EnrollFlags result = sec2_1.Enroll(s1);


            //题3：


            //题4：


            //题4：
            Console.ReadKey();
        }

        public static void ReportStatus(EnrollFlags status)
        {
            if (status == EnrollFlags.SUCCESSFULLY_ENROLLED)
            {
                Console.WriteLine("outcome:  SUCCESSFULLY_ENROLLED");
            }
            else if (status == EnrollFlags.PREREQ_NOT_SATISFIED)
            {
                Console.WriteLine("outcome:  PREREQ_NOT_SATISFIED");
            }
            else if (status == EnrollFlags.PREVIOUSLY_ENROLLED)
            {
                Console.WriteLine("outcome:  PREVIOUSLY_ENROLLED");
            }
            else if (status == EnrollFlags.SECTION_FULL)
            {
                Console.WriteLine("outcome:  SECTION_FULL");
            }
        }
    }
}

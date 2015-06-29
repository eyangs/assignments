// Course.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker  and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Course
{

    //----------------
    // Constructor(s).
    //----------------

    public Course(string cNo, string cName, double credits)
    {

        //  Initialize property values.

        CourseNumber = cNo;
        CourseName = cName;
        Credits = credits;

        // Create two empty Lists.

        OfferedAsSection = new List<Section>();
        Prerequisites = new List<Course>();
    }

    //-------------------------------
    // Auto-implemented properties.
    //-------------------------------

    public string CourseNumber { get; set; }
    public string CourseName { get; set; }
    public double Credits { get; set; }
    public List<Section> OfferedAsSection { get; set; }
    public List<Course> Prerequisites { get; set; }
    public static int SectionNumber { get; set; }

    //-----------------------------
    // Miscellaneous other methods.
    //-----------------------------

    // Used for testing purposes.

    public void Display()
    {
        Console.WriteLine("Course Information:");
        Console.WriteLine("\tCourse No.:  " + CourseNumber);
        Console.WriteLine("\tCourse Name:  " + CourseName);
        Console.WriteLine("\tCredits:  " + Credits);
        Console.WriteLine("\tPrerequisite Courses:");

        // We use a foreach statement to step through the prerequisites.

        foreach (Course c in Prerequisites)
        {
            Console.WriteLine("\t\t" + c.ToString());
        }

        // Note use of Write vs. WriteLine in next line of code.

        Console.Write("\tOffered As Section(s):  ");
        for (int i = 0; i < OfferedAsSection.Count; i++)
        {
            Section s = (Section)OfferedAsSection[i];
            Console.Write(s.SectionNumber + " ");
        }

        // Print a blank line.
        Console.WriteLine("");
    }

    //**************************************
    //
    public override string ToString()
    {
        return CourseNumber + ":  " + CourseName;
    }

    //**************************************
    //
    public void AddPrerequisite(Course c)
    {
        //确保一门课程不能把增加设为先修课程
        if (c.CourseNumber != this.CourseNumber)
        {
            Prerequisites.Add(c);
        }
    }

    //**************************************
    //
    public bool HasPrerequisites()
    {
        if (Prerequisites.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //******************************************************************
    //
    //练习四  在course类定义静态变量SectionNumber，改正逻辑错误
    public Section ScheduleSection(string day, string time, string room,
                       int capacity)
    {
        // Create a new Section (note the creative way in
        // which we are assigning a section number) ...
        Section s = new Section(SectionNumber + 1,
                    day, time, this, room, capacity);

        // ... and then add it to the List
        OfferedAsSection.Add(s);

        return s;
    }
    //练习四 增加一个取消section方法 
    public bool CancelSection(Section s)
    {
        this.OfferedAsSection.Remove(s);
        return true;
    }
}

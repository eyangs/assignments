// Course.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker  and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Course {

  //----------------
  // Constructor(s).
  //----------------

  public Course(string cNo, string cName, double credits) {

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
  //练习4 定义静态变量SectionNumber
  public static int SectionNumber { get; set; }
  public string CourseNumber { get; set; }
  public string CourseName { get; set; }
  public double Credits { get; set; }
  public List<Section> OfferedAsSection { get; set; }
  public List<Course> Prerequisites { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public void Display() {
    Console.WriteLine("Course Information:");
    Console.WriteLine("\tCourse No.:  "+CourseNumber);
    Console.WriteLine("\tCourse Name:  "+CourseName);
    Console.WriteLine("\tCredits:  "+Credits);
    Console.WriteLine("\tPrerequisite Courses:");

    // We use a foreach statement to step through the prerequisites.

    foreach ( Course c in Prerequisites ) {
      Console.WriteLine("\t\t" + c.ToString());
    }

    // Note use of Write vs. WriteLine in next line of code.

    Console.Write("\tOffered As Section(s):  ");
    for(int i=0; i<OfferedAsSection.Count; i++) {
      Section s = (Section)OfferedAsSection[i];
      Console.Write(s.SectionNumber + " ");
    }

    // Print a blank line.
    Console.WriteLine("");
  }

  //**************************************
  //	
  public override string ToString() {
    return CourseNumber + ":  "+CourseName;
  }

  //**************************************
  //练习2 增加判断语句 当c1课程号与当前课程号不相等时才能执行增加先修课，确保c1不能把自己设为先修课	
  public void AddPrerequisite(Course c1) 
  {
      if (c1.CourseNumber != this.CourseNumber)
 
    Prerequisites.Add(c1);
  }

  //**************************************
  //	
  public bool HasPrerequisites() {
    if ( Prerequisites.Count > 0 ) {
      return true;
    }  
    else {
      return false;
    }
  }

  //******************************************************************************
  // //在course类定义静态变量SectionNumber，改正逻辑错误
  public Section ScheduleSection(int secNumber, string day, string time, string room,
				       int capacity) 
  {
    // Create a new Section  ...
    Section s = new Section(secNumber, day, time, this, room, capacity);
		
    // ... and then remember it!
    OfferedAsSection.Add(s);
		
    return s;
  }
      //练习4 增加一个取消section方法
  public bool CancelSection(Section s)  
  {  
     this.OfferedAsSection.Remove(s);  
     return true;  
  
  }  
  }
}

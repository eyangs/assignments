// Professor.cs - Chapter 16 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Professor : Person 
{

  //----------------
  // Constructor(s).
  //----------------

  // Reuse the parent constructor with the base keyword,
  // then update auto-implemented property values and create
  // an empty List.

  public Professor(string name, string id, 
                   string title, string dept) : base(name, id) {

    Title = title;
    Department = dept;

    // Create an empty List.
    Teaches = new List<Section>();
  }
		
  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string Title { get; set; }
  public string Department { get; set; }
  public List<Section> Teaches { get; set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  //**************************************
  //	
  public override void Display() {
    // First, let's call the Person Display method.

    base.Display();
		
    // Then, display Professor-specific info.

    Console.WriteLine("Professor-Specific Information:");
    Console.WriteLine("\tTitle:  " + Title);
    Console.WriteLine("\tTeaches for Dept.:  " + Department);
    DisplayTeachingAssignments();
  }
	
  //***************************************************************
  // We are forced to program this method because it is specified
  // as an abstract method in our parent class (Person); failing to
  // do so would render the Professor class abstract, as well.
  //
  // For a Professor, we wish to return a String as follows:
  //
  // 	Josephine Blow (Associate Professor, Math)
  //
  public override string ToString() {
    return Name + " (" + Title + ", " +
		       Department + ")";
  }

  //**************************************
  //	
  public void DisplayTeachingAssignments() {
    Console.WriteLine("Teaching Assignments for " + Name + ":");
		
    // We'll step through the Teaches List, processing
    // Section objects one at a time.

    if (Teaches.Count == 0) {
      Console.WriteLine("\t(none)");
    } 
    else {
      for (int i=0; i<Teaches.Count; i++) {
        // Because we are going to need to delegate a lot of the effort
        // of satisfying this request to the various Section objects that
        // comprise the Professor's teaching load, we "pull" the Section 
        // object once, and refer to it by a temporary object reference.

        Section s = Teaches[i];

        // Note how we call upon the Section object to do
        // a lot of the work for us!

        Console.WriteLine("\tCourse No.:  "+
                           s.RepresentedCourse.CourseNumber);
        Console.WriteLine("\tSection No.:  "+s.SectionNumber);
        Console.WriteLine("\tCourse Name:  "+
                           s.RepresentedCourse.CourseName);
        Console.WriteLine("\tDay and Time:  "+
                           s.DayOfWeek+" - "+s.TimeOfDay);
			Console.WriteLine("\t-----");
      }
    }
  }
	
  //14.5练习3 若没有section，则添加，若已有，添加一个判断，
  //遍历Teaches已有的section,只有没有的Teaches才能添加	
  public void AgreeToTeach(Section s)
  {
      //确保一个教授不能在同一天/同一时刻教第二门课
      Boolean l = false;
      if (Teaches.Count == 0)
      {
          Teaches.Add(s);
      }
      else
      {
          for (int i = 0; i < Teaches.Count; i++)
          {
              if (Teaches[i].TimeOfDay == s.TimeOfDay && Teaches[i].DayOfWeek == s.DayOfWeek)
                  l = true;
              break;
          }
          if (l == false)
              Teaches.Add(s);
          // We need to link this bidirectionally.
          s.Instructor = this;
      }
  }
}

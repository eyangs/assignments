// Professor.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Professor : Person {

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
  public List<Student> AddviseStus { get; set; }


  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public override void Display() {
    // First, let's call the Person Display method.

    base.Display();
		
    // Then, display Professor-specific info.

    Console.WriteLine("Professor-Specific Information:");
    Console.WriteLine("\tTitle:  " + Title);
    Console.WriteLine("\tTeaches for Dept.:  " + Department);
    DisplayTeachingAssignments();
  }
	
  // We are forced to program this method because it is specified
  // as an abstract method in our parent class (Person); failing to
  // do so would render the Professor class abstract, as well.
  //
  // For a Professor, we wish to return a String as follows:
  //
  // 	Josephine Blow (Associate Professor, Math)

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
	
  //**************************************
  //第三题，确保教授不能在同一天同一时间教授两门课
  public void AgreeToTeach(Section s) {
      bool access = true;      
      //循环检查此课程和将添加课程时间是否相同
      for (int i = 0; i < Teaches.Count; i++)
      {
          //如果时间相同
          if (string.Equals(s.TimeOfDay , Teaches[i].TimeOfDay)) 
          {
              //则不可选
              access = false;
              Console.WriteLine(s + "和" + Teaches[i] +"的时间相冲突");
              Console.WriteLine("Tips："+ this.Name + "不能在同一时间教授两门课程！！！");
          }
          break;
      }
      //循环结束，若access值仍为false，则表示没有时间冲突，可以添加
      if(access)
      {
          Teaches.Add(s);
          // We need to link this bidirectionally.
          s.Instructor = this;

      }     


    //Teaches.Add(s);

    // We need to link this bidirectionally.
    //s.Instructor = this;
  }
}

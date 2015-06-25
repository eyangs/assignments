// Student.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;
using System.Collections.Generic;

public class Student : Person {

  //------------
  // Field.
  //------------

  private List<Section> attends; // of Sections
	
  //----------------
  // Constructor(s).
  //----------------

  // Reuse the code of the parent's constructor using the base.
  // keyword. 

  public Student(string name, string id, 
                 string major, string degree) : base(name, id) {

    Major = major;
    Degree = degree;
    Transcript = new Transcript(this);

    // We create an empty List.

    attends = new List<Section>();
  }
	
  // A second form of constructor, used when a Student has not yet
  // declared a major or degree.
  // Reuse the code of the other Student constructor.

  public Student(string name, string id) : this(name, id, "TBD", "TBD"){
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string Major { get; protected set; }
  public string Degree { get; protected set; }
  public Transcript Transcript { get; protected set; }

  //-----------------------------
  // Standard property for List.
  //-----------------------------

  public List<Section> Attends {
    get {
      return attends;
    }
    set {
      attends = value;
    }
  }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // Used for testing purposes.

  public override void Display() {
    // First, let's display Person class data elements.

    base.Display();
		
    // Then, display Student-specific info.

    Console.WriteLine("Student-Specific Information:");
    Console.WriteLine("\tMajor:  "+Major);
    Console.WriteLine("\tDegree:  "+Degree);
    DisplayCourseSchedule();
    Transcript.Display();
  }	
	
  // We are forced to program this method because it is specified
  // as an abstract method in our parent class (Person); failing to
  // do so would render the Student class abstract, as well.
  //
  // For a Student, we wish to return a String as follows:
  //
  // 	Jackson Palmer (123-45-6789) [Master of Science - Math]
  //
  public override string ToString() {
    return Name + " (" + Id + ") [" + Degree +" - " + Major + "]";
  }

  //**************************************
  //
  public void DisplayCourseSchedule() {
    // Display a title first.

    Console.WriteLine("Course Schedule for " + this.Name);
		
    // Step through the List of Section objects, 
    // processing these one by one.

    foreach ( Section s in attends ) {

      // Since the attends List contains Sections that the
      // Student took in the past as well as those for which
      // the Student is currently enrolled, we only want to
      // report on those for which a grade has not yet been
      // assigned.
            
      if ( s.GetGrade(this) == null ) {
        Console.WriteLine("\tCourse No.:  "+ 
                           s.RepresentedCourse.CourseNumber);
        Console.WriteLine("\tSection No.:  "+s.SectionNumber);
        Console.WriteLine("\tCourse Name:  "+ 
                           s.RepresentedCourse.CourseName);
        Console.WriteLine("\tMeeting Day and Time Held:  "+
                          s.DayOfWeek + " - "+
                          s.TimeOfDay);
        Console.WriteLine("\tRoom Location:  "+s.Room);
        Console.WriteLine("\tProfessor's Name:  "+
                           s.Instructor.Name);
        Console.WriteLine("\t-----");
      }
    }
  }
	
  //**************************************
  //
  public void AddSection(Section s) {
    attends.Add(s);
  }
	
  public void DropSection(Section s) {
    attends.Remove(s);
  }
	
  //************************************************************
  // Determine whether the Student is already enrolled in THIS
  // EXACT Section.
  //
  public bool IsEnrolledIn(Section s) {
    return attends.Contains(s);
  }
	
  //*******************************	
  public void PrintTranscript() {
    Transcript.Display();
  }

  //****************************************************************
  // Determine whether the Student is already enrolled in ANOTHER
  // Section of this SAME Course.
  //
  public bool IsCurrentlyEnrolledInSimilar(Section s1) {
    bool foundMatch = false;
    Course c1 = s1.RepresentedCourse;

    foreach( Section s2 in attends ) {
      Course c2 = s2.RepresentedCourse;
      if (c1 == c2) {
        // There is indeed a Section in the attends
        // List representing the same Course.
        // Check to see if the Student is CURRENTLY
        // ENROLLED (i.e., whether or not he has
        // yet received a grade).  If there is no
        // grade, he/she is currently enrolled; if
        // there is a grade, then he/she completed
        // the course some time in the past.
        if ( s2.GetGrade(this) == null ) {
          // No grade was assigned!  This means
          // that the Student is currently
          // enrolled in a Section of this
          // same Course.
          foundMatch = true;
          break;
        }
      }
    }

    return foundMatch;
  }
		
}

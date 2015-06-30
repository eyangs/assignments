// Course.cs - Chapter 14 version.

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
  //
  public void AddPrerequisite(Course c) {
      //第二题，改进先修课程方法，防止把自己设为先修
     //判断课程名是否相等
      if (string.Equals(c.CourseName, this.CourseName))
      {
          //课程名相同 即为同一门课程 不能添加
          Console.WriteLine("不能将自身设为先修课程");          
      }
      else 
      {
          //否则可以添加
          Prerequisites.Add(c);
      }

    //Prerequisites.Add(c);
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

  //******************************************************************
  //第四题 更正ScheduleOfSection方法的错误逻辑，使之不出现重复的课程编号
  //增加一个静态字段表示选课编号
  public static int NOOfSection=1;
  public Section ScheduleSection(string day, string time, string room,
				       int capacity) {
    // Create a new Section (note the creative way in
    // which we are assigning a section number) ...

      //新的选课如下表示：
       Section s = new Section(NOOfSection++, 
                day, time, this, room, capacity);

    //Section s = new Section(OfferedAsSection.Count + 1, 
    //            day, time, this, room, capacity);
		
    // ... and then add it to the List
    OfferedAsSection.Add(s);
		
    return s;
  }
    //第四题为Course实现一个CancelSection方法
    //更正ScheduleOfSection方法的错误逻辑，使之不出现重复的课程编号
  public void  CancelSection(Section sec)
  {
      Console.WriteLine("删除前的结果为：");
      Console.WriteLine("------------------------");
      for (int i = 0; i < OfferedAsSection.Count; i++)
      {
          Console.WriteLine(OfferedAsSection[i]);
      }
      Console.WriteLine("-----------------------");
      //移除选课s 
      //定义索引
      int suoyin = 255;
      for (int i = 0; i < OfferedAsSection.Count;i++ )
      {
          if (OfferedAsSection[i]==sec)
          {
              //根据选课取得索引
              suoyin = i;
              break;
          }
      }
      if (suoyin != 255)
      {
          OfferedAsSection.RemoveAt(suoyin);          
          Console.WriteLine(sec +"这门课程已经移除！");
      }
      else 
      {
          Console.WriteLine("已经不存在" + sec + "这门课程！");
      }
      Console.WriteLine("删除后的结果为：");
      Console.WriteLine("------------------------");
      for (int i = 0; i < OfferedAsSection.Count; i++)
      {
          Console.WriteLine(OfferedAsSection[i]);
      }
      Console.WriteLine( "-----------------------");

 
      

      //bool successed = OfferedAsSection.Remove(sec);
      //Console.WriteLine(successed );
      //Console.WriteLine(sec +"这门课程已经移除！");
      
  }



}

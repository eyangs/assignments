// Person.cs - Chapter 14 version.

// Copyright 2008 by Jacquie Barker and Grant Palmer - all rights reserved.

// A MODEL class.

using System;

// We are making this class abstract because we do not wish for it
// to be instantiated.

public abstract class Person {

  //----------------
  // Constructor(s).
  //----------------

  // Initialize the auto-implemented property values using the set 
  // accessor.

  public Person(string name, string id) {
    Name = name;
    Id = id;
  }
	
  // We're replacing the default parameterless constructor that got 
  // "wiped out"as a result of having created a constructor above.
  // We reuse the two-argument constructor with dummy values.

  public Person() : this("?", "???-??-????")  {
  }

  //-------------------------------
  // Auto-implemented properties.
  //-------------------------------

  public string Name { get; protected set; }
  public string Id { get; protected set; }

  //-----------------------------
  // Miscellaneous other methods.
  //-----------------------------

  // We'll let each subclass determine how it wishes to be
  // represented as a String value.

  public abstract override string ToString(); 

  // Used for testing purposes.

  public virtual void Display() {
    Console.WriteLine("Person Information:");
    Console.WriteLine("\tName:  " + this.Name);
    Console.WriteLine("\tID number:  " + this.Id);
  }
}	

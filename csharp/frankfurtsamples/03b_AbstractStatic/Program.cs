Test<FooTest> t1 = new();
t1.TestMethod();

FooTest2.Foo();

Test2<FooTest2> t2 = new();
t2.TestMethod();

public interface IFoo
{
    abstract static void Foo();
}

// IFoo declares an abstract static method with the interface,
// so it must be implemented in the class that implements the interface.
public class FooTest : IFoo
{
    public static void Foo()
    {
        Console.WriteLine("FooTest.Foo");
    }
}

// The generic class can use the static interface method with the implementation
public class Test<T>
    where T : IFoo
{
    public void TestMethod()
    {
        T.Foo();
    }
}

public class Test2<T>
    where T : IFoo2
{
    // Foo2 is a method implemented with the interface. If it's not implemented with the concrete class,
    // the implementation of the interface will be used.
    public void TestMethod()
    {
        T.Foo2();
    }
}

// Static methods can be implemented in interfaces!
// To allow changing the implementation with concrete types, the method must be declared as virtual.
public interface IFoo2 : IFoo
{
    static virtual void Foo2()
    {
        Console.WriteLine("IFoo2.Foo2");
    }
}

public class FooTest2 : IFoo2
{
    public static void Foo()
    {
        Console.WriteLine("FooTest2.Foo");
    }

    public static void Foo2()
    {
        Console.WriteLine("FooTest2.Foo2");
    }
}
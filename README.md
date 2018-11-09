# AutoName.xUnit

| master                                                                                                                                                 | feature                                                                                                                                                        |
| ------------------------------------------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [![pipeline status](https://gitlab.com/gudthing/autoname.xunit/badges/master/pipeline.svg)](https://gitlab.com/gudthing/autoname.xunit/commits/master) | [![pipeline status](https://gitlab.com/gudthing/autoname.xunit/badges/feature/ci/pipeline.svg)](https://gitlab.com/gudthing/autoname.xunit/commits/feature/ci) |
 

## Usage
There are **three ways** to use this package:

1. **Use defaults**
2. **Customise inline default behaviour**:
    3. Name - The name for the test
    4. Splitter - The method to use to split your unit test method name
    5. Joiner - The method to use to join your splitter
6. **Custom Implementation**

---
### 1. Use Defaults 

```c#
// Output:  `The Name Of Your Test Method`
[NamedFact]
public void TheNameOfYourTestMethod() 
{}

// Output:  `The Name Of Another Test Method`
[NamedTheory]
[InlineData(...)]
public void TheNameOfAnotherTestMethod(...) 
{}

```
Default rules are: 
* Split by uppercase
* Join with single space

---
### 2. Customise Inline Default Behaviour
Customising inline data is useful for when you have a small number of tests that use a different naming stategy. If the defaults do not fit the majority of your scheme, then you should look at custom implementation.

```c#
// Output: `The Name Of Your Test Method`
[NamedFact(SplitBy.Underscore, JoinWith.SingleSpace)] 
public void The_Name_Of_Your_Test_Method()
{}

// Output: `The Name Of Your Test Method`
[NamedFact(NameIt.MethodName, SplitBy.Underscore, JoinWith.SingleSpace)] 
public void The_Name_Of_Your_Test_Method()
{}

// Output: `The Name Of Another Test Method`
[NamedTheory(SplitBy.Underscore, JoinWith.SingleSpace)]
[InlineData(...)]
public void The_Name_Of_Another_Test_Method(...)
{}

// Output: `The Name Of Another Test Method`
[NamedTheory(NameIt.MethodName, SplitBy.Underscore, JoinWith.SingleSpace)]
[InlineData(...)]
public void The_Name_Of_Another_Test_Method(...)
{}

```
`NameIt` and `SplitBy` are flags, enabling you to also handle mixed naming: 
```c#
// Output: `The Name Of Your Test Method`
[NamedFact(SplitBy.Underscore | SplitBy.Uppercase, JoinWith.SingleSpace)] 
public void TheName_OfYour_TestMethod()
{}

// Output: `The Name Of Another Test Method`
[NamedTheory(SplitBy.Underscore | SplitBy.Uppercase, JoinWith.SingleSpace)]
[InlineData(...)]
public void The_Name_Of_Another_Test_Method(...)
{}
```

---
### 3. Custom Implementation

Configure the default `NameIt`, `SplitBy` and `JoinWith` enums. To create a custom attribute that will handle mixed uppercase and underscore naming:
```c#
    public class MyCustomAttribute : NamedFactAttribute
    {
		public MyCustomAttribute([CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
		: base(NameIt.MethodName, SplitBy.Underscore | SplitBy.Uppercase, JoinWith.SingleSpace, callerName, sourceFilePath)
		{ }
    }
```
Usage: 
```c#
// Output: This Is My Preferred Naming Style
[MyCustomAttribute]
public void ThisIsMy_Preferred_NamingStyle()
{}
```

Most common cases should be handled by the built in behaviour. However, should you need a more bespoke naming convention, then you can override the `SetName` method like shown below:
```c#
public class MyCustomAttribute : NamedFactAttribute
{
	public MyCustomAttribute([CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
	: base(NameIt.MethodName, SplitBy.Underscore | SplitBy.Uppercase, JoinWith.SingleSpace, callerName, sourceFilePath)
	{ }

    
    public override void SetDisplayName()
    {
        // Customise the base name
        NameIt = NameIt.MethodName | NameIt.NameSpace;

        // Customise how you wish to split the name 
        Func<string, IEnumerable<string>> mySplitter = delegate(string name)
        {
            return name.Split("__");
        }; 

        // Supports multiple splitters
        var mySplitters = new [] { mySplitter };

        // Customise how you wish to join the name
        Func<IEnumerable<string>, string> myJoiner = delegate(IEnumerable<string> names)
        {
            return string.Join("#", names);
        };

		base.DisplayName = ResolveName(mySplitters, myJoiner);
    }
}
```
Usage:
```c#
// Output: My.Special.NameSpace#ThisIsMy#Preferred#NamingStyle
[MyCustomAttribute]
public void __ThisIsMy__Preferred__NamingStyle()
{}
```
If you want to access existing properties you could also do:
```c#
public override void SetDisplayName()
{
    var splitters = GetSplitters();
    var joiner = GetJoiner();
    var splitterMethods = LoadSplitters(splitters);
    var joinerMethod = LoadJoiner(joiner);
    base.DisplayName = ResolveName(splitterMethods, joinerMethod);
}
```

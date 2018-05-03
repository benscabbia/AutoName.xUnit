# AutoName.xUnit

## Usage
There are three ways to use this package:

1. Use defaults
2. Customise inline default behaviour:
    3. Name - The name for the test
    4. Splitter - The method to use to split your unit test method name
    5. Joiner - The method to use to join your splitter
6. Custom Implementation


### 1. Use Defaults 

```c#
// Output:  `The Name Of Your Test Method`
[NamedFact]
public void TheNameOfYourTestMethod() 
{}

// Output:  `The Name Of Your Test Method2`
[NamedTheory]
[InlineData(...)]
public void TheNameOfYourTestMethod2(...) 
{}

```
**Output:**

### 2. Customise Inline Default Behaviour
Customising inline data is useful for when you have a small number of tests that use a different naming stategy. If the defaults do not fit the majority of your scheme, then you should look at custom implementation.

```c#
// Output: `The Name Of Your Test Method`
[NamedFact(SplitBy.Underscore, JoinWith.Space)] 
public void The_Name_Of_Your_Test_Method()
{}

// Output: `The Name Of Your Test Method`
[NamedFact(NameIt.MethodName, SplitBy.Underscore, JoinWith.Space)] 
public void The_Name_Of_Your_Test_Method()
{}

// Output: `The Name Of Your Test Method`
[NamedTheory(SplitBy.Underscore, JoinWith.Space)]
[InlineData(...)]
public void TheNameOfYourTestMethod2(...)
{}

// Output: `The Name Of Your Test Method`
[NamedTheory(NameIt.MethodName, SplitBy.Underscore, JoinWith.Space)]
[InlineData(...)]
public void TheNameOfYourTestMethod2(...)
{}

```

### 3. Custom Implementation

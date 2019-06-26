# JobDependency
Repository to sort the Job dependency Challenge

## Algorithm
- Fetch the NonDependent jobs with outdegree as 0  
- Add all of those NonDependent Jobs to Queue
- Iterate the Queue 
   - Dequeue the current job from the NonDependent Queue
   - Add to the Result Sequence Queue
    - Iterate through the jobs which are dependent on the current job
      - Validate if dependent job is still dependent on any other job
        - If No then add to the Non Dependent Queue
- Validate if there any job still in the dependency graph   
  


## Unit Test

#### Should_Return_Empty_Job_Sequence_When_Empty_String_Is_Passed
``` C#
var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "" }),
                 new HashSet<Tuple<string, string>>());

            Assert.AreEqual(result, new List<string> { "" });
            Assert.AreEqual(result.Count, 1);
```

#### Should_Return_Single_Job_Sequence_When_Single_Job_Is_Passed
``` C#
 var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A" }),
                new HashSet<Tuple<string, string>>());

            Assert.AreEqual(result, new List<string> { "A" });
            Assert.AreEqual(result.Count, 1);
```

#### Should_Return_Job_Sequence_When_Jobs_With_No_Dependency_Are_Passed
``` C#
var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A" , "B" , "C" }),
                new HashSet<Tuple<string, string>>());

            Assert.AreEqual(result, new List<string> { "A" , "B", "C" });
            Assert.AreEqual(result.Count, 3);
```

#### Should_Return_Job_Sequence_When_Jobs_With_Dependency_Are_Passed
``` C#
   var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A", "B", "C", "D", "E", "F" }),
             new HashSet<Tuple<string, string>>(
                 new[] {
                        Tuple.Create("B" , "C"),
                        Tuple.Create("C", "F"),
                        Tuple.Create("D", "A"),
                        Tuple.Create("E" , "B"),

                 }));

            Assert.AreEqual(result, new List<string> { "A", "D", "F", "C", "B", "E" });
            Assert.AreEqual(result.Count, 6);
```

#### Should_Return_Null_When_Jobs_With_Circular_Dependency_Are_Passe
``` C#
  var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A" }),
               new HashSet<Tuple<string, string>>(new[] {
                        Tuple.Create("A" , "A") }));

            Assert.AreEqual(result, null);
```

## Scenario's Validated

#### Given the following job structure:
```
a =>
The result should be a sequence consisting of a single job a.
```

#### Given the following job structure:
```
a =>
b =>
c =>
The result should be a sequence containing all three jobs abc in no significant order.
```

#### Given the following job structure:
```
a =>
b => c
c =>
The result should be a sequence that positions c before b, containing all three jobs abc.
```

#### Given the following job structure:
```
a =>
b => c
c => f
d => a
e => b
f =>
The result should be a sequence that positions f before c, c before b, b before e and a before d containing all six jobs abcdef.
```

#### Given the following job structure:
```
a =>
b =>
c => c
The result should be an error stating that jobs can’t depend on themselves.
```

#### Given the following job structure:
```
a =>
b => c
c => f
d => a
e =>
f => b
The result should be an error stating that jobs can’t have circular dependencies.
```
